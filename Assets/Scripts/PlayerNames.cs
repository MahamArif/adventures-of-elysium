using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerNames : MonoBehaviour {
    public GameObject mytext;
	// Use this for initialization

	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void setText(string name)
    {
        mytext.GetComponent<Text>().text = name;
    }
}
