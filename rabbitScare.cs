using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rabbitScare : MonoBehaviour {


    public static rabbitScare Instance { set; get; }
    public Animator Rabbit;
   



    private void Awake()
    {
        Instance = this;
    }

  

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Here");
            Rabbit.SetTrigger("RbbitScare");
           
        }
    }
  
}
