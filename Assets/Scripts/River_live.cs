using UnityEngine;
using System.Collections;
using System;

public class River_live : MonoBehaviour {

	public GameObject characterScript;

	public GameObject obj2;
	Player_Info info1;
	Animator anim1;                          // Reference to the animator component.

	Transform character;

	float restartDelay = 5f;         // Time to wait before restarting the level
	float restartTimer;                     // Timer to count up to restarting the level

	AudioSource soundPlay;
	public AudioClip gameOver;


	void Start ()
	{
		//info1 = GameObject.Find("Player_Info").GetComponent<Player_Info>();
		// Set up the reference.
		anim1 = GetComponent <Animator> ();
		soundPlay = GameObject.Find("SoundPlay").GetComponent<AudioSource>();

		character = GameObject.Find ("Steampunk_KidExtra_animation4").GetComponent<Transform>();
	} 

	void OnTriggerEnter (Collider collider){

		characterScript.GetComponent<playerTestRun>().enabled = false;

		float no = characterScript.GetComponent<Transform> ().position.x;
		float no1 = characterScript.GetComponent<Transform> ().position.z;

		characterScript.GetComponent<Transform>().position = new Vector3 (no, -42, no1);



		soundPlay.clip = gameOver;
		soundPlay.Play();

		restartTimer += Time.deltaTime;
		
		// .. if it reaches the restart delay...
		if(restartTimer >= restartDelay)
		{

			//anim1.SetTrigger ("GameOver");

			MyDelay(2);
			// .. then reload the currently loaded level.
			//Application.LoadLevel(Application.loadedLevel);
			obj2.active = true;
		}

	}
	// Use this for initialization
	
	// Update is called once per frame
	void Update () {
	
	}

	public void MyDelay(int seconds)
	{
		DateTime dt = DateTime.Now + TimeSpan.FromSeconds(seconds);
		
		do { } while (DateTime.Now < dt);
	}
}
