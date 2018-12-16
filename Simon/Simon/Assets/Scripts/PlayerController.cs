using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

	//Public Variables
	public Animator playerAnimation;
	public float walkSpeed;
	public float rotationSpeed;
	public float health;
	public float healthGainedFromPickup;


	//Private Variables
	private float maxHealth;
	private bool inHitStun;
	private bool attackState;
    private bool moveableScene;

    private int currentScene;

    private GameObject playerCamera;

	// Use this for initialization
	void Start () {

//		currentScene = SceneManager.GetActiveScene().buildIndex;

		maxHealth = health;
		inHitStun = false;
		attackState = false;
		playerAnimation = GetComponent<Animator> ();
        playerCamera = gameObject.transform.Find("Main Camera").gameObject;
    }
	
	// Update is called once per frame
	void Update () {

        currentScene = SceneManager.GetActiveScene().buildIndex;

        if (currentScene != 7)
        {
            DontDestroyOnLoad(this.gameObject);
        }

        if ((currentScene == 2 || currentScene == 4) || (currentScene == 6 || currentScene == 7))
        {
            playerCamera.SetActive(false);
        }
        else if ((currentScene == 1 || currentScene == 3) || currentScene == 5)
        {
            playerCamera.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Equals))
        {
//            currentScene += 1;
            SceneManager.LoadScene(currentScene + 1);
        }

		if (health > maxHealth) {
			health = maxHealth;
		}

		if (health > 0.0f){
			if (inHitStun == false) {
				if (attackState == false && Input.GetKey (KeyCode.W)) {
					playerAnimation.Play ("Walkv1");
				} else if (attackState == false && Input.GetKey (KeyCode.S)) {
					playerAnimation.Play ("Walkv1 Backwards");
				} else {
					if (attackState == false) {
						playerAnimation.Play ("Idle");
					}
				}

				if (attackState == false && Input.GetKeyDown(KeyCode.Space)) {
					StartCoroutine (AttackMonsterCoRoutine ());
				}
			}
		}
	}

	void FixedUpdate(){
		if (health > 0.0f) {
			if (attackState == false) {
				if (inHitStun == false) {
					float moveHorizontal = Input.GetAxis ("Horizontal");
					float moveVertical = Input.GetAxis ("Vertical");

					transform.Translate (0.0f, 0.0f, moveVertical * walkSpeed);
					transform.Rotate (0.0f, moveHorizontal * rotationSpeed, 0.0f);
				}
			}
		}
	}

	void OnTriggerStay (Collider other){
		if (other.tag == "Health" && Input.GetKeyDown(KeyCode.U)) {
			if (health < maxHealth && health > 0.0f) {
				health += healthGainedFromPickup;

				//For Testing
				other.gameObject.SetActive (false);
			}
		}
	}

	private IEnumerator AttackMonsterCoRoutine(){
		attackState = true;
		playerAnimation.Play ("Attackv2");

		yield return new WaitForSeconds (139.0f/60.0f);

		attackState = false;
	}
}
