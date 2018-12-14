using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class Monster : MonoBehaviour {

	//Public Variables
	public Transform playerTransform;
    public GameObject player;
	public GameObject leftArm;
	public GameObject rightArm;

    public float damageFromCriticalHit;
    public float health;

    public float maxNonDashDistance;
	public float maxAttackDistance;
	public float secondsForDash;
	public float secondsForAttack;
	public float dashSpeed;
    public float attackCooldownSeconds;

	//Private Variables
	private float distanceToPlayer;

	private Animator monsterAnimator;

	private NavMeshAgent agent;

    private Scene currentScene;
    private string currentSceneName;

	private bool active;
    private bool attacking;
    private bool inHitstun;
    private bool phaseOne;
    private bool stunned;
    private bool canAttack;

	private bool seesGap;
	private bool seesPlayer;

	private int layerMaskPlayer = 1 << 9;
	private int layerMaskGaps = 1 << 10;

    // Use this for initialization
    void Start () {
		agent = GetComponent<NavMeshAgent> ();
		monsterAnimator = GetComponent<Animator> ();

        currentScene = SceneManager.GetActiveScene();
        currentSceneName = currentScene.name;

		if (currentSceneName == "BossPhase 1" || currentSceneName == "BossPhase 2") {
            active = true;
		}
		else{
			active = false;
		}

		if (currentSceneName == "BossPhase 1" && active == true) {
			phaseOne = true;
			GetComponent<NavMeshAgent> ().speed = 1.0f;
		}
		if (currentSceneName == "BossPhase 2" && active == true) {
			phaseOne = false;
			GetComponent<NavMeshAgent> ().speed = 3.0f;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(player != null)
        {
            distanceToPlayer = Vector3.Distance(player.transform.position, gameObject.transform.position);
//            Debug.Log(string.Format("Distance between player and monster is {0}", distanceToPlayer));
        }

		//Check if monster has direct line of sight to player
		RaycastHit hitPlayer;
		if (Physics.Raycast (transform.position, transform.TransformDirection (Vector3.forward), out hitPlayer, Mathf.Infinity, layerMaskPlayer)) {
			Debug.Log ("Hit Player");
			seesPlayer = true;
		} else {
			seesPlayer = false;
		}

		//Check if there is a gap in the floor
		RaycastHit seeGap;
		if (Physics.Raycast (transform.position, transform.TransformDirection (Vector3.forward), out seeGap, Mathf.Infinity, layerMaskGaps)) {
			Debug.Log ("Hit Gap");
			seesGap = true;
		} else {
			seesGap = false;
		}


	}

	void FixedUpdate(){
		if (distanceToPlayer <= maxNonDashDistance) {
			if (attacking == false && canAttack == true) {
				Vector3 walkTo = playerTransform.transform.position;
				agent.destination = walkTo;
			}
		}
		if (seesPlayer == true && seesGap == false) {
			if (distanceToPlayer > maxNonDashDistance && attacking == false) {
				StartCoroutine (DashCoRoutine ());
			}
			if (distanceToPlayer <= maxAttackDistance && attacking == false) {
				StartCoroutine (AttackCoRoutine ());
			}
		}
	}

	private IEnumerator DashCoRoutine(){
		attacking = true;
		agent.enabled = false;
//		monsterAnimator.Play ("DashAttack");

		yield return new WaitForSeconds (secondsForDash);


		agent.enabled = true;
		attacking = false;
        StartCoroutine(AttackCooldownCoRoutine());
	}

	private IEnumerator AttackCoRoutine(){
		attacking = true;
		agent.enabled = false;
		monsterAnimator.Play ("Attack");
		Debug.Log ("Played AttackCoRoutine");

		yield return new WaitForSeconds (secondsForAttack);

		agent.enabled = true;
		attacking = false;
        StartCoroutine(AttackCooldownCoRoutine());
	}

	private IEnumerator AttackCooldownCoRoutine(){
        canAttack = false;

        yield return new WaitForSeconds(attackCooldownSeconds);

        canAttack = true;
	}
}
