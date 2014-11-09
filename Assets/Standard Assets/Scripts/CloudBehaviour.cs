using UnityEngine;
using System.Collections;
using RTS;

public class CloudBehaviour : MonoBehaviour {

    public bool isSelected;
    
    // Use to see what weather is active
    public bool isRaining;
    public bool isHeavyRaining;
    public bool isLightning;
    public bool isTornado;

    // Use to set how long particles are enabled
    public float rainDuration;
    public float heavyRainDuration;
    public float LightningDuration;
    public float TornadoDuration;

    // Use to time how long a particle has been enabled
    private float deltaTimePassed;

    // Use to block simulatneous actions
    private bool activeWeather;

    ParticleEmitter[] emitters;
    ParticleEmitter rain;
    ParticleEmitter heavyRain;
    ParticleEmitter lightning;
    ParticleEmitter tornado;

    ParticleEmitter rainClone;
    ParticleEmitter heavyRainClone;
    ParticleEmitter lightningClone;
    ParticleEmitter tornadoClone;

    public bool hasDest;
    public Vector3 destination;
    public GameObject moveToObject;
    public Sprite rightClickSprite;

	// Use this for initialization
	void Start () {
        isSelected = false;
        destination = new Vector3();
        moveToObject = new GameObject("Move To Light");
        moveToObject.AddComponent<SpriteRenderer>();
        moveToObject.transform.Rotate(new Vector3(90f, 0f, 0f));
        SpriteRenderer moveToSprite = moveToObject.GetComponent<SpriteRenderer>();
        moveToSprite.sprite = rightClickSprite;
        moveToObject.SetActive(false);

        CursorBehaviour c = GameObject.FindObjectOfType<CursorBehaviour>();
        c.CursorLeftClick += OnCursorLeftClick;
        c.CursorRightClick += OnCursorRightClick;

        //Get all the Particle Emitters
        emitters = this.GetComponentsInChildren<ParticleEmitter>();

        //Assign Particle Emmitters to respective Particles
        for (int i = 0; i < emitters.Length; i++)
        {
            if (emitters[i].name == "Rain")
            {
                rain = emitters[i];
            }
            else if (emitters[i].name == "Heavy Rain")
            {
                heavyRain = emitters[i];
            }
            else if (emitters[i].name == "Lightning")
            {
                lightning = emitters[i];
            }
            else if (emitters[i].name == "Tornado")
            {
                tornado = emitters[i];
            }
        }

        // Intialize active bools
        activeWeather = false;
        isRaining = false;
        isHeavyRaining = false;
        isLightning = false;
        isTornado = false;

        // Destroy the Particle Emmitters
        rain.enabled = false;

        rainDuration = 3;
	}

    private void OnCursorLeftClick(object sender, CursorClickEventArgs e)
    {
        RaycastHit raycastInfo;

        Ray cursorRay = Camera.main.ScreenPointToRay(new Vector3(e.position.x, Screen.height - e.position.y, 0));//new Ray(startPos, Vector3.MoveTowards(startPos, cursorPos, Mathf.Infinity));
        //Debug.Log(string.Format("cursorray: {0}", cursorRay));
        //Debug.DrawRay(cursorRay.origin, cursorRay.direction * 1000, Color.blue, 10000f);
        bool rayCollides = Physics.Raycast(cursorRay, out raycastInfo, Mathf.Infinity);
        if (rayCollides)
        {            
            if (raycastInfo.collider.gameObject == this.gameObject)
            {
                //Debug.Log(string.Format("Ray collides with cloud at point {0}", raycastInfo.point));
                isSelected = true;
            }
            else
            {
                isSelected = false;
            }
        }
    }

    private void OnCursorRightClick(object sender, CursorClickEventArgs e)
    {
        RaycastHit raycastInfo;
        Ray cursorRay = Camera.main.ScreenPointToRay(new Vector3(e.position.x, Screen.height - e.position.y, 0));
        //Debug.DrawRay(cursorRay.origin, cursorRay.direction * 1000, Color.red, 10000f);
        bool rayCollides = Physics.Raycast(cursorRay, out raycastInfo, Mathf.Infinity);
        if (rayCollides)
        {
            if (isSelected && raycastInfo.collider.gameObject.tag == "Terrain")
            {
                //Debug.Log(string.Format("Right click ray collides with terrain at point {0}", raycastInfo.point));
                moveToObject.transform.position = raycastInfo.point - new Vector3(-5f, 0f, 0f);
                moveToObject.SetActive(true);
                destination = moveToObject.transform.position;
                hasDest = true;
            }
        }
    }
	
	// Update is called once per frame
	void Update () {

        // Cloud movement: always moves forward by default
        Vector3 forward = transform.TransformDirection(Vector3.forward * ResourceManager.CloudMoveSpeed * Time.deltaTime);
        this.transform.position += forward;

        // If there is a destination, head towards it
        if(hasDest)
        {
            Vector3 cloudForward = this.transform.forward;
            Vector3 cloudToDest = destination - this.transform.position;
            cloudToDest.y = 0.0f;
            Debug.Log(string.Format("cloudforward: {0}", cloudForward));
            Debug.Log(string.Format("cloudtodest: {0}", cloudToDest));
            
            Debug.DrawRay(this.transform.position, this.transform.forward, Color.blue, 10000f);
            Debug.DrawRay(this.transform.position, cloudToDest, Color.red, 10000f);

            Vector3 newRotation = Vector3.RotateTowards(cloudForward, cloudToDest, Mathf.Infinity, Mathf.Infinity);
            this.transform.rotation = Quaternion.LookRotation(newRotation);
        }

        // Control cloud rotation
        if (Input.GetKey("a"))
        {
            this.transform.Rotate(Vector3.up, (-1) * ResourceManager.CloudTurnSpeed * Time.deltaTime);
        }
        if (Input.GetKey("d"))
        {
            this.transform.Rotate(Vector3.up, ResourceManager.CloudTurnSpeed * Time.deltaTime);
        }

        // Control cloud spotlight
        Light groundLight = this.GetComponentInChildren<Light>();
        groundLight.enabled = isSelected;

        if (!activeWeather)
        {
            if (Input.GetKey("1"))
            {
                rainClone = (ParticleEmitter)Instantiate(rain, rain.transform.position, rain.transform.rotation);
                rainClone.transform.parent = rain.transform.parent;
                rainClone.enabled = true;
                activeWeather = true;
                isRaining = true;
                Debug.Log("Make it rain.");
            }
        }
        else
        {
            deltaTimePassed += Time.deltaTime;
            Debug.Log(string.Format("deltaTimePassed: {0}", deltaTimePassed));
            if (isRaining)
            {
                if (rainDuration <= deltaTimePassed)
                {
                    Destroy(rainClone);
                    isRaining = false;
                    activeWeather = false;
                    deltaTimePassed = 0;
                    Debug.Log("Stop making it rain...");
                }
            }
        }
	}


}
