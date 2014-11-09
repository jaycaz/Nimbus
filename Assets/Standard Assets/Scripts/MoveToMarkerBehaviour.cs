using UnityEngine;
using System.Collections;
using RTS;

public class MoveToMarkerBehaviour : MonoBehaviour {

    public bool isAnimating;
    public float animateTime;

	// Use this for initialization
	void Start () {
        animateTime = 0f;
        isAnimating = true;
	}

    public void animate()
    {
        this.gameObject.SetActive(true);
        isAnimating = true;
        Debug.Log("Successfully set marker to active!");
    }
	
	// Update is called once per frame
	void Update () {
	    
        if(isAnimating)
        {
            animateTime += Time.deltaTime;

            if(animateTime > ResourceManager.MoveToMarkerDuration)
            {
                isAnimating = false;
                animateTime = 0f;
                this.gameObject.SetActive(false);
            }
        }
	}
}
