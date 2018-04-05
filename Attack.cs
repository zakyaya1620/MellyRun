using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{


    private GameObject Player;
    private Vector3 AttackPointPosition;
    

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        


    }
    private void Update()
    {
        AttackPointPosition = new Vector3(0, Player.transform.position.y-2, Player.transform.position.z-2);
        this.gameObject.transform.position = Vector3.Lerp (this.transform.position, AttackPointPosition, Time.deltaTime * 1f);

     



    }

}