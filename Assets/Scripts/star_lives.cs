using UnityEngine;
using System.Collections;

public class star_lives : MonoBehaviour {


	void OnTriggerEnter (Collider collider){
		
		    Destroy (gameObject);
			LivesUpdate life = GameObject.Find ("Lives").GetComponent<LivesUpdate> ();
			life.Gainlife ();
			

	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
