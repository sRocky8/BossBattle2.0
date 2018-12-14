using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour {

    //Public Variables
    public GameObject switchOne;
    public float maxTime;
    public float rotationValue;

    //Private Variables
    private bool switchActive;
    private float timeCount;

    private void Start()
    {
        timeCount = 0.0f;
    }

    // Update is called once per frame
    void Update () {
        switchActive = switchOne.GetComponent<Switches>().activated;

        if(switchActive == true)
        {
            if(timeCount < maxTime){
                timeCount += (Time.deltaTime);
                transform.Rotate(-Vector3.up * Time.deltaTime * rotationValue);
            }
        }
	}
}
