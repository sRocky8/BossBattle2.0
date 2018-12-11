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

	public float maxNonDashDistance;
	public float maxAttackDistance;

	public bool attacking;
	public bool stunned;
	public bool inHitstun;

	//Private Variables
	private float distanceToPlayer;

	private bool phaseOne;
	private bool active;

	// Use this for initialization
	void Start () {
		agent = GetComponent<NavMeshAgent> ();

		if (SceneManager.GetSceneByName == "BossPhase 1" || SceneManager.GetSceneByName == "BossPhase 2") {
			active == true;
		}
		else{
			active = false;
		}

		if (SceneManager.GetSceneByName == "BossPhase 1") {
			phaseOne == true;
		}
		if (SceneManager.GetSceneByName == "BossPhase 2") {
			phaseOne == false;
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
