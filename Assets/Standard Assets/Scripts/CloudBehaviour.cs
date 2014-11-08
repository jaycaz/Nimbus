using UnityEngine;
using System.Collections;
using RTS;

public class CloudBehaviour : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        //Debug.Log(string.Format("Rotation: {0}", transform.rotation));

        // Cloud movement: always moves forward
        Vector3 forward = transform.TransformDirection(Vector3.forward * ResourceManager.CloudMoveSpeed);
        this.transform.position += forward;

        // Control cloud rotation
        if (Input.GetKey("a"))
        {
            this.transform.Rotate(Vector3.up, (-1) * ResourceManager.CloudTurnSpeed);
        }
        if (Input.GetKey("d"))
        {
            this.transform.Rotate(Vector3.up, ResourceManager.CloudTurnSpeed);
        }
	}
}
