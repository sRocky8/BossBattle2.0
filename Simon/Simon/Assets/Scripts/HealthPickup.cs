using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour {

	//Public Variables
	public Vector3 maxHeight;
	public Vector3 minHeight;

	[Range(0.0f, 1.0f)]
	public float lerpPosition;

	public float translationValue;
	public bool maxTrueMinFalse;

	public float duration;
	public float maxY;
	public float minY;

	//Private Variables
	private float startTime;
	private float xPosition;
	private float zPosition;

	// Use this for initialization
	void Start () {
		xPosition = gameObject.GetComponent<Transform> ().position.x;
		zPosition = gameObject.GetComponent<Transform> ().position.z;
		startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate (-Vector3.up);

		float timePassed = (Time.time - startTime) / duration;

		if (maxTrueMinFalse == false) {
			
			transform.position = new Vector3 (xPosition, Mathf.SmoothStep(minY, maxY, timePassed), zPosition);


//			transform.position = Vector3.Lerp(maxHeight, minHeight, lerpPosition);
//			lerpPosition -= 1.0f / translationValue;
		} //else {
		if (maxTrueMinFalse == true) {
//			float timePassed = (Time.time - startTime) / duration;
			transform.position = new Vector3 (xPosition, Mathf.SmoothStep(maxY, minY, timePassed), zPosition);

//			transform.position = Vector3.Lerp (maxHeight, minHeight, lerpPosition);
//			lerpPosition += 1.0f / translationValue;
		}

		if (gameObject.GetComponent<Transform> ().position.y <= minY) {
			maxTrueMinFalse = false;
		}
		if (gameObject.GetComponent<Transform> ().position.y >= maxY) {
			maxTrueMinFalse = true;
		}






//		if (lerpPosition >= 1.0f){
//			maxTrueMinFalse = true;
//		}
//		if (lerpPosition <= 0.0f){
//			maxTrueMinFalse = false;
//		}
	}
//	private IEnumerator MoveUpCoRoutine(){

//	}
//	private IEnumerator MoveDownCoRoutine(){

//	}
}
