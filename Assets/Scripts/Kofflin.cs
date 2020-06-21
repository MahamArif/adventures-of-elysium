using UnityEngine;
using System.Collections;

public class Kofflin : MonoBehaviour {


	
	void OnTriggerEnter (Collider collider){
		
		LivesUpdate life = GameObject.Find("Lives").GetComponent<LivesUpdate>();
		life.LoseLife();

	}
	
	
	
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
