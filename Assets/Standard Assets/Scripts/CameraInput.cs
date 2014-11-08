using UnityEngine;
using System.Collections;
using RTS;

public class CameraInput : MonoBehaviour
{

    //private Player player;

    // Use this for initialization
    void Start()
    {
        //player = transform.root.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (player && player.human)
        //{
        MoveCamera();
        CheckForSnapToPlayer();
        //}
    }

    private void MoveCamera()
    {
        //float xpos = Input.mousePosition.x;
        //float ypos = Input.mousePosition.y;
        CursorBehaviour cursor = GameObject.Find("Cursor").GetComponent<CursorBehaviour>();
        float xpos = cursor.xpos;
        float ypos = cursor.ypos;
        //Debug.Log(string.Format("cursor(x,y): ({0}, {1})", xpos, ypos));

        Vector3 movement = new Vector3(0, 0, 0);
        float scrollSpeed = ResourceManager.ScrollSpeed * Time.deltaTime;

        //horizontal camera movement
        if (xpos >= 0 && xpos < ResourceManager.ScrollWidth)
        {
            movement.x -= scrollSpeed;
        }
        else if (xpos <= Screen.width && xpos > Screen.width - ResourceManager.ScrollWidth)
        {
            movement.x += scrollSpeed;
        }

        //vertical camera movement
        if (ypos >= 0 && ypos < ResourceManager.ScrollWidth)
        {
            movement.z += scrollSpeed;
        }
        else if (ypos <= Screen.height && ypos > Screen.height - ResourceManager.ScrollWidth)
        {
            movement.z -= scrollSpeed;
        }

        //make sure movement is in the direction the camera is pointing
        movement = Camera.main.transform.TransformDirection(movement);
        movement.y = 0;

        //zoom movement
        Vector3 zoomDirection = Camera.main.transform.TransformDirection(Vector3.forward) * ResourceManager.ZoomSpeed * Time.deltaTime;
        
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if(scroll != 0.0f)
        {
            movement += zoomDirection * Mathf.Sign(scroll);
        }

        //calculate desired camera position based on received input
        Vector3 origin = Camera.main.transform.position;
        Vector3 destination = origin;
        destination.x += movement.x;
        destination.y += movement.y;
        destination.z += movement.z;

        //limit away from ground movement to be between a minimum and maximum distance
        if (destination.y > ResourceManager.MaxCameraHeight)
        {
            destination.y = ResourceManager.MaxCameraHeight;
        }
        else if (destination.y < ResourceManager.MinCameraHeight)
        {
            destination.y = ResourceManager.MinCameraHeight;
        }

        //if a change in position is detected perform the necessary update
        if (destination != origin)
        {
            Camera.main.transform.position = destination;//Vector3.MoveTowards(origin, destination, Time.deltaTime * ResourceManager.ScrollSpeed);
        }
    }

    private void CheckForSnapToPlayer()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            // Moves to look at player.  Changes distance but preserves rotation
            Vector3 playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
            Vector3 abovePlayerPos = playerPos - (Camera.main.transform.TransformDirection(Vector3.forward) * ResourceManager.SnapToPlayerDist);
            Camera.main.transform.position = abovePlayerPos;
        }
    }
}