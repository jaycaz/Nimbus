using UnityEngine;
using System.Collections;

public class SmogBehaviour : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
    void Update()
    {
        GameObject[] clouds = GameObject.FindGameObjectsWithTag("Cloud");
        GameObject closest = null;
        float distance = Mathf.Infinity;

        for (int i = 0; i < clouds.Length; i++)
        {
            Vector3 difference = (clouds[i].transform.position - this.transform.position);
            float currentDistance = difference.sqrMagnitude;
            if (currentDistance < distance)
            {
                closest = clouds[i];
                distance = currentDistance;
            }
        }
        if (closest != null)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, closest.transform.position, 0.03f);
        }
    }

    void OnTriggerStay(Collider other)
    {
        Debug.Log("Collisoin with Smog!");
        CloudBehaviour cloud = other.GetComponent<CloudBehaviour>();
        cloud.water -= 3f * Time.deltaTime;
    }
}
