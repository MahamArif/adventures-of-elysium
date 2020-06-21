using UnityEngine;
using System.Collections;

public class SplashWithFading : MonoBehaviour {

    public float delay = 5;
    // Use this for initialization
    IEnumerator Start()
    {
        yield return new WaitForSeconds(delay);
        float fadeTime = GameObject.Find("Fade").GetComponent<Fading>().BeginFade(1);
        yield return new WaitForSeconds(fadeTime);
        Application.LoadLevel(Application.loadedLevel+1);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
