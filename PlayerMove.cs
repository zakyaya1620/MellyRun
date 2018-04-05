using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerMove : MonoBehaviour {


  
    public static PlayerMove Instance { set; get; }

    private const float Lane_Distance = 2.5f;
    private float Timer;

    //movement
    private CharacterController controller;
    private float JumpForce = 10f;
    private float gravity = 15.0f;
    private float verticalVelocity;

    //Speed Modifier

    public float originalSpeed ;
    private float speed ;
    private float speedIncreaseLastTick;
    private float speedIncreaseTime = 2.5f;
    private float speedIncreaseAmount = 0.1f;

    private int desiredLane = 1;//0=Left,1=Middle,2=Right
    public Animator PlayerAnimator;
    private GameObject Player;
    //ProtectSkill
    public Slider MagicBar;
    private GameObject Protect;
    public bool ProtectOn;
    public Animator NoMpAnim;
  
    

    //
    public bool isRunning=false ;

    //
    public GameObject Monster;

    private void Awake()
    {
        Instance = this;
        
    }

    // Use this for initialization
    void Start () {

        controller = GetComponent<CharacterController>();
        speed = originalSpeed;
        Player = GameObject.Find("Player_dog");
        Protect = GameObject.Find("ProtectSkill");
        Protect.SetActive(false);






    }
	
	// Update is called once per frame
	void Update ()
    {


        if (!isRunning)
            return;

        if (Time.time - speedIncreaseLastTick > speedIncreaseTime)
        {
            speedIncreaseLastTick = Time.time;
            speed += speedIncreaseAmount;
            GameManager.Instance.UpdateModifier(speed-originalSpeed);
        }

        
        
        //controller.center = new Vector3(0, PlayerAnimator.GetFloat("CenterHeight"), 0);

        //Gather the inputs on whitch lane on which lane we shoudld be
        if (MobileInput.Instance.SwipeLeft&&GameManager.Instance.gamestate==GameState.Running)
            MoveLane(false);
        if (MobileInput.Instance.SwipeRight && GameManager.Instance.gamestate == GameState.Running)
            MoveLane(true);

        //Calculate where we should be in the future


        Vector3 targetPosition = transform.position.z * Vector3.forward;

        if (desiredLane == 0)
        {
            targetPosition += Vector3.left * Lane_Distance;
        }
        else if (desiredLane == 2)
            targetPosition += Vector3.right * Lane_Distance;

        //Let's calculate our move delta
        Vector3 moveVector = Vector3.zero;
        
       
            moveVector.x = (targetPosition.x - transform.position.x) * 10;
        
       
       


        //Calculate Y
        if (IsGrounded())//if Grounde
        {
            moveVector.y = -0.1f;
            if (MobileInput.Instance.SwipeUp && GameManager.Instance.gamestate == GameState.Running)
            {

                int JumpStyle = Random.Range(0, 2);
                //Jump
                verticalVelocity = JumpForce;                
                if (JumpStyle == 0) { PlayerAnimator.SetTrigger("jump"); }
                if (JumpStyle == 1) { PlayerAnimator.SetTrigger("CoolJump"); }

            }
        }
        else
        {
            
            verticalVelocity -= (gravity * Time.deltaTime*1.5f);
            //Fast Falling mechanic
            if (MobileInput.Instance.SwipeDown)
            {
                verticalVelocity = -JumpForce;
            }
        }
        moveVector.y = verticalVelocity;
        moveVector.z = speed;

        //Move the Pengu
        controller.Move(moveVector * Time.deltaTime);
        
        //Rotate the Player to where he is going
        Vector3 dir = controller.velocity;
        if (dir != Vector3.zero)
        {
            dir.y = 0;
            transform.forward = Vector3.Lerp(transform.forward, dir, 1f);
        }
        

        if (MobileInput.Instance.SwipeDown && GameManager.Instance.gamestate == GameState.Running)
        {
           

            
                PlayerAnimator.SetTrigger("Slide");
            

           
        }
        //Monster Follow
        MonsterFollow();

      
            if (ProtectOn ==true)
            {
                
                Timer += 1f * Time.deltaTime;
                if (Timer>= 2f)
                {

                    Protect.SetActive(false);
                    Debug.Log("Off");
                    Timer = 0;
                     ProtectOn = false;






            }

                



            }
            
        
       
    

    }

   




    private void MoveLane(bool goingRight)
    {
        desiredLane += (goingRight) ? 1 : -1;
        desiredLane = Mathf.Clamp(desiredLane, 0, 2);
    }

    private bool IsGrounded()
    {
        Ray groundRay = new Ray(
            new Vector3(controller.bounds.center.x,
            (controller.bounds.center.y - controller.bounds.extents.y) + 0.2f,
            controller.bounds.center.z),
            Vector3.down);
        Debug.DrawRay(groundRay.origin, groundRay.direction, Color.cyan, 1.0f);
        return (Physics.Raycast(groundRay, 0.2f + 0.1f));

        
    }

    public void StarRunning()
    {
        isRunning = true; 
    }
   
   

    public void Crash()
    {
        PlayerAnimator.SetTrigger("Death");
        isRunning = false;
        GameManager.Instance.OnDeath();
    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        switch (hit.gameObject.tag)
        {

            case "Obstacle":
                Crash();
                break;
            default:
                break;
        }
    }

    private void FixedUpdate()
    {
        controller.height = PlayerAnimator.GetFloat("SlideHeight");
    }

    
    private void MonsterFollow()
    {        
            Monster.transform.position = new Vector3(0, 7, this.transform.position.z + 27);
               
    }


    //Button click
    public void ProtectMagic()
    {


        if (MagicBall.Instance.MagicProtect != false && ProtectOn == false)
        {
            Animator decease = GameObject.Find("Value Descrease").GetComponent<Animator>();
            
            ProtectOn = true;            
            Protect.SetActive(true);
            if (MagicBar.value >= 30)
            {

                MagicBar.value = Mathf.MoveTowards(MagicBar.value, MagicBar.value -30f, 100f);
                decease.SetTrigger("show");
                
            }
           






        }
        else if (MagicBall.Instance.MagicProtect == false)
        {
            Debug.Log("No mp");
            NoMpAnim.SetTrigger("Show");
        }
    }

    


}
