 using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LivesUpdate : MonoBehaviour {

    AudioSource soundPlay;
    public AudioClip loselife;
    public AudioClip gainlife;

	public int lives = 3;
	private Text myText;
	
	// Use this for initialization
	void Start () {
        soundPlay = GameObject.Find("SoundPlay").GetComponent<AudioSource>();
		myText = GetComponent<Text> ();
	}

	public void Gainlife () {
        soundPlay.clip = gainlife;
        soundPlay.Play();
		lives++;
		print (lives.ToString());
		myText.text = lives.ToString ();
	}

	public void LoseLife(){
        soundPlay.clip = loselife;
        soundPlay.Play();
		lives--; 
		print (lives.ToString());
		myText.text = lives.ToString ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
