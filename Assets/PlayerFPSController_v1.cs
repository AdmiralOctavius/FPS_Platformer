using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFPSController_v1 : MonoBehaviour {

    //Ver1 variables
    public float speed;
    public float jumpForce;
    public float mouseXaxis;
    public float mouseYaxis;
    //public float mouseSpeed;
    public float horizontalSpeed = 2.0f;
    public float verticalSpeed = -2.0f;
    public GameObject playerCamera;

    // Use this for initialization
    void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        //This is move code
        //Pulled from: https://docs.unity3d.com/ScriptReference/Input.GetAxis.html

        /*mouseXaxis = Input.GetAxis("Vertical") * mouseSpeed;
        mouseYaxis = Input.GetAxis("Horizontal") * mouseSpeed;

        mouseXaxis *= Time.deltaTime;
        mouseYaxis *= Time.deltaTime;

        transform.Translate(0, 0, mouseXaxis);
        transform.Rotate(0, mouseYaxis, 0);
        Debug.Log(Input.GetAxis("Vertical").ToString());*/




        // Get the mouse delta. This is not in the range -1...1
        mouseXaxis = horizontalSpeed * Input.GetAxis("Mouse X");
        mouseYaxis = verticalSpeed * Input.GetAxis("Mouse Y");

        transform.Rotate(0, mouseXaxis, 0);
        playerCamera.GetComponent<Transform>().transform.Rotate(mouseYaxis, 0, 0);
    }  
}
