using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	//Public Variables
	public Animator playerAnimation;
	public float walkSpeed;
	public float rotationSpeed;
	public float health;
	public float healthGainedFromPickup;
    public float deathTime;

    public float sphereCastRadius;
    public float sphereCastDistance;

    public GameObject weapon;

    public GameObject healthCanvas;

    public GameObject bloodBagHealth5;
    public GameObject bloodBagHealth4;
    public GameObject bloodBagHealth3;
    public GameObject bloodBagHealth2;
    public GameObject bloodBagHealth1;
    public GameObject bloodBagHealth0;

    [HideInInspector] public bool inHitStun;
    [HideInInspector] public bool closeEnoughToAttack;
    [HideInInspector] public bool moveableScene;

    //Private Variables
    private float maxHealth;
	private bool attackState;

    private int currentScene;

    private GameObject playerCamera;
    private Sprite currentBloodBag;

    private int monsterLayerMask = 1 << 11;

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
            healthCanvas.SetActive(false);
            moveableScene = false;

            transform.position = new Vector3(-3.0f, 2.0f, 24.0f);
            transform.rotation = Quaternion.identity;
        }
        else if ((currentScene == 1 || currentScene == 3) || currentScene == 5)
        {
            playerCamera.SetActive(true);
            healthCanvas.SetActive(true);
            moveableScene = true;
        }

        playerAnimation.SetFloat("health", health);

        RaycastHit hitMonster;

        if (Physics.SphereCast(gameObject.transform.position, sphereCastRadius, transform.forward, out hitMonster, sphereCastDistance , monsterLayerMask))
        {
            closeEnoughToAttack = true;
//            Debug.Log("Hit monster");
        }
        else
        {
            closeEnoughToAttack = false;
        }

//        if (Input.GetKeyDown(KeyCode.Equals))
//        {
//            currentScene += 1;
//            SceneManager.LoadScene(currentScene);
//        }

		if (health > maxHealth) {
			health = maxHealth;
		}

        if (health == 5.0f){
            bloodBagHealth5.SetActive(true);
        }
        else if (health == 4.0f){
            bloodBagHealth4.SetActive(true);
            bloodBagHealth5.SetActive(false);
        }
        else if (health == 3.0f){
            bloodBagHealth3.SetActive(true);
            bloodBagHealth4.SetActive(false);
        }
        else if (health == 2.0f){
            bloodBagHealth2.SetActive(true);
            bloodBagHealth3.SetActive(false);
        }
        else if (health == 1.0f){
            bloodBagHealth1.SetActive(true);
            bloodBagHealth2.SetActive(false);
        }
        else if (health <= 0.0f){
            bloodBagHealth0.SetActive(true);
            bloodBagHealth1.SetActive(false);
        }



        if (health > 0.0f){
            if(moveableScene == true) {
                if (inHitStun == false)
                {
                    if (attackState == false && Input.GetKey(KeyCode.W))
                    {
                        playerAnimation.Play("Walkv1");
                    }
                    else if (attackState == false && Input.GetKey(KeyCode.S))
                    {
                        playerAnimation.Play("Walkv1 Backwards");
                    }
                    else
                    {
                        if (attackState == false)
                        {
                            playerAnimation.Play("Idle");
                        }
                    }

                    if (attackState == false && Input.GetKeyDown(KeyCode.Space) && closeEnoughToAttack == true)
                    {
                        StartCoroutine(AttackMonsterCoRoutine());
                    }
                    if (attackState == false && Input.GetKeyDown(KeyCode.Space) && closeEnoughToAttack == false)
                    {
                        StartCoroutine(SwingCoRoutine());
                    }
                }
            }
		}
        else
        {
            StartCoroutine(DeathCoRoutine());
        }
	}

	void FixedUpdate(){
		if (health > 0.0f) {
            if (moveableScene == true)
            {
                if (attackState == false)
                {
                    if (inHitStun == false)
                    {
                        float moveHorizontal = Input.GetAxis("Horizontal");
                        float moveVertical = Input.GetAxis("Vertical");

                        transform.Translate(0.0f, 0.0f, moveVertical * walkSpeed);
                        transform.Rotate(0.0f, moveHorizontal * rotationSpeed, 0.0f);
                    }
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

        if(other.tag == "MonsterArm")
        {

            health -= 1.0f;
            StartCoroutine(HitstunAndInvincibilityCoRoutine());
        }
	}

	private IEnumerator AttackMonsterCoRoutine(){
		attackState = true;
		playerAnimation.Play ("Attackv2");

		yield return new WaitForSeconds (139.0f/60.0f);

		attackState = false;
	}

    private IEnumerator SwingCoRoutine()
    {
        attackState = true;
        playerAnimation.Play("Swing");

        yield return new WaitForSeconds(1.0f);

        attackState = false;
    }

    private IEnumerator HitstunAndInvincibilityCoRoutine()
    {
        inHitStun = true;
        playerAnimation.Play("Stun");
        
        yield return new WaitForSeconds(2.0f);

        inHitStun = false;
    }

    private IEnumerator DeathCoRoutine()
    {
        playerAnimation.Play("Death");

        yield return new WaitForSeconds(deathTime);

        SceneManager.LoadScene(0);
    }
}
