using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController1 : MonoBehaviour
{
    /* This script is gonna directly copy a lot from the NimSony script but
     * I now want full 360 control of both player and camera so I'm gonna get
     * rid of the section where the player moves forward with the camera's forward
     */ 

    //Component references
    public Rigidbody rb;
    Animator anim;

    public float jumpForce, speed;

    //Lifted right from https://www.youtube.com/watch?v=ORD7gsuLivE&t=524s

    //This is the parent object of the Main Camera
    public Transform camPivot;
    float heading;
    //Direct reference to the camera
    public Transform cam;

    //Movement input
    Vector2 input;

    //Now I'm taking from this https://www.youtube.com/watch?v=Gv70bd_GHkA
    Vector3 currentRotation;

    public bool grounded;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Rotate by 180 degrees per second
        heading += Input.GetAxis("Mouse X") * Time.deltaTime * 180;
        camPivot.rotation = Quaternion.Euler(0, heading, 0);

        float h = Input.GetAxisRaw("Horizontal"); float v = Input.GetAxisRaw("Vertical");

        //If the player is moving at all
        if (h != 0 || v != 0)
            anim.SetBool("IsMoving", true);
        else if (h == 0 && v == 0)
            anim.SetBool("IsMoving", false);

        input = new Vector2(h, v);
        input = Vector2.ClampMagnitude(input, 1);

        input = new Vector2(h, v);
        input = Vector2.ClampMagnitude(input, 1);

        Vector3 camF = cam.forward; //forward
        Vector3 camR = cam.right; //right

        //Because of the downward angle the camera is facing
        camF.y = 0;
        camR.y = 0;
        camF = camF.normalized;
        camR = camR.normalized;

        Vector3 movement = new Vector3(h, 0.0f, v) * Time.deltaTime * 5;

        //moving forward
        if (v > 0)
        {
            //Sets rotation to the camera pivot's forward
            //currentRotation = camPivot.eulerAngles;
            //transform.eulerAngles = currentRotation;
            transform.rotation = Quaternion.LookRotation(camF);
            movement = camF * Time.deltaTime * 5;
        }
        //moving backward
        if(v < 0)
        {
            transform.rotation = Quaternion.LookRotation(-camF);
            movement = -camF * Time.deltaTime * 5;
        }
        //moving right
        if(h > 0)
        {
            transform.rotation = Quaternion.LookRotation(camR);
            movement = camR * Time.deltaTime * 5;
        }
        //moving left
        if(h < 0)
        {
            transform.rotation = Quaternion.LookRotation(-camR);
            movement = -camR * Time.deltaTime * 5;
        }

        transform.position += movement;
    }

    void LateUpdate()
    {
        /* When the player is airborne and their velocity is 0, this means that
         * the player is at the top of their jump arc and the animator can switch
         * from the 'jump up' animation clip to the 'floating' clip. While Unity's 
         * animator doesn't detect when a float equals a specific value, we can 
         * check for when it is less than 0.
         */
        anim.SetFloat("Velocity", rb.velocity.y);
    }

    void FixedUpdate()
    {
        if (Input.GetButtonDown("Jump") && grounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            anim.SetTrigger("Jump");
        }
    }

    /* All of these collision methods are meant to determine whether the player
     * is touching the ground or not. This will help the animator know when to 
     * switch from the 'floating' clip to the 'landing' clip.
     */

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            grounded = true;
            anim.SetBool("Grounded", true);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            grounded = true;
            anim.SetBool("Grounded", true);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            grounded = false;
            anim.SetBool("Grounded", false);
        }
    }
}
