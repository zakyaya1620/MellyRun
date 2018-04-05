using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack02 : MonoBehaviour
{



    public GameObject _1, _2, _3, _4, _5;
    private int SpawnType;
    private GameObject go;

    public Animator Monster2;
    private int attackType;

    // Use this for initialization
    void Start()
    {
        InvokeRepeating("Spawn", 1,3);


    }

    // Update is called once per frame
   

    private void Spawn()
    {
        SpawnType = Random.Range(1, 6);
        attackType = Random.Range(0, 2);

        if (SpawnType == 1)
        {
            if (attackType == 1) { Monster2.SetTrigger("attack01"); }
            else { Monster2.SetTrigger("attack02"); }
            go = (GameObject)Instantiate(_1);
            go.transform.position = this.transform.position;

        }
        if (SpawnType == 2)
        {
            if (attackType == 1) { Monster2.SetTrigger("attack01"); }
            else { Monster2.SetTrigger("attack02"); }
            go = (GameObject)Instantiate(_2);
            go.transform.position = this.transform.position;

        }
        if (SpawnType == 3)
        {
            if (attackType == 1) { Monster2.SetTrigger("attack01"); }
            else { Monster2.SetTrigger("attack02"); }
            go = (GameObject)Instantiate(_3);
            go.transform.position = this.transform.position;

        }
        if (SpawnType == 4)
        {
            if (attackType == 1) { Monster2.SetTrigger("attack01"); }
            else { Monster2.SetTrigger("attack02"); }
            go = (GameObject)Instantiate(_4);
            go.transform.position = this.transform.position;

        }
        if (SpawnType == 5)
        {
            if (attackType == 1) { Monster2.SetTrigger("attack01"); }
            else { Monster2.SetTrigger("attack02"); }
            go = (GameObject)Instantiate(_5);
            go.transform.position = this.transform.position;

        }

    }
}