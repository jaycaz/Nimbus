using UnityEngine;
using System.Collections;
using System;
using RTS;

public class CursorClickEventArgs : EventArgs
{
    public Vector2 position;
}

public class CursorBehaviour : MonoBehaviour {

    public Texture cursorTexture;
    public delegate void CursorLeftClickHandler(object sender, CursorClickEventArgs e);
    public delegate void CursorRightClickHandler(object sender, CursorClickEventArgs e);
    public event CursorLeftClickHandler CursorLeftClick;
    public event CursorRightClickHandler CursorRightClick;

    public float xpos;
    public float ypos;

    public Vector2 Position { get { return new Vector2(xpos, ypos); } }

	// Use this for initialization
	void Start () {
        Screen.lockCursor = true;
        Screen.showCursor = false;
        xpos = Screen.width / 2;
        ypos = Screen.height / 2;
	}

    void OnGUI()
    {
        GUI.depth = -1;

        if (!cursorTexture)
        {
            Debug.LogError("Assign a Texture in the inspector.");
            return;
        }

        float dx = Input.GetAxis("Mouse X");
        float dy = Input.GetAxis("Mouse Y") * (-1);
        //Debug.Log(string.Format("dx= {0}, dy = {0}", dx, dy));
        xpos += dx * ResourceManager.MouseSensitivity * Time.deltaTime;
        ypos += dy * ResourceManager.MouseSensitivity * Time.deltaTime;

        xpos = Mathf.Max(0f, xpos);
        xpos = Mathf.Min(Screen.width, xpos);
        ypos = Mathf.Max(0f, ypos);
        ypos = Mathf.Min(Screen.height, ypos);

        GUI.DrawTexture(new Rect(xpos, ypos, 50.0f, 50.0f), cursorTexture, ScaleMode.StretchToFill, true, 10.0F);

        // Left mouse click
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 pos = Position;
            var e = new CursorClickEventArgs();
            e.position = pos;
            OnCursorLeftClick(e);
        }
        if (Input.GetMouseButtonDown(1))
        {
            Vector2 pos = Position;
            var e = new CursorClickEventArgs();
            e.position = pos;
            OnCursorRightClick(e);
        }
        /*if (Input.GetMouseButtonDown(2))
        {
            Vector2 pos = Position;
            var e = new CursorClickEventArgs();
            e.position = pos;
            OnCursorMiddleClick(e);
        }*/
    }

    void OnCursorLeftClick(CursorClickEventArgs e)
    {
        CursorLeftClickHandler handler = CursorLeftClick;
        if(handler != null)
        {
            handler(this, e);
        }
    }

    void OnCursorRightClick(CursorClickEventArgs e)
    {
        CursorRightClickHandler handler = CursorRightClick;
        if(handler != null)
        {
            handler(this, e);
        }
    }
	
	// Update is called once per frame
	void Update () {        

	}
}
