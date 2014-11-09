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
        //Debug.Log(string.Format("Left Mouse Clicked (cloud)! {0}", e.position));
        RaycastHit raycastInfo;
        //Vector3 startPos = Camera.main.transform.TransformPoint(Camera.main.transform.localPosition);        
        //Vector3 cursorPos = Camera.main.ScreenToWorldPoint(new Vector3(e.position.x, e.position.y, 0f));

        Ray cursorRay = Camera.main.ScreenPointToRay(new Vector3(e.position.x, Screen.height - e.position.y, 0));//new Ray(startPos, Vector3.MoveTowards(startPos, cursorPos, Mathf.Infinity));
        //Debug.Log(string.Format("cursorray: {0}", cursorRay));
        Debug.DrawRay(cursorRay.origin, cursorRay.direction * 1000, Color.blue, 10000f);
        bool rayCollides = Physics.Raycast(cursorRay, out raycastInfo, Mathf.Infinity);
        if (rayCollides)
        {
            Debug.Log(string.Format("Ray collides with something at point {0}", raycastInfo.point));
            if (raycastInfo.collider.gameObject == this.gameObject)
            {
                isSelected = true;
            }
            else
            {
                isSelected = false;
            }
        }
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
