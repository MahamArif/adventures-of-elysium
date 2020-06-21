using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TUpdate : MonoBehaviour {

	public int Treasure = 0;
	private Text myText;
	
	// Use this for initialization
	void Start () {
		myText = GetComponent<Text> ();
	}
	
	public void TCount(){
		Treasure++; 
		print (Treasure.ToString());
		myText.text = Treasure.ToString ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
