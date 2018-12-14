using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switches : MonoBehaviour {

    //Public Variables
    [HideInInspector]public bool activated;
    public Vector3 minHeight;
    public Vector3 maxHeight;
    public float lerpPosition;
    public float translationValue; 

    //Private Variables

    public void Start()
    {
        activated = false;
    }

    private void Update()
    {
        if (activated == true)
        {
            if (lerpPosition < 1.0f)
            {
                transform.position = Vector3.Lerp(maxHeight, minHeight, lerpPosition);
                lerpPosition += 1.0f / translationValue;
            }
        }
        else
        {
            if (lerpPosition > 0.0f)
            {
                transform.position = Vector3.Lerp(maxHeight, minHeight, lerpPosition);
                lerpPosition -= 1.0f / translationValue;
            }
        }
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && Input.GetKeyDown(KeyCode.U))
        {
            activated = true;
        }
    }
}