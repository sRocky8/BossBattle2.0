using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switches : MonoBehaviour {

    //Public Variables
    public bool activated;

    //Private Variables

    public void Start()
    {
        activated = false;
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && Input.GetKeyDown(KeyCode.U))
        {
            activated = true;
        }
    }
}