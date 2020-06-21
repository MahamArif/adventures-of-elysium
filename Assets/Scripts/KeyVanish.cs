using UnityEngine;
using System.Collections;

public class KeyVanish : MonoBehaviour {

    AudioSource soundPlay;
    public AudioClip gainKey;
	
	void OnTriggerEnter (Collider collider){

        soundPlay.clip = gainKey;
        soundPlay.Play();

		Destroy (gameObject);
		KeyUpdate key = GameObject.Find("Key").GetComponent<KeyUpdate>();
		key.KeyCount ();
	}



	// Use this for initialization
	void Start () {
        soundPlay = GameObject.Find("SoundPlay").GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
