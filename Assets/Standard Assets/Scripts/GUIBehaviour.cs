﻿using UnityEngine;
using System.Collections;

[System.Serializable]
public class GUIBehaviour : MonoBehaviour {

    public Texture rainTexture;
    public Texture rainActiveTexture;
    public Texture downpourTexture;
    public Texture downpourActiveTexture;
    public Texture lightningTexture;
    public Texture lightningActiveTexture;
    public Texture tornadoTexture;
    public Texture tornadoActiveTexture;
    public Texture sunTexture;
    
    private Rect rainRect;
    private Rect downpourRect;
    private Rect lightningRect;
    private Rect tornadoRect;
    private Rect sunRect;
    
    public enum Ability
    {
        None,
        Rain,
        Downpour,
        Lightning,
        Tornado
    };

    public Ability abilitySelected;

	// Use this for initialization
	void Start () {
        abilitySelected = Ability.None;
	}

    void OnGUI()
    {
        float scale = (1f / 8f);

        // Draw sun
        sunRect = new Rect(Screen.width - 120f, Screen.height * (2f/3f), sunTexture.width * scale, sunTexture.height * scale);
        GUI.DrawTexture(sunRect, sunTexture);

        // Calculate placement of buttons
        GUIStyle buttonStyle = new GUIStyle();
        
        rainRect = new Rect(sunRect.xMin, sunRect.yMin - 55f, rainTexture.width * scale, rainTexture.height * scale);
        downpourRect = new Rect(rainRect.xMin - 50f, rainRect.yMax - 15f, downpourTexture.width * scale, downpourTexture.height * scale);
        lightningRect = new Rect(downpourRect.xMin, downpourRect.yMax + 20f, lightningTexture.width * scale, lightningTexture.height * scale);
        tornadoRect = new Rect(rainRect.xMin, lightningRect.yMax - 15f, tornadoTexture.width * scale, tornadoTexture.height * scale);

        Texture rainImage = abilitySelected == Ability.Rain ? rainActiveTexture : rainTexture;
        Texture downpourImage = abilitySelected == Ability.Downpour ? downpourActiveTexture : downpourTexture;
        Texture lightningImage = abilitySelected == Ability.Lightning ? lightningActiveTexture : lightningTexture;
        Texture tornadoImage = abilitySelected == Ability.Tornado ? tornadoActiveTexture : tornadoTexture;

        // Cycle through abilities using number keys
        if (GUI.Button(rainRect, rainImage, buttonStyle) || Input.GetKeyDown("1"))
        {
            abilitySelected = Ability.Rain;
        }

        if (GUI.Button(downpourRect, downpourImage, buttonStyle) || Input.GetKeyDown("2"))
        {
            abilitySelected = Ability.Downpour;
        }

        if (GUI.Button(lightningRect, lightningImage, buttonStyle) || Input.GetKeyDown("3"))
        {
            abilitySelected = Ability.Lightning;
        }

        if (GUI.Button(tornadoRect, tornadoImage, buttonStyle) || Input.GetKeyDown("4"))
        {
            abilitySelected = Ability.Tornado;
        }
    }
	
	// Update is called once per frame
	void Update () {
        	
        
	}
}
