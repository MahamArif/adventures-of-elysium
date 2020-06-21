using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;

public class YouWon : MonoBehaviour
{
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

    public KeyUpdate Key;       // Reference to the player's health.

    public GameObject youwon;
    public GameObject youwontext;
    //public GameObject levelfinished;

    float restartDelay = 3f;
    float restartTimer;    
                             // Reference to the animator component.
    // Timer to count up to restarting the level


    void Awake()
    {
        // Set up the reference.

        info = GameObject.Find("Player_Info").GetComponent<Player_Info>();
        obj = GameObject.Find("BackButtonClick").GetComponent<LevelBackButtonClick>();
        soundPlay = GameObject.Find("SoundPlay").GetComponent<AudioSource>();
    }


    void Update()
    {
        
        if (Key.Keys == 3)
        {

            soundPlay.clip = levelSucceededsound;
            soundPlay.Play();


            youwon.active = true;
            youwontext.active = true;


            if (info.getProgress() == 0)
            {
                info.setProgress(1);
            }
            else
            {
                info.setProgress(2);
            }

            restartTimer += Time.deltaTime;

            // .. if it reaches the restart delay...
            if (restartTimer >= restartDelay)
            {
                // .. then reload the currently loaded level.
                //Application.LoadLevel(Application.loadedLevel);

                characterScript.GetComponent<playerTestRun>().enabled = false;

                id1 = info.getID();
                name1 = info.getName();

                levelProgress = info.getProgress();

                score2dis = info.getScore();
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