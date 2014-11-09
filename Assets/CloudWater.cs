using UnityEngine;
using System.Collections;

public class CloudWater : MonoBehaviour {

    public GameObject target;  // City to get statistics for
    CloudBehaviour cloud;

    // Use this for initialization
    void Start()
    {
        cloud = target.GetComponent<CloudBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        int water = (int)cloud.water;
        guiText.text = water.ToString();
    }
}
