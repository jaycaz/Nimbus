using UnityEngine;
using System.Collections;
using RTS;

public class CursorBehaviour : MonoBehaviour {

    public Texture cursorTexture;
    public float xpos;
    public float ypos;

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
        //Debug.Log(string.Format("dx= {0}, dy = {0}", dx, dy));
        xpos += dx * ResourceManager.MouseSensitivity;
        ypos += dy * ResourceManager.MouseSensitivity;

        xpos = Mathf.Max(0f, xpos);
        xpos = Mathf.Min(Screen.width, xpos);
        ypos = Mathf.Max(0f, ypos);
        ypos = Mathf.Min(Screen.height, ypos);

        GUI.DrawTexture(new Rect(xpos, ypos, 50.0f, 50.0f), cursorTexture, ScaleMode.StretchToFill, true, 10.0F);
    }
	
	// Update is called once per frame
	void Update () {        

	}
}
