using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KillMonster : MonoBehaviour {


    public GameObject Monster1, Monster2, Monster3;
    public Slider _1, _2, _3;
    private float Damage;
    private GameObject DOG;
    private Animator Player;
    public GameObject AttackPrefeb;
    private GameObject go;
    private Slider MagicValue;

    public Animator Monster1Ani;
    public Animator Monster2Ani;
    private Animator NOmp;

    //Dead

    public GameObject DeadParticle;
    private GameObject Clone;

    private AudioSource PlayerAttackSound;



    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        DOG = GameObject.FindGameObjectWithTag("Attack_Player");
        PlayerAttackSound = GameObject.Find("powerup05 (1)").GetComponent<AudioSource>();
        NOmp = GameObject.Find("NoMp").GetComponent<Animator>();


    }
    public void Kill()
    {
        MagicValue = GameObject.FindGameObjectWithTag("MagicValueControll").GetComponent<Slider>();
        if (MagicValue.value >= 20)
        {
            Animator descrese = GameObject.Find("Value Decrease-20").GetComponent<Animator>();
            descrese.SetTrigger("show");
            MagicValue.value -= 20f;
            go = (GameObject)Instantiate(AttackPrefeb);
            go.transform.position = DOG.transform.position;

            Player.SetTrigger("attack");
            PlayerAttackSound.Play();
        }
        else
        {
            NOmp.SetTrigger("Show");
            Debug.Log("No MP");
        }
        
       
    

        


    }
    private void Update()
    {
        if (GameObject.FindGameObjectWithTag("Monster1") && _1.value > 0 || GameObject.FindGameObjectWithTag("Monster2") && _2.value > 0)

        
            return;
        

        if ((GameObject.FindGameObjectWithTag("Monster1") && _1.value <= 0) || (GameObject.FindGameObjectWithTag("Monster2") && _2.value <= 0))
        {
            if (GameObject.FindGameObjectWithTag("Monster1"))
            {
                Monster1Ani.SetTrigger("Monster1Dead");
                Clone = (GameObject)Instantiate(DeadParticle);
                Clone.transform.position = this.transform.position;
                Clone.transform.parent = this.transform;
            }
            if (GameObject.FindGameObjectWithTag("Monster2"))
            {
                Monster2Ani.SetTrigger("Monster2Dead");
                Clone = (GameObject)Instantiate(DeadParticle);
                Clone.transform.position = this.transform.position;
                Clone.transform.parent = this.transform;
            }
            Destroy(this.gameObject, 1.5f);
            
        }
       
    }

}
