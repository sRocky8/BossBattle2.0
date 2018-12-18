using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ActivateFinalCutscene : MonoBehaviour {

    //Public Variables
    public GameObject switchOne;

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Monster" && switchOne.GetComponent<Switches>().activated == true)
        {
            SceneManager.LoadScene(6);
        }
    }
}
