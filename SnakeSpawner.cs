using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeSpawner : MonoBehaviour {

    public Animator SnakeShow; 
    private GameObject Player;
   

	// Use this for initialization
	void Start () {

        Player = GameObject.FindGameObjectWithTag("Player");
        
		
	}


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == Player)
        {
            Debug.Log("Come on");
            SnakeShow.SetTrigger("snakeshow");
        }
    }
  
}
