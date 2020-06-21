using UnityEngine;
using System.Collections;

public class InstructionAnimation : MonoBehaviour {

    float delay = 2;
    Animator anim;
    Player_Info code1;

	// Use this for initialization
	IEnumerator Start () {
        code1 = GameObject.Find("Player_Info").GetComponent<Player_Info>();

        anim = GetComponent<Animator>();

        if (code1.getStatus())
        {
            yield return new WaitForSeconds(delay);
            anim.SetInteger("InstrPar", 1);

            yield return new WaitForSeconds(4f);
            anim.SetInteger("InstrPar", 2);

           // yield return new WaitForSeconds(5.5f);
           // anim.SetInteger("InstrPar", 3);

            yield return new WaitForSeconds(4f);
            anim.SetInteger("InstrPar", 4);

            yield return new WaitForSeconds(4f);
            anim.SetInteger("InstrPar", 5);

            yield return new WaitForSeconds(4f);
            anim.SetInteger("InstrPar", 6);

			yield return new WaitForSeconds(4f);
			anim.SetInteger("InstrPar", 7);
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
