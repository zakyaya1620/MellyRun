using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileInput : MonoBehaviour {


    public static MobileInput Instance { set; get; }

    private bool tap, swipeLeft, swipeRight, swipeUP, swipeDown;
    private Vector2 swipeDelta, starTouch;
    private const float DEADZONE = 100.0f;

    public bool Tap { get { return tap; } }//?????
    public Vector2 SwipeDeta { get { return swipeDelta; } }
    public bool SwipeLeft { get { return swipeLeft; } }
    public bool SwipeRight { get { return swipeRight; } }
    public bool SwipeUp { get { return swipeUP; } }
    public bool SwipeDown { get { return swipeDown; } }
    public Canvas canvas;


    private void Awake()
    {
        Instance = this;
    }


    private void Start()
    {
        
    }

    private void Update()
    {
        //Reseting all the booleans
        tap = false;
        swipeLeft = false;
        swipeRight = false;
        swipeDown = false;
        swipeRight = false;
        swipeUP = false;
        //Let's check for inputs!

        #region Standalone Inputs

        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Fuck You Bitch");
            tap = true;
            starTouch = Input.mousePosition;
           // canvas.GetComponent<Canvas>().renderMode = RenderMode.WorldSpace;

            //canvas.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;

        }
        else if (Input.GetMouseButtonUp(0))
        {
            starTouch = swipeDelta = Vector2.zero;
        }


        #endregion
        #region Mobile Inputs

        if (Input.touches.Length != 0)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                tap = true;
                starTouch = Input.mousePosition;
            }
            else if (Input.touches[0].phase == TouchPhase.Ended|| Input.touches[0].phase == TouchPhase.Canceled)
            {
                starTouch = swipeDelta = Vector2.zero;
            }
        }



        #endregion

        //calculate distance
        swipeDelta = Vector2.zero;
        if (starTouch != Vector2.zero)
        {
            //Let's check witch mobile
            if (Input.touches.Length != 0)
            {
                swipeDelta = Input.touches[0].position - starTouch;
            }
            //Let'ss check with standalone
            else if (Input.GetMouseButton(0))
            {
                swipeDelta = (Vector2)Input.mousePosition - starTouch;
            }
        }

        //Let's check if we're beyond the deadzone
        if (swipeDelta.magnitude > DEADZONE)
        {
            //this is confirm swipe
            float x = swipeDelta.x;
            float y = swipeDelta.y;
            if (Mathf.Abs(x) > Mathf.Abs(y))
            {
                //Left or right
                if (x < 0)
                    swipeLeft = true;
                else
                    swipeRight = true;
            }
            else
            {
                //Up or down
                if (y< 0)
                    swipeDown = true;
                else
                    swipeUP = true;
            }

            starTouch = swipeDelta = Vector2.zero;
        }
        
       
    }

    


}
