using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainVat : MonoBehaviour {

    //Public Variables
    public GameObject switchOne;
    public Vector3 startHeight;
    public Vector3 endHeight;
    public float lerpPosition;
    public float translationValue;
    public float secondsToChangeScene;


    //Private Variables
    private Scene currentScene;
    private string currentSceneName;
    
    private bool switchActive;

    private AudioSource vatHit;
    
	void Start () {
        currentScene = SceneManager.GetActiveScene();
        currentSceneName = currentScene.name;

        vatHit = GetComponent<AudioSource>();

        if (currentSceneName == "Cutscene 1")
        {
            lerpPosition = 1.0f;
        }
    }
	
	void Update () {

        if (switchOne != null)
        {
            switchActive = switchOne.GetComponent<Switches>().activated;
            if (currentSceneName == "BossPhase 2")
            {
                if (switchActive == true && lerpPosition < 1.0f)
                {
                    transform.position = Vector3.Lerp(startHeight, endHeight, lerpPosition);
                    lerpPosition += 1.0f / translationValue;
                }
            }
        }

        if (currentSceneName == "Cutscene 1")
        {
            if(lerpPosition > 0.0f)
            {
                transform.position = Vector3.Lerp(startHeight, endHeight, lerpPosition);
                lerpPosition -= 1.0f / translationValue;
            }
        }


    }

    private void OnTriggerEnter(Collider other)
    {
        if (currentSceneName == "BossFight") {
            if (other.tag == "Weapon")
            {
                StartCoroutine(TransitionCoRoutine());
            }
        }
    }

    private IEnumerator TransitionCoRoutine()
    {
        vatHit.Play();

        yield return new WaitForSeconds(secondsToChangeScene);

        SceneManager.LoadScene(currentScene.buildIndex + 1);
    }

    private IEnumerator CutsceneCoRoutine()
    {
        yield return new WaitForSeconds(secondsToChangeScene);

        SceneManager.LoadScene(currentScene.buildIndex + 1);
    }
}
