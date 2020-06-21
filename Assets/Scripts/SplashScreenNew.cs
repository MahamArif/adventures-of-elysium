using UnityEngine;
using System.Collections;

public class SplashScreenNew : MonoBehaviour {
	public float delay = 5;
	// Use this for initialization
	IEnumerator Start (){
		yield return new WaitForSeconds (delay);
		Application.LoadLevel ("SceneonLoad");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
