using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class SelectableObject : MonoBehaviour {

    public EventSystem event1;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            // Check if the mouse was clicked over a UI element
            if (EventSystem.current.IsPointerOverGameObject())
            {
                Debug.Log("Clicked on the UI");
                GameObject gameObject = event1.lastSelectedGameObject;
                string text1 = gameObject.GetComponent<Text>().text;
                print(text1);
            }
        }	
	}
}
