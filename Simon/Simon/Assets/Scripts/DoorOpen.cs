using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour {

    //Public Variables
    public GameObject switchOne;
    public Quaternion startRotation;
    public Quaternion endRotation;
    public float lerpPosition;
    public float rotationValue;

    //Private Variables
    private bool switchActive;
	
	// Update is called once per frame
	void Update () {
        switchActive = switchOne.GetComponent<Switches>().activated;

        if(switchActive == true)
        {
            if (lerpPosition < 1.0f)
            {
                transform.rotation = Quaternion.Lerp(startRotation, endRotation, lerpPosition);
                lerpPosition += 1.0f / rotationValue;
            }
        }
	}
}
