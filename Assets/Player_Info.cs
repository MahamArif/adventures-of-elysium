using UnityEngine;
using System.Collections;
using System.Data;
using System.Collections.Generic;
using System;

public class Player_Info : MonoBehaviour {

    private int id = 0;
    private Double score = 0;
    private string name = "";
    private bool newPlayer = false;
    private int levelProgress = 0;

    public int getProgress()
    {
        return levelProgress;
    }

    public void setProgress(int progress)
    {
        levelProgress = progress;
    }

    public bool getStatus()
    {
        return newPlayer;
    }

    public void setStatus(bool stat)
    {
        newPlayer = stat;
    }

    public string getName()
    {
        return name;
    }

    public int getID()
    {
        return id;
    }

    public Double getScore()
    {
        return score;
    }

    public void setName(string name)
    {
        this.name = name;
    }

    public void setID(int id)
    {
        this.id = id;
    }

    public void setScore(double score)
    {
        this.score = score;
    }

    public void printInfo()
    {
        print(name + " " + id.ToString() + " " + score.ToString());
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
