﻿using UnityEngine;
using System.Collections;

public class CityListeners : MonoBehaviour {

    // Variables for Water Capacities
    public float maxWaterCapacity;
    public float waterLevel;
    public float waterConsumptionRate;

    private void cloudChangeWaterLevel(float change)
    {
        if((int)(waterLevel + change) > (int) waterLevel)
        {
            if(isDusting)
            {
                scoring.avgGoodReward();
            }
            else
            {
                scoring.smallGoodReward();
            }
        }
        else
        {
            if(isDusting)
            {
                scoring.avgBadReward();
            }
            else
            {
                scoring.smallBadReward();
            }
        }
        
        waterLevel += change;
    }

    // Variables for Water Levels
    private float lowWaterLevel;
    private float highWaterLevel;
    public bool isDrought;
    public bool isFlooded;

    ParticleEmitter[] emitters;
    ParticleEmitter dust;
    ParticleEmitter flood;

    private bool isDusting;
    private bool isFlooding;
    private bool lightningDestroy;

    private ScoreManager scoring;

	// Use this for initialization
	void Start () {
        // Set Up water levels
        highWaterLevel = maxWaterCapacity - 2;
        lowWaterLevel = 20 * waterConsumptionRate;

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

        // Set up connection to score manager
        scoring = GameObject.FindObjectOfType<ScoreManager>();
        lightningDestroy = false;
	}
	
	// Update is called once per frame
	void Update () {
        waterLevel -= waterConsumptionRate * Time.deltaTime;
        //Debug.Log(string.Format("waterLevel: {0}", waterLevel));
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
                isFlooding = false;
                flood.emit = isFlooding;
            }
        }
        
        if (isDrought || isFlooded) {
            scoring.largeBadReward();
            this.transform.position -= new Vector3(0f, .1f, 0f);
            if (this.transform.position.y < -20)
            {
                Destroy(this.gameObject);
            }
        }
        else {
            if (waterLevel >= maxWaterCapacity)
            {
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
            cloudChangeWaterLevel(cloud.rainRate * Time.deltaTime);
        }
        else if (cloud.isDownpouring)
        {
            //Debug.Log("ITS RAINING IN THE CITY!");
            cloudChangeWaterLevel(cloud.downpourRate * Time.deltaTime);
        }
        else if (cloud.isLightning && !lightningDestroy)
        {
            //Debug.Log("ITS RAINING IN THE CITY!");
            waterLevel -= 1;
            bool buildingFound = false;
            GameObject building = null;

            for (int i = 0; i <= this.gameObject.transform.childCount; i++)
            {
                building = this.gameObject.transform.GetChild(0).gameObject;
                if (building.name != "Dust" && building.name != "Flood" && building.name != "GUI Text")
                {
                    buildingFound = true;
                    break;
                }
            }
            if (buildingFound)
            {
                lightningDestroy = true;
                Destroy(building);
            }
            else
            {
                lightningDestroy = true;
                Destroy(this.gameObject);
            }
            
        }
    }

    void OnTriggerStay(Collider other)
    {
        CloudBehaviour cloud = other.GetComponent<CloudBehaviour>();
        //Debug.Log("There's a cloud in the city.");
        
        if (cloud.isRaining)
        {
           // Debug.Log("ITS RAINING IN THE CITY!");
            cloudChangeWaterLevel(cloud.rainRate * Time.deltaTime);
        }
        else if (cloud.isDownpouring)
        {
            //Debug.Log("ITS RAINING IN THE CITY!");
            cloudChangeWaterLevel(cloud.downpourRate * Time.deltaTime);
        }
        else if (cloud.isLightning && !lightningDestroy)
        {
            //Debug.Log("ITS RAINING IN THE CITY!");
            waterLevel -= 1;
            bool buildingFound = false;
            GameObject building = null;

            for (int i = 0; i <= this.gameObject.transform.GetChildCount(); i++)
            {
                building = this.gameObject.transform.GetChild(0).gameObject;
                if (building.name != "Dust" && building.name != "Flood" && building.name != "GUI Text")
                {
                    buildingFound = true;
                    break;
                }
            }
            if (buildingFound)
            {
                lightningDestroy = true;
                Destroy(building);

            }
            else
            {
                lightningDestroy = true;
                Destroy(this.gameObject);

            }

        }
    }

    void OnTriggerExit(Collider other)
    {
        //Debug.Log("Exit");
    }
}
