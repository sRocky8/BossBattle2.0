using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits : MonoBehaviour {

    //Public Variables
    public float timeUntilGameShutsDown;
    
    //Private Variables

    
    // Use this for initialization
    void Start() {
        Destroy(GameObject.Find("PlayerV1"));
    }

    // Update is called once per frame
    void Update() {

    }

    private IEnumerator EndGameCoRoutine()
    {
        yield return new WaitForSeconds(timeUntilGameShutsDown);

        Application.Quit();
    }
}
