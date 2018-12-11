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

    private Scene currentScene;

	// Use this for initialization
	void Start () {

        currentScene = SceneManager.GetActiveScene();

		maxHealth = health;
		inHitStun = false;
		attackState = false;
		playerAnimation = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {

        DontDestroyOnLoad(this.gameObject);

        if (Input.GetKeyDown(KeyCode.Equals))
        {
            SceneManager.LoadScene(currentScene.buildIndex + 1);
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
		playerAnimation.Play ("Attackv1");

		yield return new WaitForSeconds (139.0f/60.0f);

		attackState = false;
	}
}
