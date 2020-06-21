using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    Player_Info code1;

	public KeyUpdate Keey;       // Reference to the player's health.

	
	Animator anim;                          // Reference to the animator component.
	                    // Timer to count up to restarting the level
	
	
	void Awake ()
	{
		// Set up the reference.
		anim = GetComponent <Animator> ();

        code1 = GameObject.Find("Player_Info").GetComponent<Player_Info>();
	}
	
	
	void Update ()
	{
		// If the player has run out of health...
		if(Keey.Keys == 3)
		{
			// ... tell the animator the game is over.
			anim.SetTrigger ("DoorOpen");

            if (code1.getProgress() == 0)
            {
                code1.setProgress(1);
            }
            else
            {
                code1.setProgress(2);
            }


		}
	}
}