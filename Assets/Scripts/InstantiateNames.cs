using UnityEngine;
using System.Collections;

public class InstantiateNames : MonoBehaviour {

	// Use this for initialization
	void Start () {
        PlayerNamesManager code = GameObject.Find("PlayerManager").GetComponent<PlayerNamesManager>();
        code.OpenDB("Score.sqlite");
        code.SingleSelect("ScoreCard", "*");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
