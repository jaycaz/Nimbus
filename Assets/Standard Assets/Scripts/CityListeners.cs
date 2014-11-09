using UnityEngine;
using System.Collections;

public class CityListeners : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	}

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Enter");
    }

    void OnTriggerStay(Collider other)
    {
        //Debug.Log("Stay");
    }

    void OnTriggerExit(Collider other)
    {
        //Debug.Log("Exit");
    }
}
