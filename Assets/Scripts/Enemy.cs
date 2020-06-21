using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	public Transform other;

	private Animator anim;
	private CharacterController controller;

	// Use this for initialization
	void Start () {
		transform.Rotate (0, 180, 0);
		controller = GetComponent <CharacterController>();
		anim = gameObject.GetComponentInChildren<Animator>();
		anim.SetBool ("EnemyParam", false);
	}
	
	// Update is called once per frame
	void Update () {
		float dist = Vector3.Distance(other.position, transform.position);
		//print("Distance to other: " + dist);
		if (dist <= 20) {
			anim.SetBool ("EnemyParam", true);
		} else {
			anim.SetBool ("EnemyParam", false);
		}

		
	}
}
