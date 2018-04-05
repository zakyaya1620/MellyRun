using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarnningPosition : MonoBehaviour
{

    private GameObject Player;
    public Animator warnning;
   

    // Use this for initialization
    void Start()
    {

        Player = GameObject.FindGameObjectWithTag("Player");


    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == Player)
        {
            warnning.SetTrigger("warnning");
        }
    }
}
