using UnityEngine;
using System.Collections;

public class PlayerRun : MonoBehaviour {

	private Animator anim;
	private CharacterController controller;
	
	/*public float speed = 6.0f;
	public float turnSpeed = 60.0f;
	private Vector3 moveDirection = Vector3.zero;
	public float gravity = 20.0f;*/
	
	private Vector2 fp; // first finger position
	private Vector2 lp; // last finger position
	
	void Start () {
		controller = GetComponent <CharacterController>();
		anim = gameObject.GetComponentInChildren<Animator>();
		//anim.SetInteger ("AnimPar", 0);
	}
	
	void Update () {
		//anim.SetInteger ("AnimPar", 0);
		foreach(Touch touch in Input.touches)
		{
			//moveDirection = transform.forward * Input.GetAxis("Vertical") * speed;
			if (touch.phase == TouchPhase.Began)
			{
				fp = touch.position;
				lp = touch.position;
			}
			if (touch.phase == TouchPhase.Moved )
			{
				lp = touch.position;
			}
			if(touch.phase == TouchPhase.Ended)
			{
				//anim.SetInteger ("AnimPar", 1);
				Vector3 newPosition = transform.position;
				if((fp.x - lp.x) > 80) // left swipe
				{
					//newPosition.x -= speed;
					//transform.position = newPosition;
					//transform.Rotate(Vector3.zero,-90,Space.World);
					transform.Rotate(0,-90,0);
					//moveDirection = Vector3.left;
				}
				else if((fp.x - lp.x) < -80) // right swipe
				{
					//newPosition.x += speed;
					//transform.position = newPosition;
					transform.Rotate(0,90,0);
					//moveDirection = Vector3.right;
				}
				else if((fp.y - lp.y) < -80 ) // up swipe
				{
					
					/*moveDirection *= speed;
					// Apply gravity
					moveDirection.y -= gravity * Time.deltaTime;
					// Move the controller
					//var controller : CharacterController = GetComponent(CharacterController);
					controller.Move(moveDirection * Time.deltaTime);
					//grounded = (flags & CollisionFlags.CollidedBelow) != 0;
					// add your jumping code here*/
				}
				
				//moveDirection.y -= gravity * Time.deltaTime;
				//controller.Move(moveDirection * Time.deltaTime);
				
				
				
				
			}
		}
		transform.Translate(Vector3.forward * Time.deltaTime*20);
	}


	void OnTriggerEnter (Collider collider){

        if (collider.gameObject.CompareTag("sword") || collider.gameObject.CompareTag("cone") || collider.gameObject.name=="Kofflin" )//|| collider.gameObject.CompareTag("Kofflin1") || collider.gameObject.CompareTag ("Kofflin2"))
        {
			LivesUpdate life = GameObject.Find("Lives").GetComponent<LivesUpdate>();
			life.LoseLife();

	
		 

	}


}
}
