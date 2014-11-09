using UnityEngine;
using System.Collections;

public class CityListeners : MonoBehaviour {

    // Variables for Water Capacities
    public float maxWaterCapacity;
    public float waterLevel;
    public float waterConsumptionRate;
    
    // Variables for Weather
    public float rainRate;
    public float heavyRainRate;

    // Variables for Low Water
    public float lowWaterLevel;
    public bool isDrought;

    // Variables for High Water
    public float highWaterLevel;
    public bool isFlooded;

    ParticleEmitter[] emitters;
    ParticleEmitter dust;
    ParticleEmitter flood;

    private bool isDusting;
    private bool isFlooding;

	// Use this for initialization
	void Start () {


        //Get all the Particle Emitters
        emitters = this.GetComponentsInChildren<ParticleEmitter>();

        //Assign Particle Emmitters to respective Particles
        for (int i = 0; i < emitters.Length; i++)
        {
            if (emitters[i].name == "Dust")
            {
                dust = emitters[i];
            }
            else if (emitters[i].name == "Flood")
            {
                flood = emitters[i];
            }
        }

        dust.emit = false;
        flood.emit = false;
        isFlooded = false;
        isDrought = false;
	}
	
	// Update is called once per frame
	void Update () {
        waterLevel -= waterConsumptionRate * Time.deltaTime;
        Debug.Log(string.Format("waterLevel: {0}", waterLevel));
        if (waterLevel <= lowWaterLevel)
        {
            isDusting = true;
            dust.emit = isDusting;
        }
        else if (waterLevel >= highWaterLevel)
        {
            isFlooding = true;
            flood.emit = isFlooding;
        }
        else
        {
            if (isDusting)
            {
                isDusting = false;
                dust.emit = isDusting;
            }
            else if (isFlooding)
            {
                isFlooding = true;
                flood.emit = isFlooding;
            }
        }
        
        if (isDrought || isFlooded) {
            this.transform.position -= new Vector3(0f, .1f, 0f);
            if (this.transform.position.y < -20)
            {
                Destroy(this);
            }
        }
        else {
            if (waterLevel >= maxWaterCapacity){
                isFlooded = true;
             }
            else if (waterLevel <= 0)
            {
               isDrought = true;
            }
        }
	}

    void OnTriggerEnter(Collider other)
    {
        CloudBehaviour cloud = other.GetComponent<CloudBehaviour>();
        //Debug.Log("There's a cloud in the city.");
        if (cloud.isRaining)
        {
            //Debug.Log("ITS RAINING IN THE CITY!");
            waterLevel += rainRate * Time.deltaTime;
        }
        else if (cloud.isHeavyRaining)
        {
            //Debug.Log("ITS RAINING IN THE CITY!");
            waterLevel += heavyRainRate * Time.deltaTime;
        }
    }

    void OnTriggerStay(Collider other)
    {
        CloudBehaviour cloud = other.GetComponent<CloudBehaviour>();
        //Debug.Log("There's a cloud in the city.");
        
        if (cloud.isRaining)
        {
           // Debug.Log("ITS RAINING IN THE CITY!");
            waterLevel += rainRate * Time.deltaTime;
        }
        else if (cloud.isHeavyRaining)
        {
            //Debug.Log("ITS RAINING IN THE CITY!");
            waterLevel += heavyRainRate * Time.deltaTime;
        }
    }

    void OnTriggerExit(Collider other)
    {
        //Debug.Log("Exit");
    }
}
