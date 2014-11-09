using UnityEngine;
using System.Collections;
using System;
using RTS;

public class CloudBehaviour : MonoBehaviour {

    public bool isSelected;

    public float rainRate;
    public float downpourRate;

    // Use to see what weather is active
    public bool isRaining;
    public bool isDownpouring;
    public bool isLightning;
    public bool isTornado;

    // Use to set how long particles are enabled
    public float rainDuration;
    public float downpourDuration;
    public float lightningDuration;
    public float tornadoDuration;

    // Use to time how long a particle has been enabled
    private float deltaTimePassed;

    public float water;
    private float initialWater;
    private float initialAlpha;

    // Use to block simulatneous actions
    private bool activeWeather;

    ParticleEmitter[] emitters;
    ParticleEmitter rain;
    ParticleEmitter downpour;
    ParticleSystem lightning;
    ParticleEmitter tornado;

    public bool hasDest;
    public Vector3 destination;
    public GameObject markerPrefab;
    private GameObject moveToMarker;
    private MoveToMarkerBehaviour moveToMarkerScript;

	// Use this for initialization
	void Start () {
        isSelected = false;
        destination = new Vector3();


        // Initialize game object to show "move here" animation
        moveToMarker = (GameObject)Instantiate(markerPrefab);
        moveToMarkerScript = moveToMarker.GetComponent<MoveToMarkerBehaviour>();
        //SpriteRenderer moveToSprite = moveToMarker.GetComponent<SpriteRenderer>();
        //Animator spriteAnimator = moveToMarker.GetComponent<Animator>();
        moveToMarker.SetActive(false);

        CursorBehaviour c = GameObject.FindObjectOfType<CursorBehaviour>();
        c.CursorLeftClick += OnCursorLeftClick;
        c.CursorRightClick += OnCursorRightClick;

        GUIBehaviour g = GameObject.FindObjectOfType<GUIBehaviour>();
        g.TriggerRain += OnRain;
        g.TriggerDownpour += OnDownpour;
        g.TriggerLightning += OnLightning;
        g.TriggerTornado += OnTornado;

        //Get all the Particle Emitters
        emitters = this.GetComponentsInChildren<ParticleEmitter>();

        //Assign Particle Emmitters to respective Particles
        for (int i = 0; i < emitters.Length; i++)
        {
            if (emitters[i].name == "Rain")
            {
                rain = emitters[i];
            }
            else if (emitters[i].name == "Downpour")
            {
                downpour = emitters[i];
            }
            else if (emitters[i].name == "Tornado")
            {
                tornado = emitters[i];
            }
        }

        lightning = this.GetComponentInChildren<ParticleSystem>();

        // Intialize active bools
        activeWeather = false;
        isRaining = false;
        rain.emit = false;
        isDownpouring = false;
        downpour.emit = false;
        isLightning = false;
        lightning.enableEmission = false;
        isTornado = false;

        // Destroy the Particle Emmitters

        initialWater = water;

        initialAlpha = this.renderer.material.GetColor("_TintColor").a;
	
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
            if (this != null && raycastInfo.collider.gameObject == this.gameObject)
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
        RaycastHit[] raycastInfo;
        Ray cursorRay = Camera.main.ScreenPointToRay(new Vector3(e.position.x, Screen.height - e.position.y, 0));
        //Debug.DrawRay(cursorRay.origin, cursorRay.direction * 1000, Color.red, 10000f);
        
        raycastInfo = Physics.RaycastAll(cursorRay, Mathf.Infinity);
        if (isSelected && raycastInfo.Length != 0)
        {
            foreach(RaycastHit hit in raycastInfo)
            {
                if (hit.collider.gameObject.tag == "Terrain")
                {
                    //Debug.Log(string.Format("Right click ray collides with terrain at point {0}", raycastInfo.point));
                    moveToMarker.transform.position = hit.point + new Vector3(0f, 5f, 0f);
                    destination = moveToMarker.transform.position;
                    moveToMarkerScript.animate();
                    hasDest = true;
                    break;
                }
            }
            
        }
    }

    void OnRain(object sender, EventArgs e)
    {
        if (!isSelected || activeWeather)
        {
            return;
        }
        activeWeather = true;
        isRaining = true;
        rain.emit = true;
        //Debug.Log("Make it rain.");
    }

    void OnDownpour(object sender, EventArgs e)
    {
        if (!isSelected || activeWeather)
        {
            return;
        }
        activeWeather = true;
        isDownpouring = true;
        downpour.emit = true;
        Debug.Log("MAKE IT RAIN!");
    }
	
    void OnLightning(object sender, EventArgs e)
    {
        if (!isSelected || activeWeather)
        {
            return;
        }
        activeWeather = true;
        isLightning = true;
        lightning.enableEmission = true;
        Debug.Log("Zeus-ed!");
    }

    void OnTornado(object sender, EventArgs e)
    {
        //throw new NotImplementedException();
    }

    // Update is called once per frame
	void Update () {

        // Water decreaases with time
        water -= Time.deltaTime;
        Color newColor;
        newColor = this.renderer.material.GetColor("_TintColor");
        newColor.a = water * initialAlpha / initialWater;

        this.renderer.material.SetColor("_TintColor", newColor);
        if (water < 0)
        {
            Destroy(this.gameObject);
        }

        // Cloud movement: always moves forward by default
        Vector3 forward = transform.TransformDirection(Vector3.forward * ResourceManager.CloudMoveSpeed * Time.deltaTime);
        this.transform.position += forward;

        // If not at ideal elevation, rise towards it
        Vector3 targetPoint = this.transform.position;
        targetPoint.y = ResourceManager.CloudHeight;
        Vector3 up = Vector3.MoveTowards(this.transform.position, targetPoint, ResourceManager.CloudMoveSpeed * Time.deltaTime);
        //Debug.Log(up);

        this.transform.position = up;


        // If there is a destination, head towards it
        if(hasDest)
        {
            Vector3 cloudForward = this.transform.forward;
            Vector3 cloudToDest = destination - this.transform.position;
            cloudToDest.y = 0.0f;
            //Debug.Log(string.Format("cloudforward: {0}", cloudForward));
            //Debug.Log(string.Format("cloudtodest: {0}", cloudToDest));
            
            Debug.DrawRay(this.transform.position, this.transform.forward, Color.blue, 10000f);
            Debug.DrawRay(this.transform.position, cloudToDest, Color.red, 10000f);

            Vector3 newRotation = Vector3.RotateTowards(cloudForward, cloudToDest, 
                ResourceManager.CloudAutoTurnSpeed * Time.deltaTime, Mathf.Infinity);
            this.transform.rotation = Quaternion.LookRotation(newRotation);
        }


        // Control cloud spotlight
        Light groundLight = this.GetComponentInChildren<Light>();
        groundLight.enabled = isSelected;
        
        // Check if weather should stop
        if (activeWeather)
        {
            deltaTimePassed += Time.deltaTime;
            //Debug.Log(string.Format("deltaTimePassed: {0}", deltaTimePassed));
            if (isRaining)
            {
                water -= Time.deltaTime * rainRate;
                if (rainDuration <= deltaTimePassed)
                {
                    isRaining = false;
                    rain.emit = false;
                    activeWeather = false;
                    deltaTimePassed = 0;
                    //Debug.Log("Stop making it rain...");
                }
            }
            if (isDownpouring)
            {
                water -= Time.deltaTime * downpourRate;
                if (downpourDuration <= deltaTimePassed)
                {
                    isDownpouring = false;
                    downpour.emit = false;
                    activeWeather = false;
                    deltaTimePassed = 0;
                    Debug.Log("Stop making it downpour...");
                }
            }
            if (isLightning)
            {
                if (lightningDuration <= deltaTimePassed)
                {
                    isLightning = false;
                    lightning.enableEmission = false;
                    activeWeather = false;
                    deltaTimePassed = 0;
                    Debug.Log("Stop making striking down cities...");
                }
            }
        }

        // Code past here should only execute if the cloud is selected
        if (isSelected)
        {
            // Control cloud rotation
            if (Input.GetKey("a"))
            {
                this.transform.Rotate(Vector3.up, (-1) * ResourceManager.CloudManualTurnSpeed * Time.deltaTime);
            }
            if (Input.GetKey("d"))
            {
                this.transform.Rotate(Vector3.up, ResourceManager.CloudManualTurnSpeed * Time.deltaTime);
            }
        }
	}


}
