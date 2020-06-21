using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using SimpleJSON;

public class HighScores : MonoBehaviour {

    string highScoreURL1 = "http://dadarentacar.com/displayScore3.php";

    public GameObject scorePrefab;
    public Transform scoreParent;
    public Text textobject;

	// Use this for initialization
	void Start () {

        print("Hello");
        StartCoroutine(getScores());
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    IEnumerator getScores()
    {
        //TextMesh textObject = GameObject.Find("Text").GetComponent<TextMesh>();
        //textObject.text = "Loading Scores";
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            Debug.Log("Error. Check internet connection!");
            textobject.GetComponent<Text>().text = "No Internet Connection!";

        }
        else
        {

            WWW hs_get = new WWW(highScoreURL1);
            yield return hs_get;

            if (hs_get.error != null)
            {
                print(hs_get.error);
            }
            else
            {
                print(hs_get.text);
            }

            //string t1 = "{\"Player_Info\":" + hs_get.text + "}";
            //string t = "";
            var N = JSON.Parse(hs_get.text);

            for (int i = 0; i < N.Count; i++)
            {
                if (N[i]["Player_HScore"].Value.ToString() != "0")
                {
                    GameObject tmpObject = Instantiate(scorePrefab);
                    tmpObject.GetComponent<HighScoreScript>().SetScore("#" + (i + 1).ToString(), N[i]["Player_Name"].Value.ToString(), N[i]["Player_HScore"].Value.ToString());
                    tmpObject.transform.SetParent(scoreParent);
                    tmpObject.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                }
                
            }


        }

        //textObject.text = N.Count.ToString();
        //for (int i = 0; i < N.Count; i++)
           // t += N[i]["Player_Name"].Value.ToString() + "\n";
        //textObject.text = t;
    }
}
