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
    public GameObject firePoint;

    //Ver2 variables
    public float currentview;

    //Ver3 Variables
    public RaycastHit fireCast;
    public float explosionForceVar;
    public float explosionRadius;

    //Ver4 Variables
    //Was a big dummy and needed new speed variables for mouse movement
    public float horizontalMouseSpeed = 2.0f;
    public float verticalMouseSpeed = -2.0f;
    //Starting gravity is Y -9.81
    // Use this for initialization
    void Start () {

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void FixedUpdate()
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
        mouseXaxis = horizontalMouseSpeed * Input.GetAxis("Mouse X");
        mouseYaxis = verticalMouseSpeed * Input.GetAxis("Mouse Y");

        transform.Rotate(0, mouseXaxis, 0);
        //playerCamera.GetComponent<Transform>().transform.Rotate(mouseYaxis, 0, 0);
        //playerCamera.GetComponent<Transform>().transform.localEulerAngles.y 

        currentview = Mathf.Clamp(mouseYaxis, -60, 60);
        //currentview = mouseYaxis;

        //playerCamera.GetComponent<Transform>().transform.localEulerAngles = new Vector3(currentview, playerCamera.GetComponent<Transform>().transform.localEulerAngles.y);
        playerCamera.GetComponent<Transform>().transform.Rotate(currentview, 0, 0);

        if(playerCamera.GetComponent<Transform>().transform.localEulerAngles.x >= 60f && playerCamera.GetComponent<Transform>().transform.localEulerAngles.x <= 300f)
        {
            if (playerCamera.GetComponent<Transform>().transform.localEulerAngles.x > 60f)
            {
                if (playerCamera.GetComponent<Transform>().transform.localEulerAngles.x > 300f)
                {
                    //Debug.Log("Do nothing");
                }
                else if (playerCamera.GetComponent<Transform>().transform.localEulerAngles.x > 60f && playerCamera.GetComponent<Transform>().transform.localEulerAngles.x < 260f)
                {
                    //Debug.Log("Got here with x value of: " + playerCamera.GetComponent<Transform>().transform.localEulerAngles.x);
                    playerCamera.GetComponent<Transform>().transform.localEulerAngles = new Vector3(60f, playerCamera.GetComponent<Transform>().transform.localEulerAngles.y);
                }
                else if (playerCamera.GetComponent<Transform>().transform.localEulerAngles.x < 300f && playerCamera.GetComponent<Transform>().transform.localEulerAngles.x > 260f)
                {
                  //  Debug.Log("Got here with x value of: " + playerCamera.GetComponent<Transform>().transform.localEulerAngles.x);
                    playerCamera.GetComponent<Transform>().transform.localEulerAngles = new Vector3(300f, playerCamera.GetComponent<Transform>().transform.localEulerAngles.y);
                }
            }
            else
            {
                //Debug.Log("Got here with x value of: " + playerCamera.GetComponent<Transform>().transform.localEulerAngles.x);
                playerCamera.GetComponent<Transform>().transform.localEulerAngles = new Vector3(-60f, playerCamera.GetComponent<Transform>().transform.localEulerAngles.y);
            }

        }

        if (Input.GetAxis("Vertical") != 0)
        {
            Debug.Log("Moving forward or backward");
            if(Input.GetAxis("Vertical") > 0)
            {
                this.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0, 0, verticalSpeed));
            }
            else
            {
                this.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0, 0, -verticalSpeed));
            }
            
        }
        if(Input.GetAxis("Horizontal") != 0)
        {
            Debug.Log("Moving left or Right");
            //this.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(horizontalSpeed, 0, 0));

            if (Input.GetAxis("Horizontal") > 0)
            {
                this.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(horizontalSpeed, 0, 0));
            }
            else    
            {
                this.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(-horizontalSpeed, 0, 0));
            }
        }

        if(Input.GetAxis("Fire1") > 0)
        {
            Debug.Log("Firing");
            //firePoint.GetComponent<Transform>()
            if(Physics.Raycast(firePoint.GetComponent<Transform>().localPosition,firePoint.GetComponent<Transform>().forward, out fireCast, 10f))
            {
                Debug.Log("Got spot on wall");
                //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * fireCast.distance, Color.yellow);
                Debug.DrawRay(firePoint.GetComponent<Transform>().localPosition, firePoint.GetComponent<Transform>().forward * fireCast.distance, Color.yellow);
                Debug.Log(fireCast.distance);
                if (fireCast.distance <= 7f)
                {
                    Debug.Log("In Distance, commencing movement");
                    this.GetComponent<Rigidbody>().AddExplosionForce(explosionForceVar, fireCast.point, explosionRadius);
                }
                
            }
            
        }

        if(Input.GetButtonDown("Jump"))
        {
            Debug.Log("Got Jump");
            this.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0, jumpForce, 0));
        }

    }  
}
