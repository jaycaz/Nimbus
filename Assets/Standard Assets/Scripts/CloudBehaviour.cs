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

	// Use this for initialization
	void Start () {
        isSelected = false;
        CursorBehaviour c = GameObject.FindObjectOfType<CursorBehaviour>();
        c.CursorLeftClick += OnCursorLeftClick;

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
        //Debug.Log(string.Format("Left Mouse Clicked (cloud)! {0}", UnityEngine.Random.Range(1, 100)));\
        
    }
	
	// Update is called once per frame
	void Update () {
        //Debug.Log(string.Format("Rotation: {0}", transform.rotation));
        // Cloud movement: always moves forward
        Vector3 forward = transform.TransformDirection(Vector3.forward * ResourceManager.CloudMoveSpeed * Time.deltaTime);
        this.transform.position += forward;
        //Debug.Log(Time.deltaTime);

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
