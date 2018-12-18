using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    //Public Variables
    public GameObject player;

    public float timeForAttackStartFrames;
    public float timeForAttackEndFrames;
    public float timeForSwingStartFrames;
    public float timeForSwingEndFrames;

    //Private Variables
    private bool inMotion;
    
	// Use this for initialization
	void Start () {
        gameObject.GetComponent<BoxCollider>().enabled = false;
        inMotion = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (player.GetComponent<PlayerController>().inHitStun == true)
        {
            StopAllCoroutines();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (inMotion == false && player.GetComponent<PlayerController>().inHitStun == false)
            {
                if (player.GetComponent<PlayerController>().moveableScene == true)
                {
                    if (player.GetComponent<PlayerController>().closeEnoughToAttack == true)
                    {
                        StartCoroutine(MonsterActivateColliderCoRoutine());
                    }
                    if (player.GetComponent<PlayerController>().closeEnoughToAttack == false)
                    {
                        StartCoroutine(VatActivateColliderCoRoutine());
                    }
                }
            }
        }
    }

    private IEnumerator MonsterActivateColliderCoRoutine()
    {
        gameObject.tag = "WeaponMonster";
        inMotion = true;

        yield return new WaitForSeconds((1.0f/60.0f) * timeForAttackStartFrames);

        gameObject.GetComponent<BoxCollider>().enabled = true;

        yield return new WaitForSeconds((1.0f/60.0f) * timeForAttackEndFrames);
        
        gameObject.GetComponent<BoxCollider>().enabled = false;
        inMotion = false;
        gameObject.tag = "Weapon";
    }

    private IEnumerator VatActivateColliderCoRoutine()
    {
        gameObject.tag = "WeaponVat";
        inMotion = true;

        yield return new WaitForSeconds((1.0f/60.0f) * timeForSwingStartFrames);

        gameObject.GetComponent<BoxCollider>().enabled = true;

        yield return new WaitForSeconds((1.0f/60.0f) * timeForSwingEndFrames);

        gameObject.GetComponent<BoxCollider>().enabled = false;
        inMotion = false;
        gameObject.tag = "Weapon";
    }
}
