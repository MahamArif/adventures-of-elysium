using UnityEngine;
using System.Collections;

public class SplashScreen : MonoBehaviour {

	public float delay = 5;
	IEnumerator Start (){
		yield return new WaitForSeconds (delay);
		Application.LoadLevel ("SceneonLoad");
	}
}
