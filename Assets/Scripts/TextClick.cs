using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class TextClick : MonoBehaviour {

    public GameObject gameObject;
    public GameObject colorFor;
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void onClick()
    {
        //ScoreManager score = GameObject.Find("GameObject").GetComponent<ScoreManager>();
        //score.GetScores();
        PlayerNamesManager script1 = GameObject.Find("PlayerManager").GetComponent<PlayerNamesManager>();
        script1.changeColor();
        colorFor.GetComponent<Image>().color = new Color32(251, 160, 78, 255);
        string text1 = gameObject.GetComponent<Text>().text;
        
        print(text1 + colorFor.name);
       
        Player_Info script2 = GameObject.Find("Player_Info").GetComponent<Player_Info>();
        script2.setName(text1);
        script2.setID(int.Parse(colorFor.name));
        Double score2 = script1.SingleSelectWhere("ScoreCard", "*", "Player_ID", colorFor.name);
        script2.setScore(score2);

        int levelID = script1.SingleSelectLevelID("ScoreCard", "*", "Player_ID", colorFor.name);
        script2.setProgress(levelID);

        //script1.CloseDB();

        script2.printInfo();
        //score.writeData("Maham");
    }
}
