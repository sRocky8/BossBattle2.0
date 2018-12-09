using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour {

	//Public Variables
	public NavMeshAgent agent;
	public Transform playerTransform;

	//Private Variables


	// Use this for initialization
	void Start () {
		agent = GetComponent<NavMeshAgent> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate(){
		Vector3 walkTo = playerTransform.transform.position;
		agent.destination = walkTo;
	}
}
