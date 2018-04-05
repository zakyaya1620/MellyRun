using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster3 : MonoBehaviour {



    public Animator Monster3Anim;
    private int Turn;
  
	// Use this for initialization
	void Start () {

        InvokeRepeating("TurnTowhere", 4f, 2f);
      
	}
	
	// Update is called once per frame
	

    private void TurnTowhere()
    {
        Turn = Random.Range(0, 2);
        if (Turn == 0)
        {
            Monster3Anim.SetTrigger("Left");
        }
        if (Turn == 1)
        {
            Monster3Anim.SetTrigger("Right");
        }
    }

   
}
