using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NimsonyPlayerScript : MonoBehaviour
{
    //Component references
    public Rigidbody rb;
    Animator anim;
    public AudioSource audio;

    public AudioClip jumpAudio, landingAudio;

    public float jumpForce, speed;

    //Lifted right from https://www.youtube.com/watch?v=ORD7gsuLivE&t=524s

    //This is the parent object of the Main Camera
    public Transform camPivot;
    //For horizontal rotation
    float heading;
    //For vertical rotation
    float heading2;
    //Direct reference to the camera
    public Transform cam;

    //Movement input
    Vector2 input;

    //Now I'm taking from this https://www.youtube.com/watch?v=Gv70bd_GHkA
    public Vector3 currentRotation;

    public bool grounded;

    //For when attaching to platforms
    public Vector3 scale;

    public float h, v;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
        scale = transform.localScale;
    }

    void Update ()
    {
        //Mouse input
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        //Stick input
        float inputX = Input.GetAxis("RightStickHorizontal"), inputZ = Input.GetAxis("RightStickVertical");
        //Apply the X rotation
        //Using either mouseX or inputX 
        heading += (mouseX + inputX) * Time.deltaTime * 180;
        heading2 += (mouseY + inputZ) * Time.deltaTime * 180;
        camPivot.rotation = Quaternion.Euler(heading2, heading, 0);

        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        //If the player is moving at all
        if (h != 0 || v != 0)
            anim.SetBool("IsMoving", true);
        else if (h == 0 && v == 0)
            anim.SetBool("IsMoving", false);
      
        Vector3 movement = new Vector3(inputX, 0.0f, inputZ);  
        
        input = new Vector2(h, v);
        input = Vector2.ClampMagnitude(input, 1);

        Vector3 camF = cam.forward; //forward
        Vector3 camR = cam.right; //right

        //Because of the downward angle the camera is facing
        camF.y = 0;
        camR.y = 0;
        camF = camF.normalized;
        camR = camR.normalized;

        // v > 0 Means the player's input is to move forward

        if (v != 0)
        {
            //Sets rotation to the camera pivot's forward
            currentRotation = camPivot.eulerAngles;
            transform.eulerAngles = currentRotation;
        } 
        
        //Walking backwards
        if (v < 0)
            anim.SetBool("MovingBackwards", true);
        else if (v >= 0)
            anim.SetBool("MovingBackwards", false);

        //Final movement update
        transform.position += (camF * input.y + camR * input.x) * Time.deltaTime * 5;
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
            audio.clip = jumpAudio;
            audio.Play();
            anim.SetTrigger("Jump");
        }
    }

    /* All of these collision methods are meant to determine whether the player
     * is touching the ground or not. This will help the animator know when to 
     * switch from the 'floating' clip to the 'landing' clip.
     */
     
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Moving Ground")) 
        {
            grounded = true;
            audio.clip = landingAudio;
            audio.Play();
            anim.SetBool("Grounded", true);
        }
    }

    private void OnCollisionStay(Collision collision )
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Moving Ground"))
        {
            grounded = true;
            anim.SetBool("Grounded", true);
        }

        //The player doesn't move with the platform unless they are a child of it
        //BUT the player character's transform is being altered when this happens, so bricking for the moment
        /*
        if (collision.gameObject.CompareTag("Moving Ground"))
        {
            transform.parent = collision.gameObject.transform;
            //transform.localScale = scale;
        }
        */
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Moving Ground"))
        {
            grounded = false;
            anim.SetBool("Grounded", false);
        }

        //The player doesn't move with the platform unless they are a child of it
        /*
        if (collision.gameObject.CompareTag("Moving Ground"))
            transform.parent = null;
         */
    }
}
