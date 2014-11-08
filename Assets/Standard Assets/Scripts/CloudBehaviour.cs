using UnityEngine;
using System.Collections;
using RTS;

public class CloudBehaviour : MonoBehaviour {

    public bool isSelected;

	// Use this for initialization
	void Start () {
        isSelected = false;
        CursorBehaviour c = GameObject.FindObjectOfType<CursorBehaviour>();
        c.CursorLeftClick += OnCursorLeftClick;
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
	}


}
