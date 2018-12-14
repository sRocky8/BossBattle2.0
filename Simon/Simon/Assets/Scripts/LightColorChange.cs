using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightColorChange : MonoBehaviour {

    //Public Variables
    public GameObject switchOne;

    //Private Variables
    private bool switchActive;
    private Light lt;

    // Use this for initialization
    void Start()
    {
        lt = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        switchActive = switchOne.GetComponent<Switches>().activated;

        if (switchActive == true)
        {
            lt.color = Color.red;
        }
    }
}
