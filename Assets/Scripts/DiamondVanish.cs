using UnityEngine;
using System.Collections;

public class DiamondVanish : MonoBehaviour {

	int scorepoint = 10;

    AudioSource soundPlay;
    public AudioClip diamond;
	
	void OnTriggerEnter (Collider collider){

        soundPlay.clip = diamond;
        soundPlay.Play();

		ScoreUpdate scr = GameObject.Find("Score").GetComponent<ScoreUpdate>();
		scr.Score(scorepoint);
		Destroy (gameObject);
		TUpdate treasure = GameObject.Find("Tcount").GetComponent<TUpdate>();
		treasure.TCount ();
	}

	// Use this for initialization
	void Start () {
        soundPlay = GameObject.Find("SoundPlay").GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
