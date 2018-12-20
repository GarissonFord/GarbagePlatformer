using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody rb;

    public float x, z;
    public float speed;

    public GameObject cameraBase;

    float distance = 5.0f;

	// Use this for initialization
	void Start ()
    {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");
        
        Vector3 movement = new Vector3(x, 0.0f, z);

        /*
        if (z != 0)
        {
            
            //float difference = cameraBase.transform.localEulerAngles.y - transform.localEulerAngles.y;
            //transform.Rotate(0.0f, difference, 0.0f, Space.World);
            movement += cameraBase.transform.forward;
            transform.LookAt(cameraBase.transform.forward);
            //rb.AddRelativeForce(cameraBase.transform.forward * speed);        
            //transform.Rotate(Vector3.up * Time.deltaTime);
        }
        */
        //transform.position += movement * speed * Time.deltaTime;
        rb.AddForce(movement * speed);
        //rb.MovePosition
    }

        

    void FixedUpdate()
    {

    }
}
