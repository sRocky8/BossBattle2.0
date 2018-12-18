using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartScreen : MonoBehaviour {

    //Public Variables
    public Button newGameButton;
//    public GameObject fade;
//    public GameObject music;
    public float timeToWait;
//    public float volumeDecrease;
    public bool buttonClicked;

    //Private Variables
//    private Animator fadeOut;
//    private AudioSource musicAudio;

    void Start()
    {
//        fadeOut = fade.GetComponent<Animator>();
//        musicAudio = music.GetComponent<AudioSource>();
        buttonClicked = false;
//        volumeDecrease = (1.0f / 300.0f);
        Destroy(GameObject.Find("PlayerV1"));
    }

    void Update()
    {
        if (buttonClicked == true)
        {
//            musicAudio.volume -= volumeDecrease;
        }
    }

    public void ButtonClicked(string newGame)
    {
        buttonClicked = true;
//        fade.SetActive(true);
        StartCoroutine(TransitionCoRoutine());
    }

    private IEnumerator TransitionCoRoutine()
    {
//        fadeOut.Play("FadeOut");

        yield return new WaitForSeconds(timeToWait);

        SceneManager.LoadScene(1);
    }
}
