using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class ScoreUpdate : MonoBehaviour {

	public Double score = 0;
    Player_Info script2;
    
	private Text myText;

	// Use this for initialization
	void Start () {
		myText = GetComponent<Text> ();
        script2 = GameObject.Find("Player_Info").GetComponent<Player_Info>();
        score = script2.getScore();
        myText.text = score.ToString();
	}

	public void Score(int points){
		score += points;
        script2.setScore(score);
		myText.text = score.ToString ();
	}

	// Update is called once per frame
	void Update () {
	
	}
}
