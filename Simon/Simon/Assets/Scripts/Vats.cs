using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vats : MonoBehaviour {

    //Public Variables
    public GameObject switchOne;
    public GameObject switchTwo;
    public Vector3 maxHeight;
    public Vector3 minHeight;
    public float lerpPosition;
	public float translationValue;

    //Private Variables
    private bool switchOneActive;
    private bool switchTwoActive;
    
	// Use this for initialization
	void Start () {
        switchOneActive = false;
        switchTwoActive = false;
	}
	
	// Update is called once per frame
	void Update () {
        switchOneActive = switchOne.GetComponent<Switches>().activated;
        switchTwoActive = switchTwo.GetComponent<Switches>().activated;

        if (switchOneActive == true && switchTwoActive == true)
        {
			if(lerpPosition < 1.0f){
            	transform.position = Vector3.Lerp(maxHeight, minHeight, lerpPosition);
				lerpPosition += 1.0f / translationValue;
			}
        }
        else
        {
			if (lerpPosition > 0.0f) {
				transform.position = Vector3.Lerp (maxHeight, minHeight, lerpPosition);
				lerpPosition -= 1.0f / translationValue;
			}
        }
    }
}
