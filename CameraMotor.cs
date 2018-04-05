using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotor : MonoBehaviour {

    public static CameraMotor Instance { set; get; }

    public Transform LookAt;//Our Cat //object we're looking at
    private Vector3 offset = new Vector3(0, 7f, -5);
    private Vector3 rotation = new Vector3(23, 0, 0);
    private float Timer;
    

    public bool IsMoving { set; get; }


    private void Awake()
    {
        Instance = this;
        
    }


    public void Update()
    {

        


    }




    private void LateUpdate()
    {

        if (!IsMoving)
            return;


       
        CamerTransform();
        
        transform.rotation = Quaternion.Lerp(transform.rotation,Quaternion.Euler(rotation),0.1f);
    }

    public void CamerTransform()
    {
        Vector3 desiredPosition = LookAt.position + offset;
        //desiredPosition.x = 0;


        transform.position = Vector3.Lerp(transform.position, desiredPosition, 5 * Time.deltaTime);
    }
    

    public void CameraChange()
    {

        Debug.Log("shit");
        offset = new Vector3(0, 4, -7);
        rotation = new Vector3(10, 0, 0);

    }
    public void CamerChangeBack()
    {
        Debug.Log("Back");
        offset = new Vector3(0, 7, -5);
        rotation = new Vector3(23, 0, 0);
    }
   
    




}
