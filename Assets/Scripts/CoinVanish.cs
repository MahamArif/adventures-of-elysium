using UnityEngine;
using System.Collections;

public class CoinVanish : MonoBehaviour {
    AudioSource soundPlay;
    public AudioClip gainCoin;

	int scorepoint = 5;

	void OnTriggerEnter (Collider collider){

        soundPlay.clip = gainCoin;
        soundPlay.Play();

		ScoreUpdate scr = GameObject.Find("Score").GetComponent<ScoreUpdate>();
		scr.Score(scorepoint);
        print("before");
		Destroy (gameObject);
        print("after");
	}

	// Use this for initialization
	void Start () {
        soundPlay = GameObject.Find("SoundPlay").GetComponent<AudioSource>();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
