using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public enum GameState
{
    Running,
    Pause
}

public class GameManager : MonoBehaviour {

    public GameState gamestate = GameState.Running;

    //Timer
    private float Timer;
    private float Timer2;
    //Tutorial
    public Animator TutorialAnim;
    private bool Tutorial1, Tutorial2, Tutorial3;
    public string DeadHintString;
    private string DeadHintString_2 = "You did well ! \n Let's try again !";
    private int StringType;
    public Text DeadHintText;

    //music
    public AudioSource Music;
    private AudioSource GameoverSound;
    private AudioSource PauseSound;
   


    





    private const int COIN_SCORE_AMOUNT = 5;

    public static GameManager Instance { set; get; }
    private bool isGameStarted = false;
    public bool isDead { set; get; }
    private PlayerMove motor;
    public Sprite Pause;
    public Sprite Continue;
    public GameObject PauseButton;


    //Music

    [HideInInspector] public AudioClip BackGroundMusic,MenuMusic;
    [HideInInspector] public AudioSource BackGroundMusic_Sourse,MenuMusic_Sourse;
    private AudioSource HintSound;
    private AudioSource dead;
    private AudioSource StartSound;
   


    //UI and the UI fieds



    public Animator gameCanvas,menuAnim;
    public Text scoreText, coinText, modifierText, hiScoreText, TotalCoin_data;
    private float score, coinScore, modifierScore ;
    private int lastScore;
    //TotalCoin
    public float TotalCoin;
    private float FinalCoin;
   

    public Animator Player;

    //Death menu
    public Animator deathMenuAnim;
    public Text deadscoreText, deadcoinText;

    private void Awake()
    {
        Instance = this;
        modifierScore = 1;       
        motor = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMove>();

        modifierText.text = "x" + modifierScore.ToString("0.0");
        scoreText.text = scoreText.text = score.ToString("0");
        coinText.text = coinScore.ToString("0");
        //TotalCoin.text = TotalCoin_int.ToString("0");


        hiScoreText.text = PlayerPrefs.GetInt("Hiscore").ToString();
        //TotalCoin    
        TotalCoin = PlayerPrefs.GetFloat("TotalCoin");
        TotalCoin_data.text = PlayerPrefs.GetFloat("TotalCoin").ToString();


    }
    private void Update()
    {
      
        

        if (isGameStarted&& !isDead)
        {
                     
            //Bump the score up
            score += (Time.deltaTime * modifierScore);
            lastScore = (int)score;
            scoreText.text = score.ToString("0");
            if (lastScore != (int)score)
            {
                lastScore = (int)score;
                scoreText.text = scoreText.text = score.ToString("0");
            }

            Timer += Time.deltaTime;
            
            if (Timer > 0.5f && Timer < 0.52f&& !Tutorial1)
            {
                HintSound = GameObject.Find("Hint").GetComponent<AudioSource>();               
                Timer2 += Time.deltaTime;
                TutorialAnim.SetTrigger("Show");
                HintSound.Play();



            }


        }

        TotalCoin_data.text = TotalCoin.ToString();



    }

    public void GetCoin()
    {
        coinScore+=1;
        
        coinText.text= coinScore.ToString("0");
        

        score += COIN_SCORE_AMOUNT;
        scoreText.text= scoreText.text = score.ToString("0");
    }

    public void CoinCollection()
    {
        FinalCoin += 1;
       
    }

   
     

    public void UpdateModifier(float modifierAmount)
    {
        modifierScore= 1.0f + modifierAmount;
        modifierText.text = "x" + modifierScore.ToString("0.0");

    }

    public void OnPlayButton()
    {
        StartSound = GameObject.Find("Start_Sound").GetComponent<AudioSource>();
        StartSound.Play();
        UnityEngine.SceneManagement.SceneManager.LoadScene("1_14");
        AdManager.Instance.ShowVedio();

    }
   
    public void OnDeath()
    {
        dead = GameObject.Find("dead").GetComponent<AudioSource>();
        dead.Play();
        Music = GameObject.Find("Splashing_Around").GetComponent<AudioSource>();
        Music.Stop();
        GameoverSound = GameObject.Find("GameOverSound").GetComponent<AudioSource>();
        GameoverSound.Play();
        FindObjectOfType<GlacierSpawn>().IsScrolling = false;
        isDead = true;
        Debug.Log("Dead");
        TotalCoin += FinalCoin;

        StringType = Random.Range(0, 2);
        if (StringType == 0)
        {
            DeadHintText.text = DeadHintString;
        }
        if (StringType == 1)
        {
            DeadHintText.text = DeadHintString_2;
        }
        HintSound = GameObject.Find("Hint").GetComponent<AudioSource>();
    
        TutorialAnim.SetTrigger("Show");
        HintSound.Play();


        PlayerPrefs.SetFloat("TotalCoin", (float)TotalCoin);





        deadscoreText.text = score.ToString("0");
        deadcoinText.text = coinScore.ToString("0");
        deathMenuAnim.SetTrigger("Dead");


        //Check if this is a highscore
        if(score > PlayerPrefs.GetInt("Hiscore"))
        {
            float s = score;
            if (s % 1 == 0)
                s += 1;
            PlayerPrefs.SetInt("Hiscore",(int)s);
        }

        
      
        



}


    public void GameStart()
    {
        if (!isGameStarted)
        {
            StartSound = GameObject.Find("Start_Sound").GetComponent<AudioSource>();
            StartSound.Play();
            MenuMusic_Sourse.clip = MenuMusic;
            MenuMusic_Sourse.Stop();
            BackGroundMusic_Sourse.clip = BackGroundMusic;
            BackGroundMusic_Sourse.Play();
            isGameStarted = true;

            Player.SetBool("Run", true);

            motor.StarRunning();
            FindObjectOfType<GlacierSpawn>().IsScrolling = true;
            FindObjectOfType<CameraMotor>().IsMoving = true;
            gameCanvas.SetTrigger("Show");
            menuAnim.SetTrigger("Hide");

          

        }
    }


    public void transformGameState()
    {
        if(gamestate == GameState.Running)
        {
            GamePause();
        }
        else if(gamestate == GameState.Pause)
        {
            ContinueGame();
        }
    }

    public void GamePause()
    {
        PauseSound = GameObject.Find("Pause_Sound").GetComponent<AudioSource>();
        PauseSound.Play();
        Time.timeScale = 0; //Time.deltaTime = 0
        Music = GameObject.Find("Splashing_Around").GetComponent<AudioSource>();
        Music.Pause();
        gamestate = GameState.Pause;
        PauseButton.GetComponent<Image>().sprite = Continue;
        
    }
    public void ContinueGame()
    {
        
        Music = GameObject.Find("Splashing_Around").GetComponent<AudioSource>();
        Music.Play();
        Time.timeScale = 1;
        gamestate = GameState.Running;
        PauseButton.GetComponent<Image>().sprite = Pause;
    }

    public void OnClick_GameState()
    {
        transformGameState();
    }


}
