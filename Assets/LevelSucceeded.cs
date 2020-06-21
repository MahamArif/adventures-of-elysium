using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class LevelSucceeded : MonoBehaviour {

    AudioSource soundPlay;
    public AudioClip levelSucceededsound;

    private string addScoreURL = "http://dadarentacar.com/addScore2.php?";
    private string checkURL = "http://dadarentacar.com/CheckQuery2.php?";
    private string updateURL = "http://dadarentacar.com/UpdateScore1.php?";


    Player_Info info;

    int buttonPressed = 0;

    public GameObject levelsucceed;

    public GameObject scoreText;

    public GameObject star1;
    public GameObject star2;
    public GameObject star3;

    public GameObject characterScript;

    LevelBackButtonClick obj;

    int id1;
    string name1;

    int levelProgress;

    Double score2dis;

    void OnTriggerEnter(Collider collider)
    {
        if(collider.tag=="Player"){
            soundPlay.clip = levelSucceededsound;
            soundPlay.Play();

			info.setStatus(false);

            characterScript.GetComponent<playerTestRun>().enabled = false;

            id1 = info.getID();
            name1 = info.getName();

            levelProgress = info.getProgress();

            score2dis =  info.getScore();
            scoreText.GetComponent<Text>().text = score2dis.ToString();

            

            print(name1 + id1 + score2dis);

            obj.OpenDB("Score.sqlite");
            obj.updateScore(id1.ToString(), score2dis.ToString(), levelProgress.ToString());


            levelsucceed.active = true;

            

            if (score2dis >= 100 && score2dis <= 200)
            {
                star1.active = true;
            }
            if (score2dis >= 300 && score2dis <= 400)
            {
                star1.active = true;
                star2.active = true;
            }
            if (score2dis >= 500)
            {
                star1.active = true;
                star2.active = true;
                star3.active = true;
            }

        }
 
    }

	// Use this for initialization
	void Start () {
        info = GameObject.Find("Player_Info").GetComponent<Player_Info>();
        obj = GameObject.Find("BackButtonClick").GetComponent<LevelBackButtonClick>();
        soundPlay = GameObject.Find("SoundPlay").GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ReloadLevel()
    {
        buttonPressed = 1;
        StartCoroutine(checkScore(id1, name1, score2dis));
        //Application.LoadLevel(Application.loadedLevel);
    }

    public void GotoLevelSelect()
    {
        buttonPressed = 2;
        StartCoroutine(checkScore(id1, name1, score2dis));
        //Application.LoadLevel("Level Select");
    }

    public void GotoNextLevel()
    {
        buttonPressed = 3;
        StartCoroutine(checkScore(id1, name1, score2dis));
        //Application.LoadLevel("level2new");
    }


    IEnumerator postScores1(int id, string name, double score)
    {
        string secret_key = "12345";
        string postUrl = addScoreURL + "ID=" + id + "&Name=" + WWW.EscapeURL(name) + "&HScore=" + score + "&Secret=" + WWW.EscapeURL(secret_key);
        WWW hs_post = new WWW(postUrl);
        yield return hs_post;

        if (hs_post.error != null)
        {
            print(hs_post.error);
        }
        else
        {
            print(hs_post.text);

            if (buttonPressed == 1)
            {
                Application.LoadLevel(Application.loadedLevel);
            }
            else if (buttonPressed == 2)
            {
                Application.LoadLevel("Level Select");
            }
            else if (buttonPressed == 3)
            {
                Application.LoadLevel("Level2New");
            }
        }
    }

    IEnumerator updateScore1(int id, string name, double score)
    {
        string secret_key = "12345";
		string postUrl = updateURL + "ID=" + id + "&Name=" + WWW.EscapeURL(name) + "&HScore=" + score + "&Secret=" + WWW.EscapeURL(secret_key);
        WWW hs_post = new WWW(postUrl);
        yield return hs_post;

        if (hs_post.error != null)
        {
            print(hs_post.error);
        }
        else
        {
            print(hs_post.text);

            if (buttonPressed == 1)
            {
                Application.LoadLevel(Application.loadedLevel);
            }
            else if (buttonPressed == 2)
            {
                Application.LoadLevel("Level Select");
            }
            else if (buttonPressed == 3)
            {
                Application.LoadLevel("level2new");
            }
        }


    }

    IEnumerator checkScore(int id, string name, Double score)
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            if (buttonPressed == 1)
            {
                Application.LoadLevel(Application.loadedLevel);
            }
            else if (buttonPressed == 2)
            {
                Application.LoadLevel("Level Select");
            }
            else if (buttonPressed == 3)
            {
                Application.LoadLevel("level2new");
            }

        }

		string postUrl = checkURL + "ID=" + id + "&Name=" + WWW.EscapeURL(name);
        WWW hs_post = new WWW(postUrl);
        yield return hs_post;

        if (hs_post.error != null)
        {
            print(hs_post.error);
        }
        else
        {
            print(hs_post.text);
        }

        if (hs_post.text == "false")
        {
            StartCoroutine(postScores1(id, name, score));
        }
        else
        {
            StartCoroutine(updateScore1(id, name, score));
        }

    }

}
