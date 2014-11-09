using UnityEngine;
using System.Collections;

public class CityWater : MonoBehaviour {

    public GameObject target;  // City to get statistics for
    CityListeners city;

	// Use this for initialization
    void Start () {
        city = target.GetComponent<CityListeners>();
	}
	
	// Update is called once per frame
	void Update () {
        if (city.isDrought)
        {
            guiText.text = "City destroyed by drought.";
        }
        else if (city.isFlooded)
        {
            guiText.text = "City destroyed by flood.";
        }
        else
        {
            int water = (int)city.waterLevel;
            guiText.text = water.ToString() + " / " + city.maxWaterCapacity.ToString();
        }
	}
}
