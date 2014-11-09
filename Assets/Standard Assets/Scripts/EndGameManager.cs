using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EndGameManager : MonoBehaviour {

    public bool showEndLevelScreen;
    public bool showGoodTab;
    public Texture endLevelTexture;
    public Texture goodUpgradesTexture;
    public Texture badUpgradesTexture;
    public Texture lightningUpTexture;
    public Texture sizeUpTexture;
    public Texture healthUpTexture;
    public Texture cloudsUpTexture;
    public Texture speedUpTexture;
    public Texture turnSpeedUpTexture;

    
    Rect menuButtonRect;
    Rect continueButtonRect;
    Rect goodTabRect;
    Rect badTabRect;

    // Bad upgrades
    Rect lightningUpRect;
    Rect sizeUpRect;

    // Good upgrades
    Rect healthUpRect;
    Rect cloudsUpRect;
    Rect speedUpRect;
    Rect turnSpeedUpRect;

	// Use this for initialization
	void Start () {
        showEndLevelScreen = true;
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

        // Check upgrade buttons
        if(lightningUpRect.Contains(e.position))
        {
        }
    }

    void OnGUI()
    {
        GUI.depth = 0;

        if(showEndLevelScreen)
        {
            // Main menu rects
            Rect endLevelRect = new Rect(0, 0, Screen.width * (2f / 3f) , Screen.height - 20);
            Rect upgradesRect = new Rect(endLevelRect.xMax + 10, 20, Screen.width - endLevelRect.xMax - 40, Screen.height / 2);

            // Main menu button rects
            menuButtonRect = new Rect(endLevelRect.xMin + endLevelRect.width * (1f / 10f), endLevelRect.yMin + endLevelRect.height * (5.5f / 8f),
                                           endLevelRect.width * (1f / 3f), endLevelRect.height * (1f / 5f));
            continueButtonRect = new Rect(menuButtonRect.xMax + menuButtonRect.xMin, menuButtonRect.yMin, menuButtonRect.width, menuButtonRect.height);
            goodTabRect = new Rect(upgradesRect.xMin, upgradesRect.yMin, upgradesRect.width / 2f, upgradesRect.height / 4f);
            badTabRect = new Rect(goodTabRect.xMax, goodTabRect.yMin, upgradesRect.width - goodTabRect.width, goodTabRect.height);

            // Bad upgrade rects
            lightningUpRect = new Rect(upgradesRect.xMin + upgradesRect.width * (3f / 32f), 
                goodTabRect.yMax + upgradesRect.height * (1f/16f), upgradesRect.width / 6, upgradesRect.height / 6);
            sizeUpRect = new Rect(lightningUpRect.xMax + lightningUpRect.width / 4, lightningUpRect.yMin, lightningUpRect.width, lightningUpRect.height);


            // Good upgrade rects
            healthUpRect = lightningUpRect;
            cloudsUpRect = sizeUpRect;
            speedUpRect = new Rect(sizeUpRect.xMax + sizeUpRect.width / 4, sizeUpRect.yMin, sizeUpRect.width, sizeUpRect.height);
            turnSpeedUpRect = new Rect(speedUpRect.xMax + speedUpRect.width / 4, speedUpRect.yMin, speedUpRect.width, speedUpRect.height);
            
            GUI.DrawTexture(endLevelRect, endLevelTexture);
            
            // Draw whichever upgrades are visible
            if (showGoodTab)
            {
                GUI.DrawTexture(upgradesRect, goodUpgradesTexture);
                GUI.DrawTexture(healthUpRect, healthUpTexture);
                GUI.DrawTexture(cloudsUpRect, cloudsUpTexture);
                GUI.DrawTexture(speedUpRect, speedUpTexture);
                GUI.DrawTexture(turnSpeedUpRect, turnSpeedUpTexture);
            }
            else
            {
                GUI.DrawTexture(upgradesRect, badUpgradesTexture);
                GUI.DrawTexture(lightningUpRect, lightningUpTexture);
                GUI.DrawTexture(sizeUpRect, sizeUpTexture);
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
