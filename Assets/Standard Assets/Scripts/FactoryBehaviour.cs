using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FactoryBehaviour : MonoBehaviour {

    public List<GameObject> clouds;
    public GameObject cloudPrefab;


	// Use this for initialization
	void Start () {

        //cloudPrefab = GameObject.FindGameObjectWithTag("Cloud");

        GUIBehaviour g = GameObject.FindObjectOfType<GUIBehaviour>();
        Debug.Log("Adding cloud event to factory");
        g.TriggerAddCloud += OnTriggerAddCloud;
	}

    private void OnTriggerAddCloud(object sender, System.EventArgs e)
    {
        Debug.Log("Trigger Add Cloud");
        Transform spawnPoint = this.transform.FindChild("Cloud_Spawn_Point");
        GameObject cloud = (GameObject)Instantiate(cloudPrefab, spawnPoint.position, spawnPoint.rotation);
        clouds.Add(cloud);
        if (cloud != null)
        {
            Debug.Log(string.Format("Cloud {0} created!", clouds[clouds.Count - 1]));
        }
    }
	
	// Update is called once per frame
	void Update () {
        
        // Check if any clouds destroyed themselves
        for(int i = clouds.Count - 1; i > 0; i--)
        {
            GameObject cloud = clouds[i];
            if(cloud == null)
            {
                clouds.Remove(cloud);
            }
        }
	}
}
