using UnityEngine;
using System.Collections;

public class CityListeners : MonoBehaviour {

    public float maxWaterCapacity;
    public float currentWater;
    public bool isRaining;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	}

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("There's a cloud in the city.");
        CloudBehaviour cloud = other.GetComponent<CloudBehaviour>();
        if (cloud.isRaining)
        {
            Debug.Log("ITS RAINING IN THE CITY!");
        }
    }

    void OnTriggerStay(Collider other)
    {
        Debug.Log("There's a cloud in the city.");
        CloudBehaviour cloud = other.GetComponent<CloudBehaviour>();
        if (cloud.isRaining)
        {
            Debug.Log("ITS RAINING IN THE CITY!");
        }
    }

    void OnTriggerExit(Collider other)
    {
        Debug.Log("Exit");
    }
}
