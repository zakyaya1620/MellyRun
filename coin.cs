using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coin : MonoBehaviour {

    private Animator PlayerAnimator;
    private AudioSource CoinSound;


    private void Start()
    {
        PlayerAnimator = GetComponent<Animator>();
        CoinSound = GameObject.Find("Coin (1)").GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            CoinSound.Play();
            GameManager.Instance.GetCoin();
            GameManager.Instance.CoinCollection();
            PlayerAnimator.SetTrigger("Collected");
            
            
        }
    }
}
