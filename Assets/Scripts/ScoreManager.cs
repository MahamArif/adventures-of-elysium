using UnityEngine;
using System.Collections;
using System;
using System.Data;
using Mono.Data.Sqlite;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

    public GameObject choosePlayerMenu;
    public GameObject PlayerInput;
    public GameObject crossButtonPlayer;
    public GameObject crossButtonInput;
    public GameObject text;
    public GameObject scorePrefab;
    public Transform scoreParent;

	private string connectionString;
	// Use this for initialization
	void Start () {
		//connectionString = "URI=file:" + Application.dataPath + "/Score.sqlite";
		//GetScores ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void GetScores()
	{
		IDbConnection dbConnection = (IDbConnection)new SqliteConnection (connectionString);
		dbConnection.Open ();    

		IDbCommand dbCmd = dbConnection.CreateCommand ();
		string sqlQuery="Select * from ScoreCard";

		dbCmd.CommandText = sqlQuery;

		IDataReader reader = dbCmd.ExecuteReader ();
					while (reader.Read())
					{
                        print(reader.GetString(1)); //+ " " + reader.GetDouble(3));
					}
					dbConnection.Close ();
					reader.Close ();
				

		}

    public void writeData(string text1)
    {
        /*IDbConnection dbConnection = (IDbConnection)new SqliteConnection(connectionString);
        dbConnection.Open();

        IDbCommand dbCmd = dbConnection.CreateCommand();
        string sqlQuery = "INSERT INTO ScoreCard (Player_Name, Player_HScore) VALUES (@name, 0);";
        dbCmd.CommandText = sqlQuery;

        dbCmd.Parameters.Add(new SqliteParameter("@name", text1));
        dbCmd.ExecuteNonQuery();
        dbCmd.Dispose();
        dbCmd = null;
        dbConnection.Close();
        dbConnection = null;*/

        using (IDbConnection dbConnection = new SqliteConnection(connectionString))
        {
            dbConnection.Open();

            using (IDbCommand dbCmd = dbConnection.CreateCommand())
            {
                string sqlQuery = "INSERT INTO ScoreCard (Player_Name, Player_HScore) VALUES (@name, 0);";
                dbCmd.CommandText = sqlQuery;

                dbCmd.Parameters.Add(new SqliteParameter("@name", text1));
                dbCmd.ExecuteNonQuery();

                dbConnection.Close();
            }
        }
        
    }

    public void showObject()
    {
        choosePlayerMenu.active = true;
        crossButtonPlayer.active = true;
        //PlayerNamesManager script1 = GameObject.Find("PlayerManager").GetComponent<PlayerNamesManager>();
        //script1.GetNames();
    }

    public void hideObject()
    {
        choosePlayerMenu.active = false;
        crossButtonPlayer.active = false;
    }

    public void showInputCanvas()
    {
        PlayerInput.active = true;
        crossButtonInput.active = true;
    }

    public void hideInputCanvas()
    {
        PlayerInput.active = false;
        crossButtonInput.active = false;
    }

    public void onInputok()
    {
        string name = text.GetComponent<Text>().text;
        //writeData(name);
        if (name != "")
        {
            Player_Info script2 = GameObject.Find("Player_Info").GetComponent<Player_Info>();

            PlayerNamesManager script1 = GameObject.Find("PlayerManager").GetComponent<PlayerNamesManager>();
            //script1.OpenDB("ScoreCard");
            script1.InsertIntoSingle(name);
            
            script2.setStatus(true);

            


            //script1.CloseDB();
            GameObject tmpObject = Instantiate(scorePrefab);
            int id = script1.getCount();
            id++;
            tmpObject.name = id.ToString();
            script1.setCount(id);

            script1.AddIntoList(tmpObject);

            tmpObject.GetComponent<PlayerNames>().setText(name);
            tmpObject.transform.SetParent(scoreParent);
            tmpObject.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        } 
        PlayerInput.active = false;
        crossButtonInput.active = false;
        
    }

}
