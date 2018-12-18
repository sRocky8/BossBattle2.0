using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutSceneScript : MonoBehaviour {

    //Public Variables
    public float secondsToChangeScene;

    //Private Variables
    private int currentScene;

	// Use this for initialization
	void Start () {
        currentScene = SceneManager.GetActiveScene().buildIndex;
        StartCoroutine(ChangeSceneCoRoutine());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private IEnumerator ChangeSceneCoRoutine()
    {
        yield return new WaitForSeconds(secondsToChangeScene);

        SceneManager.LoadScene(currentScene + 1);
    }
}
