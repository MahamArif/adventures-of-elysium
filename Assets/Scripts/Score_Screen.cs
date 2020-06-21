using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Score_Screen : MonoBehaviour {
    //public AudioListener audio;

    public Sprite sprite1;
    public Sprite sprite2;
    public Button btn1;
    Image theImage;

    Player_Info script2;

    public GameObject playerSelectionConstraint;


    void Start()
    {
       // Time.timeScale = 1;
        //theImage = GameObject.Find("Toggle_Audio").GetComponent<Image>();

        script2 = GameObject.Find("Player_Info").GetComponent<Player_Info>();

        theImage = btn1.GetComponent<Image>();
        if (AudioListener.volume == 0)
        {
            theImage.sprite = sprite2;
        }
        else
        {
            theImage.sprite = sprite1;
        }
    }

    public void scoreboard()
    {
        Application.LoadLevel(Application.loadedLevel+1);
    }
    public void levels()
    {
        print(script2.getID().ToString());
        if (script2.getID() != 0)
        {
            if(script2.getStatus()){
				Application.LoadLevel("FinalScenetillnow8");
			}
			else{
			//script2.setID(2);
			//script2.setScore(1000);
              //script2.setProgress(1);
				Application.LoadLevel ("Level Select");
			}
        }
        else
        {
            playerSelectionConstraint.active = true;
        }
    }

    public void hide_PlayerConstraint()
    {
        playerSelectionConstraint.active = false;
    }

    public void quit_game()
    {
        Application.Quit();
    }
    public void back_to_menu()
    {
        Application.LoadLevel(Application.loadedLevel-1);
    }
    public void toggle_audio()
    {
        //theImage = btn1.GetComponent<Image>();
        if (AudioListener.volume != 0)
        {
            AudioListener.volume = 0;
            theImage.sprite = sprite2;
        }
        else
        {
            AudioListener.volume = 1;
            theImage.sprite = sprite1;
        }
       
    }
}
