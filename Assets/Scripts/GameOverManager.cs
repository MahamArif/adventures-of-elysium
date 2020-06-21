using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    Transform character;
	public LivesUpdate playerHealth;       // Reference to the player's health.
	float restartDelay = 5f;         // Time to wait before restarting the level

    public GameObject obj1;

	Player_Info info;
	
	Animator anim;                          // Reference to the animator component.
	float restartTimer;                     // Timer to count up to restarting the level

    AudioSource soundPlay;
    public AudioClip gameOver;

	void Awake ()
	{
		info = GameObject.Find("Player_Info").GetComponent<Player_Info>();
		// Set up the reference.
		anim = GetComponent <Animator> ();
        soundPlay = GameObject.Find("SoundPlay").GetComponent<AudioSource>();
        character = GameObject.Find("Steampunk_KidExtra_animation4").GetComponent<Transform>();
	}
	
	
	void Update ()
	{
		// If the player has run out of health...
		if(playerHealth.lives <= 0)
		{
            character.Rotate(90, 0, 0);
            soundPlay.clip = gameOver;
            soundPlay.Play();

			info.setStatus(false);

			// ... tell the animator the game is over.
			anim.SetTrigger ("GameOver");
			
			// .. increment a timer to count up to restarting.
			restartTimer += Time.deltaTime;
			
			// .. if it reaches the restart delay...
			if(restartTimer >= restartDelay)
			{
				// .. then reload the currently loaded level.
				//Application.LoadLevel(Application.loadedLevel);
                obj1.active = true;
			}
		}
	}

}