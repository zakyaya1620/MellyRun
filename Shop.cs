using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour {


    private Animator ShopUI;
    public GameObject angel, rice, beer;
    private GameObject Clone;
    private Transform Head;
    private float TotalCoin;
    public Text item1, item2, item3, item5;
    public string HasBeenBought;

    //Contack me Page
    public Animator ContactMe;
    

    //Hat status
    private string HatBuy = "hatisbebout";
    private bool AngelOn =false;

    //Rice State
    private string RiceBuy = "RiceHasBeenBought";
    private bool RiceOn = false;

    //Beer State
    private string BeerBuy = "BeerHasbennBought";
    private bool BeerOn = false;


    //Music
    public AudioSource seton, enoughmoney, buy;
    public AudioSource ClickPageSound;

    private void Awake()
    {
        TotalCoin = PlayerPrefs.GetFloat("TotalCoin");


    }
    private void Update()
    {
        if (PlayerPrefs.HasKey("BuyHat"))
        {
            item1.text = HasBeenBought;
        }
        if (PlayerPrefs.HasKey("BuyRice"))
        {
            item2.text = HasBeenBought;
        }
        if (PlayerPrefs.HasKey("BuyBeer"))
        {
            item3.text = HasBeenBought;
        }

    }
     


    // Use this for initialization
    void Start () {

        Head = GameObject.Find("Bone_003").transform;

        ShopUI = GameObject.Find("ShoPage").GetComponent<Animator>();

        TotalCoin = GameManager.Instance.TotalCoin;





    }
	
    

    public void ShopOnclick()
    {        
        ShopUI.SetTrigger("Show");
        ClickPageSound.Play();

    }
    
    public void ShopClose()
    {
        ShopUI.SetTrigger("Hide");
        ClickPageSound.Play();
    }

    public void buyhat()
    {

        if (GameManager.Instance.TotalCoin >= 200&& !PlayerPrefs.HasKey("BuyHat"))
        {
            Debug.Log("Buy it");            
            GameManager.Instance.TotalCoin -= 200;
            buy.Play();
            PlayerPrefs.SetString("BuyHat", HatBuy);
            PlayerPrefs.SetFloat("TotalCoin", (float)TotalCoin);
            Clone = (GameObject)Instantiate(angel);
            Clone.transform.parent = Head;
            AngelOn = true;
            
           
        }
        else
        {
            enoughmoney.Play();
        }
        
        if (PlayerPrefs.HasKey("BuyHat") && AngelOn==false)
        {
            Debug.Log("Set on");
            seton.Play();
            Clone = (GameObject)Instantiate(angel);
            Clone.transform.parent = Head;
            AngelOn = true;
            return;
        }
        
        if (PlayerPrefs.HasKey("BuyHat") && GameObject.FindGameObjectWithTag("Angel"))
        {
            seton.Play();
            Destroy(GameObject.FindGameObjectWithTag("Angel"), 0);
            AngelOn = false;
            return;

        }
        
    }

    public void BuyRice()
    {
        if (GameManager.Instance.TotalCoin >= 500 && !PlayerPrefs.HasKey("BuyRice"))
        {
            Debug.Log("Buy it");
            GameManager.Instance.TotalCoin -= 500;
            buy.Play();
            PlayerPrefs.SetFloat("TotalCoin", (float)TotalCoin);
            PlayerPrefs.SetString("BuyRice", RiceBuy);
            Clone = (GameObject)Instantiate(rice);
            Clone.transform.parent = Head;
            RiceOn = true;


        }
        else
        {
            enoughmoney.Play();
        }


        if (PlayerPrefs.HasKey("BuyRice") && RiceOn == false)
        {
            Debug.Log("Set on");
            seton.Play();
            Clone = (GameObject)Instantiate(rice);
            Clone.transform.parent = Head;
            RiceOn = true;
            return;
        }

        if (PlayerPrefs.HasKey("BuyRice") && GameObject.FindGameObjectWithTag("Rice"))
        {
            seton.Play();
            Destroy(GameObject.FindGameObjectWithTag("Rice"), 0);
            RiceOn = false;
            return;

        }
    }

    public void BuyBeer()
    {
        if (GameManager.Instance.TotalCoin >= 500 && !PlayerPrefs.HasKey("BuyBeer"))
        {
            Debug.Log("Buy it");
            GameManager.Instance.TotalCoin -= 500;
            buy.Play();
            PlayerPrefs.SetFloat("TotalCoin", (float)TotalCoin);
            PlayerPrefs.SetString("BuyBeer", BeerBuy);
            Clone = (GameObject)Instantiate(beer);
            Clone.transform.parent = Head;
            BeerOn = true;


        }
        else
        {
            enoughmoney.Play();
        }


        if (PlayerPrefs.HasKey("BuyBeer") && BeerOn == false)
        {
            seton.Play();
            Debug.Log("Set on");
            Clone = (GameObject)Instantiate(beer);
            Clone.transform.parent = Head;
            BeerOn = true;
            return;
        }

        if (PlayerPrefs.HasKey("BuyBeer") && GameObject.FindGameObjectWithTag("Beer"))
        {
            seton.Play();
            Destroy(Clone, 0);
            BeerOn = false;
            return;

        }
    }

    public void ContactMePage()
    {
        ContactMe.SetTrigger("Show");
        ClickPageSound.Play();
    }
    public void CloseContactMePage()
    {
        ContactMe.SetTrigger("Close");
        ClickPageSound.Play();
    }


}


