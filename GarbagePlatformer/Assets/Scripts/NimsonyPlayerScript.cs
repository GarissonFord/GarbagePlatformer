using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NimsonyPlayerScript : MonoBehaviour
{
    public Rigidbody rb;
    public float jumpForce;

    //Lifted right from https://www.youtube.com/watch?v=ORD7gsuLivE&t=524s

    public Transform camPivot;
    float heading;
    public Transform cam;

    Vector2 input;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update ()
    {
        //Rotate by 180 degrees per second
        heading += Input.GetAxis("Mouse X") * Time.deltaTime * 180;
        camPivot.rotation = Quaternion.Euler(0, heading, 0);

        input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        input = Vector2.ClampMagnitude(input, 1);

        Vector3 camF = cam.forward; //forward
        Vector3 camR = cam.right; //right

        //Because of the downward angle the camera is facing
        camF.y = 0;
        camR.y = 0;
        camF = camF.normalized;
        camR = camR.normalized;

        //transform.position += new Vector3(input.x, 0.0f, input.y) * Time.deltaTime * 5;
        transform.position += (camF * input.y + camR * input.x) * Time.deltaTime * 5;
    }

    void FixedUpdate()
    {
        if (Input.GetButtonDown("Jump"))
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
}
