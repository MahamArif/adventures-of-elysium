using UnityEngine;
using System.Collections;
using System;
using System.Data;
//using Mono.Data.Sqlite;
using System.Collections.Generic;
//using UnityEngine.UI;
using System.IO;
using System.Text;
using Mono.Data.SqliteClient;
using UnityEngine.UI;

public class PlayerNamesManager : MonoBehaviour {

    public GameObject scorePrefab;
    public Transform scoreParent;
    List<GameObject> myList = new List<GameObject>();

    int countObjects = 0;

   // private string connectionString;

    private string connection;
    private IDbConnection dbcon;
    private IDbCommand dbcmd;
    private IDataReader reader;
    private StringBuilder builder;

    //public GameObject t;
    //public GameObject t1;



	// Use this for initialization
	void Start () {
       // connectionString = "URI=file:" + Application.dataPath + "/Score.sqlite";
        //GetNames();
        if (Application.loadedLevel == 1)
        {
            OpenDB("Score.sqlite");
            SingleSelect("ScoreCard", "*");
        }
        

        //Double score = SingleSelectWhere("ScoreCard", "*", "Player_ID", "16");
        //t.GetComponent<Text>().text = score.ToString();
        //CloseDB();
        //TestFunction();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void AddIntoList(GameObject obj)
    {
        myList.Add(obj);
    }

    public void changeColor()
    {
        for(int i=0; i<myList.Count; i++)
        {
            myList[i].GetComponent<Image>().color = new Color32(246, 199, 157, 255);
        }
    }

    public void setCount(int num)
    {
        countObjects = num;
    }

    public int getCount()
    {
        return countObjects;
    }

    public void GetNames()
    {
        /*IDbConnection dbConnection = (IDbConnection)new SqliteConnection(connectionString);
        dbConnection.Open();

        IDbCommand dbCmd = dbConnection.CreateCommand();
        string sqlQuery = "Select * from ScoreCard";

        dbCmd.CommandText = sqlQuery;

        IDataReader reader = dbCmd.ExecuteReader();
        while (reader.Read())
        {
            //print(reader.GetString(1)); //+ " " + reader.GetDouble(3));
            GameObject tmpObject = Instantiate(scorePrefab);
            tmpObject.GetComponent<PlayerNames>().setText(reader.GetString(1));
            tmpObject.transform.SetParent(scoreParent);
            tmpObject.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        }
        reader.Close();
        reader = null;
        dbCmd.Dispose();
        dbCmd = null;
        dbConnection.Close();
        dbConnection = null;*/


        /*using(IDbConnection dbConnection = new SqliteConnection(connectionString))
        {
            dbConnection.Open();
            using (IDbCommand dbCmd = dbConnection.CreateCommand())
            {
                string sqlQuery = "Select * from ScoreCard";
                dbCmd.CommandText = sqlQuery;

                using(IDataReader reader = dbCmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        //print(reader.GetString(1)); //+ " " + reader.GetDouble(3));
                        GameObject tmpObject = Instantiate(scorePrefab);
                        myList.Add(tmpObject);
                        tmpObject.GetComponent<PlayerNames>().setText(reader.GetString(1));
                        tmpObject.transform.SetParent(scoreParent);
                        tmpObject.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                    }
                    reader.Close();
                    dbConnection.Close();
                }
            }
        }*/
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

    public void SingleSelect(string tableName, string itemToSelect)
    { // Selects a single Item
        string query; 
        //string result = "";
        query = "SELECT " + itemToSelect + " FROM " + tableName;
        dbcmd = dbcon.CreateCommand();
        dbcmd.CommandText = query;
        reader = dbcmd.ExecuteReader();
        //string[,] readArray = new string[reader, reader.FieldCount];
        while (reader.Read())
        {
            //result += reader.GetString(1);
            GameObject tmpObject = Instantiate(scorePrefab);
            countObjects++;
            myList.Add(tmpObject);
            int id1 = Convert.ToInt32(reader["Player_ID"]);
            tmpObject.name = id1.ToString();
            tmpObject.GetComponent<PlayerNames>().setText(reader.GetString(1));
            tmpObject.transform.SetParent(scoreParent);
            tmpObject.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        }
        //return result;
        // return matches
    }

    public Double SingleSelectWhere(string tableName, string itemToSelect, string param, string value)
    { // Selects a single Item
        Double score1 = 0;
        string query;
        //string result = "";

        try
        {
            query = "SELECT " + itemToSelect + " FROM " + tableName + " WHERE " + param + " = " + value;
            dbcmd = dbcon.CreateCommand();
            dbcmd.CommandText = query;
            //dbcmd.Parameters.Add(new SqliteParameter("@id", value));
            reader = dbcmd.ExecuteReader();
            //string[,] readArray = new string[reader, reader.FieldCount];
            while (reader.Read())
            {
                score1 = reader.GetDouble(3);
                //int id = Convert.ToInt32(reader["Player_ID"]);
                //t1.GetComponent<Text>().text = id.ToString();
            }

        }
        catch (Exception e)
        {
            //t1.GetComponent<Text>().text = e.Message;
        }
        return score1;
        // return matches
    }

    public void InsertIntoSingle(string value)
    { // single insert
        string query;
        //query = "INSERT INTO " + tableName + "(" + colName + ",Player_HScore) " + "VALUES (" + value + ",0)";
        //query = "INSERT INTO ScoreCard (Player_Name ,Player_HScore) VALUES (@name , 0);";
        query = "INSERT INTO ScoreCard (Player_Name ,Level_ID ,Player_HScore) VALUES (@name , 0, 0);";
        try
        {
            dbcmd = dbcon.CreateCommand(); // create empty command
            dbcmd.CommandText = query; // fill the command
            //reader = dbcmd.ExecuteReader(); // execute command which returns a reader
            dbcmd.Parameters.Add(new SqliteParameter("@name", value));
            dbcmd.ExecuteNonQuery();
        }
        catch (Exception e)
        {

            //Debug.Log(e);
            //text1.GetComponent<Text>().text = e.Message;

            //return 0;
        }

        //return 1;
    }

    public void TestFunction()
    {
        for (int i = 0; i < 20; i++)
        {
            GameObject tmpObject = Instantiate(scorePrefab);
            myList.Add(tmpObject);
            tmpObject.name = (i + 1).ToString();
            tmpObject.GetComponent<PlayerNames>().setText("Hello"+i.ToString());
            tmpObject.transform.SetParent(scoreParent);
            tmpObject.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        }
    }

    public void updateScore(string id, string value)
    {
        string query;
        query = "UPDATE ScoreCard SET Player_HScore = " + value + " WHERE Player_ID = " + id;

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

    public int SingleSelectLevelID(string tableName, string itemToSelect, string param, string value)
    { // Selects a single Item
        //Double score1 = 0;
        int id = 0;
        string query;
        //string result = "";

        try
        {
            query = "SELECT " + itemToSelect + " FROM " + tableName + " WHERE " + param + " = " + value;
            dbcmd = dbcon.CreateCommand();
            dbcmd.CommandText = query;
            //dbcmd.Parameters.Add(new SqliteParameter("@id", value));
            reader = dbcmd.ExecuteReader();
            //string[,] readArray = new string[reader, reader.FieldCount];
            while (reader.Read())
            {
                //score1 = reader.GetDouble(3);
                id = Convert.ToInt32(reader["Level_ID"]);
                //t1.GetComponent<Text>().text = id.ToString();
            }

        }
        catch (Exception e)
        {
            //t1.GetComponent<Text>().text = e.Message;
        }
        return id;
        // return matches
    }

}
