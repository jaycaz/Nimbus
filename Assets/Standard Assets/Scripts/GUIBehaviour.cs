using UnityEngine;
using System.Collections;

[System.Serializable]
public class GUIBehaviour : MonoBehaviour {

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
	
	// Update is called once per frame
	void Update () {
	
        // Cycle through abilities using number keys
        if(Input.GetKeyDown("1"))
        {
            abilitySelected = Ability.Rain;
        }
        if (Input.GetKeyDown("2"))
        {
            abilitySelected = Ability.Downpour;
        }
        if (Input.GetKeyDown("3"))
        {
            abilitySelected = Ability.Lightning;
        }
        if (Input.GetKeyDown("4"))
        {
            abilitySelected = Ability.Tornado;
        }
	}
}
