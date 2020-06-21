using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class KeyUpdate : MonoBehaviour {

    //Animator anim;
	public int Keys = 0;
	private Text myText;
	
	// Use this for initialization
	void Start () {
       
		myText = GetComponent<Text> ();
        //anim = GameObject.Find("Instruction Canvas").GetComponent<Animator>();
	}
	
	public void KeyCount(){
		Keys++;
		print (Keys.ToString());
		myText.text = Keys.ToString ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
