using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelSelectLogic : MonoBehaviour {

    Player_Info code1;
    public Sprite sprite2;
    public Button btn1;
    public GameObject obj;
    Image theImage;
    // Use this for initialization

    void Start()
    {
        theImage = btn1.GetComponent<Image>();
        code1 = GameObject.Find("Player_Info").GetComponent<Player_Info>();
        if (code1.getProgress() == 0)
        {
            theImage.sprite = sprite2;
            btn1.GetComponent<Button>().interactable = false;
            obj.GetComponent<Text>().text = "";

        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Level1btn()
    {
        Application.LoadLevel("FinalScenetillnow8");
    }
    public void Level2btn()
    {
        Application.LoadLevel("Level2New");
    }
    public void BackButton()
    {
        Application.LoadLevel("OptionsScreenFinal8");
    }
}
