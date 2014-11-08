using UnityEngine;
using System.Collections;

public class CursorBehaviour : MonoBehaviour {

    public Texture cursorTexture;
    public float xpos;
    public float ypos;
    public const int MOUSE_SENSITIVITY = 15;

	// Use this for initialization
	void Start () {
        Screen.lockCursor = true;
        Screen.showCursor = false;
        xpos = Screen.width / 2;
        ypos = Screen.height / 2;
	}

    void OnGUI()
    {
        if (!cursorTexture)
        {
            Debug.LogError("Assign a Texture in the inspector.");
            return;
        }

        float dx = Input.GetAxis("Mouse X");
        float dy = Input.GetAxis("Mouse Y") * (-1);
        Debug.Log(string.Format("dx= {0}, dy = {0}", dx, dy));
        xpos += dx * MOUSE_SENSITIVITY;
        ypos += dy * MOUSE_SENSITIVITY;

        GUI.DrawTexture(new Rect(xpos, ypos, 50.0f, 50.0f), cursorTexture, ScaleMode.StretchToFill, true, 10.0F);
    }
	
	// Update is called once per frame
	void Update () {        

	}
}
