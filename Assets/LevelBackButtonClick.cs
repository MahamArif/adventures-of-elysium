using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using System.Data;
using System.Collections.Generic;
//using UnityEngine.UI;
using System.IO;
using System.Text;
using Mono.Data.SqliteClient;
using UnityEngine.UI;

public class LevelBackButtonClick : MonoBehaviour {

    public GameObject characterScript;

    public GameObject optionMenu;
    //PlayerNamesManager code;
    Player_Info code1;

    //public GameObject obj;

    private string addScoreURL = "http://dadarentacar.com/addScore2.php?";
    private string checkURL = "http://dadarentacar.com/CheckQuery2.php?";
    private string updateURL = "http://dadarentacar.com/UpdateScore1.php?";

    private string connection;
    private IDbConnection dbcon;
    private IDbCommand dbcmd;
    private IDataReader reader;
    private StringBuilder builder;


	// Use this for initialization
	void Start () {
        //code = GameObject.Find("PlayerManager").GetComponent<PlayerNamesManager>();

        code1 = GameObject.Find("Player_Info").GetComponent<Player_Info>();

        
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void onBackButtonClick()
    {
        optionMenu.active = true;
    }

    public void onYesClick()
    {
        //Application.LoadLevel("OptionsScreenFinal7");
        //try
        //{
            int id1 = code1.getID();
            string name1 = code1.getName();
            Double score1 = code1.getScore();

            int levelProgress = code1.getProgress();


            print(name1 + id1 + score1);

		code1.setStatus (false);

            if (id1 != 0 && score1 != 0)
            {

                OpenDB("Score.sqlite");
                updateScore(id1.ToString(), score1.ToString(), levelProgress.ToString());

                characterScript.GetComponent<playerTestRun>().enabled = false;
                optionMenu.active = false;

                StartCoroutine(checkScore(id1, name1, score1));

                /*code.OpenDB("Score.sqlite");
                code.updateScore(id1.ToString(), score1.ToString());
                code.CloseDB();*/

               
                //CloseDB();

           }
            else
            {
                optionMenu.active = false;

                Application.LoadLevel("OptionsScreenFinal8");
            }

           // addDelay();
            //MyDelay(10);

            //Application.LoadLevel("OptionsScreenFinal8");
            // Application.Quit();
            //characterScript.GetComponent<PlayerRun>().enabled = false;
            //optionMenu.active = false;
        //}
        //catch (Exception e)
        //{
           // obj.GetComponent<Text>().text = e.Message;
        //}
    }

    public void onNoClick()
    {
        optionMenu.active = false;
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

            Application.LoadLevel("OptionsScreenFinal8");
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

            Application.LoadLevel("OptionsScreenFinal8");
        }


    }

    IEnumerator checkScore(int id, string name, Double score)
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            Application.LoadLevel("OptionsScreenFinal8");

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

    public void OpenDB(string p)
    {
        Debug.Log("Call to OpenDB:" + p);
        // check if file exists in Application.persistentDataPath
        string filepath = Application.persistentDataPath + "/" + p;
        if (!File.Exists(filepath))
        {
            Debug.LogWarning("File \"" + filepath + "\" does not exist. Attempting to create from \"" +
                             Application.dataPath + "!/assets/" + p);
            // if it doesn't ->
            // open StreamingAssets directory and load the db -> 
            WWW loadDB = new WWW("jar:file://" + Application.dataPath + "!/assets/" + p);
            while (!loadDB.isDone) { }
            // then save to Application.persistentDataPath
            File.WriteAllBytes(filepath, loadDB.bytes);
        }

        //open db connection
        connection = "URI=file:" + filepath;
        Debug.Log("Stablishing connection to: " + connection);
        dbcon = new SqliteConnection(connection);
        dbcon.Open();
    }

    public void CloseDB()
    {
        reader.Close(); // clean everything up
        reader = null;
        dbcmd.Dispose();
        dbcmd = null;
        dbcon.Close();
        dbcon = null;
    }

    public void updateScore(string id, string value, string value1)
    {
        string query;
        //query = "UPDATE ScoreCard SET Player_HScore = " + value + " WHERE Player_ID = " + id;
        query = "UPDATE ScoreCard SET Level_ID = " + value1 + ", Player_HScore = " + value + " WHERE Player_ID = " + id;

        try
        {
            dbcmd = dbcon.CreateCommand(); // create empty command
            dbcmd.CommandText = query; // fill the command
            //reader = dbcmd.ExecuteReader(); // execute command which returns a reader
            dbcmd.ExecuteNonQuery();
        }
        catch (Exception e)
        {

            // Debug.Log(e);
        }
    }

    IEnumerator addDelay()
    {
        yield return new WaitForSeconds(20f);
    }

    public void MyDelay(int seconds)
    {
        DateTime dt = DateTime.Now + TimeSpan.FromSeconds(seconds);

        do { } while (DateTime.Now < dt);
    }

    public void ReloadLevel()
    {
        Application.LoadLevel(Application.loadedLevel);
    }

    public void GotoLevelSelect()
    {
        Application.LoadLevel("Level Select");
    }
}
