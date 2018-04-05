using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterKill : MonoBehaviour
{

    public static MonsterKill Instance { set; get; }

    public bool hitBoom;
    public GameObject Boom;
    private GameObject Monster;        
    private Slider _1, _2, _3;

    private AudioSource BomFromMonster;

   



    private void Start()
    {
        Instance = this;

        //_3 = GameObject.Find("Back").GetComponent<Slider>();
        BomFromMonster = GameObject.Find("bomb from Monster").GetComponent<AudioSource>();

}



private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Monster1") || other.gameObject.CompareTag("Monster2"))
        {

            hitBoom = true;
            Destroy(this.gameObject, 0f);

            //Instatiate Boom effect!!!!!! 
            GameObject go = (GameObject)Instantiate(Boom);
            BomFromMonster.Play();
            go.transform.position = this.transform.position;
            Destroy(go, 1f);


            if (GameObject.FindGameObjectWithTag("Monster1") && _1.value >0|| GameObject.FindGameObjectWithTag("Monster2") && _2.value > 0)
            {
                if (GameObject.FindGameObjectWithTag("Monster1"))
                {
                    _1.value = Mathf.MoveTowards(_1.value, _1.value - 10f, 100f);
                }
                  
                if (GameObject.FindGameObjectWithTag("Monster2"))
                {
                      _2.value = Mathf.MoveTowards(_2.value, _2.value - 10f, 100f);
                }

            }

            

        }

    }

    private void Update()
    {
        if (GameObject.FindGameObjectWithTag("Monster1"))
        {
            _1 = GameObject.Find("Back").GetComponent<Slider>();
            transform.position = Vector3.Lerp(this.transform.position, GameObject.Find("AttackPoint_Player").transform.position, 2f*Time.deltaTime);
        }
        if (GameObject.FindGameObjectWithTag("Monster2"))
        {
            _2 = GameObject.Find("Back").GetComponent<Slider>();
            transform.position = Vector3.Lerp(this.transform.position, GameObject.Find("AttackPoint_Player").transform.position, 2*Time.deltaTime);
        }

        
        
    }
}
