using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void onClick()
    {
        string text1 = this.gameObject.GetComponent<Text>().text;
        print(text1);
    }
}
