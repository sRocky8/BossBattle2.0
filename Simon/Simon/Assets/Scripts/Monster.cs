using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class Monster : MonoBehaviour {

	//Public Variables
	public NavMeshAgent agent;
	public Transform playerTransform;
    public GameObject player;

    public float damageFromCriticalHit;
    public float health;

    public float maxNonDashDistance;
	public float maxAttackDistance;

	//Private Variables
	private float distanceToPlayer;

    private Scene currentScene;
    private string currentSceneName;

	private bool active;
    private bool attacking;
    private bool inHitstun;
    private bool phaseOne;
    private bool stunned;

    // Use this for initialization
    void Start () {
		agent = GetComponent<NavMeshAgent> ();

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
		}
		if (currentSceneName == "BossPhase 2" && active == true) {
			phaseOne = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(player != null)
        {
            distanceToPlayer = Vector3.Distance(player.transform.position, gameObject.transform.position);
            Debug.Log(string.Format("Distance between player and monster is {0}", distanceToPlayer));
        }
	}

	void FixedUpdate(){
		if (distanceToPlayer <= maxNonDashDistance) {
			Vector3 walkTo = playerTransform.transform.position;
			agent.destination = walkTo;
		}
	}
}
