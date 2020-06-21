using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Button_Transition : MonoBehaviour {

    public Button btn1;
    public Button btn2;

	// Use this for initialization
	void Start () {
        btn1.transition = Selectable.Transition.Animation;
        btn2.transition = Selectable.Transition.Animation;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
