using UnityEngine;
using System.Collections;

public class EndGameManager : MonoBehaviour {

    public bool showEndLevelScreen;
    public bool showGoodTab;
    public Texture endLevelTexture;
    public Texture goodUpgradesTexture;
    public Texture badUpgradesTexture;

    Rect menuButtonRect;
    Rect continueButtonRect;
    Rect goodTabRect;
    Rect badTabRect;

	// Use this for initialization
	void Start () {
        showEndLevelScreen = false;
        showGoodTab = true;
        CursorBehaviour c = GameObject.FindObjectOfType<CursorBehaviour>();
        c.CursorLeftClick += OnCursorLeftClick;
	}

    private void OnCursorLeftClick(object sender, CursorClickEventArgs e)
    {
        if(!showEndLevelScreen)
        {
            return;
        }

        //Debug.Log(string.Format("Click: {0}", e.position));
        Debug.Log(string.Format("bad tab: {0}", badTabRect));
        if(menuButtonRect.Contains(e.position))
        {
            // TODO: Go to main menu
        }
        if (continueButtonRect.Contains(e.position))
        {
            // TODO: Continue to next level
        }
        if (goodTabRect.Contains(e.position))
        {
            showGoodTab = true;
        }
        if (badTabRect.Contains(e.position))
        {
            showGoodTab = false;
        }
    }

    void OnGUI()
    {
        GUI.depth = 0;

        if(showEndLevelScreen)
        {
            Rect endLevelRect = new Rect(0, 0, Screen.width * (2f / 3f) , Screen.height - 20);
            Rect upgradesRect = new Rect(endLevelRect.xMax + 10, 20, Screen.width - endLevelRect.xMax - 40, Screen.height / 2);
            menuButtonRect = new Rect(endLevelRect.xMin + endLevelRect.width * (1f / 10f), endLevelRect.yMin + endLevelRect.height * (5.5f / 8f),
                                           endLevelRect.width * (1f / 3f), endLevelRect.height * (1f / 5f));
            continueButtonRect = new Rect(menuButtonRect.xMax + menuButtonRect.xMin, menuButtonRect.yMin, menuButtonRect.width, menuButtonRect.height);
            goodTabRect = new Rect(upgradesRect.xMin, upgradesRect.yMin, upgradesRect.width / 2f, upgradesRect.height / 4f);
            badTabRect = new Rect(goodTabRect.xMax, goodTabRect.yMin, upgradesRect.width - goodTabRect.width, goodTabRect.height);
            
            GUI.DrawTexture(endLevelRect, endLevelTexture);
            
            if (showGoodTab)
            {
                GUI.DrawTexture(upgradesRect, goodUpgradesTexture);
            }
            else
            {
                GUI.DrawTexture(upgradesRect, badUpgradesTexture);
            }

            // Display boxes capturing mouse input

            //GUI.Box(menuButtonRect, "Menu");
            //GUI.Box(continueButtonRect, "Continue");
            //GUI.Box(goodTabRect, "Good");
            //GUI.Box(badTabRect, "Bad");
            
        
        }
    }
	
	// Update is called once per frame
	void Update () {
	}
}
