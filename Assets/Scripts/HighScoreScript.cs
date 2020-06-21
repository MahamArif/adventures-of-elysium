using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HighScoreScript : MonoBehaviour {

    public GameObject player_rank;
    public GameObject player_name;
    public GameObject player_score;

    public void SetScore(string rank, string name, string score)
    {
        this.player_rank.GetComponent<Text>().text = rank;
        this.player_name.GetComponent<Text>().text = name;
        this.player_score.GetComponent<Text>().text = score;
    }
}
