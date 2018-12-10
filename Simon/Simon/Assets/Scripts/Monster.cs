using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour {

	//Public Variables
	public NavMeshAgent agent;
	public Transform playerTransform;
    public GameObject player;

	//Private Variables


	// Use this for initialization
	void Start () {
		agent = GetComponent<NavMeshAgent> ();
	}
	
	// Update is called once per frame
	void Update () {
		if(player != null)
        {
            float distanceToPlayer = Vector3.Distance(player.transform.position, gameObject.transform.position);
            Debug.Log(string.Format("Distance between player and monster is {0}", distanceToPlayer));
        }
	}

	void FixedUpdate(){
		Vector3 walkTo = playerTransform.transform.position;
		agent.destination = walkTo;
	}
}
