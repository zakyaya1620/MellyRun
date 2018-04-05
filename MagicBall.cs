using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagicBall : MonoBehaviour {


    public static MagicBall Instance { set; get; }
    private Slider MagicBar;
    public Animator EatMagic;
    public Animator EatMagic2;
    public bool MagicProtect ;
    public AudioSource EatSound;

   
  
   

    //Magic Value Text
    private Text MagicText;
    
    
   

    private void Awake()
    {
        Instance = this;
    }
    


    private void Start()
    {
        
        MagicBar = GameObject.Find("Layer").GetComponent<Slider>();

        //How to find a text.
        MagicText = GameObject.FindGameObjectWithTag("MagicValueText").GetComponent<Text>();





    }
    // Use this for initialization
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Player"))
        {

            EatSound = GameObject.Find("poka02").GetComponent<AudioSource>();
            EatSound.Play();
            EatMagic.SetTrigger("Eat");
            EatMagic2.SetTrigger("Eat");
            MagicBar.value = Mathf.MoveTowards(MagicBar.value, MagicBar.value + 5f, 100f);
           
                       
        }
        else if(MagicBar.value != 100f && other.gameObject.CompareTag("Player"))
        {
          
            EatMagic.SetTrigger("Eat");
            EatMagic2.SetTrigger("Eat");
            MagicBar.value = Mathf.MoveTowards(MagicBar.value, MagicBar.value + 0f, 100f);
           
        }
        
    }

    private void Update()
    {

        MagicText.text = MagicBar.value.ToString("0");
        if (MagicBar.value >=20)
        {
            MagicProtect = true;
        }
        else { MagicProtect = false; }



      

         
        
        
    }



    
}
