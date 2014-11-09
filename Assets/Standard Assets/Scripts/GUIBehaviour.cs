﻿using UnityEngine;
using System;
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

    public delegate void RainHandler(object sender, EventArgs e);
    public delegate void DownpourHandler(object sender, EventArgs e);
    public delegate void LightningHandler(object sender, EventArgs e);
    public delegate void TornadoHandler(object sender, EventArgs e);

    public event RainHandler TriggerRain;
    public event DownpourHandler TriggerDownpour;
    public event LightningHandler TriggerLightning;
    public event TornadoHandler TriggerTornado;
    
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

        CursorBehaviour c = GameObject.FindObjectOfType<CursorBehaviour>();
        c.CursorLeftClick += OnCursorLeftClick;
	}

    private void OnTriggerRain()
    {
        abilitySelected = Ability.Rain;

        RainHandler handler = TriggerRain;
        if(handler != null)
        {
            handler(this, new EventArgs());
        }
    }

    private void OnTriggerDownpour()
    {
        abilitySelected = Ability.Downpour;

        DownpourHandler handler = TriggerDownpour;
        if (handler != null)
        {
            handler(this, new EventArgs());
        }
    }

    private void OnTriggerLightning()
    {
        abilitySelected = Ability.Lightning;

        LightningHandler handler = TriggerLightning;
        if (handler != null)
        {
            handler(this, new EventArgs());
        }
    }

    private void OnTriggerTornado()
    {
        abilitySelected = Ability.Tornado;

        TornadoHandler handler = TriggerTornado;
        if (handler != null)
        {
            handler(this, new EventArgs());
        }
    }

    private void OnCursorLeftClick(object sender, CursorClickEventArgs e)
    {
        if(rainRect.Contains(e.position))
        {
            OnTriggerRain();
        }
        else if (downpourRect.Contains(e.position))
        {
            OnTriggerDownpour();
        }
        else if (lightningRect.Contains(e.position))
        {
            OnTriggerLightning();
        }
        else if (tornadoRect.Contains(e.position))
        {
            OnTriggerTornado();
        }
    }

    void OnGUI()
    {
        GUI.depth = 0;

        // Start sun animation
        sunRect = new Rect(Screen.width - 100f, Screen.height * (1f/2f), 100, 100);
        Vector3 newAnimPos = Camera.main.ScreenToWorldPoint(new Vector3(sunRect.xMin, sunRect.yMin, 6));
        Transform sunAnimTransform = transform.FindChild("animtest");

        sunAnimTransform.position = newAnimPos;
        //GUI.DrawTexture(sunRect, sunTexture);

        // Calculate placement of buttons
        GUIStyle buttonStyle = new GUIStyle();
        
        rainRect = new Rect(sunRect.xMin - 125, sunRect.yMin - 215f, rainTexture.width, rainTexture.height);
        downpourRect = new Rect(rainRect.xMin - 80f, rainRect.yMax - 45f, downpourTexture.width, downpourTexture.height);
        lightningRect = new Rect(downpourRect.xMin, downpourRect.yMax + 20f, lightningTexture.width, lightningTexture.height);
        tornadoRect = new Rect(rainRect.xMin, lightningRect.yMax - 50f, tornadoTexture.width, tornadoTexture.height);

        Texture rainImage = abilitySelected == Ability.Rain ? rainActiveTexture : rainTexture;
        Texture downpourImage = abilitySelected == Ability.Downpour ? downpourActiveTexture : downpourTexture;
        Texture lightningImage = abilitySelected == Ability.Lightning ? lightningActiveTexture : lightningTexture;
        Texture tornadoImage = abilitySelected == Ability.Tornado ? tornadoActiveTexture : tornadoTexture;

        // Cycle through abilities using number keys
        if (GUI.Button(rainRect, rainImage, buttonStyle) || Input.GetKeyDown("1"))
        {
            OnTriggerRain();
        }

        if (GUI.Button(downpourRect, downpourImage, buttonStyle) || Input.GetKeyDown("2"))
        {
            OnTriggerDownpour();
        }

        if (GUI.Button(lightningRect, lightningImage, buttonStyle) || Input.GetKeyDown("3"))
        {
            OnTriggerLightning();
        }

        if (GUI.Button(tornadoRect, tornadoImage, buttonStyle) || Input.GetKeyDown("4"))
        {
            OnTriggerTornado();
        }
    }
	
	// Update is called once per frame
	void Update () {
        	
        
	}
}
