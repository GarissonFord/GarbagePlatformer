using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMove : MonoBehaviour
{
    Rigidbody rb;

    public float moveSpeed;
    public float origin;
    public float distanceFromOrigin;
    public float maxDistance;

    private void Start()
    {
        //Start at the 'origin'
        rb = GetComponent<Rigidbody>();
        origin = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        distanceFromOrigin = transform.position.z;
        if (distanceFromOrigin >= origin + maxDistance || distanceFromOrigin <= origin - maxDistance)
            moveSpeed = -moveSpeed;

        transform.Translate(transform.forward * moveSpeed * Time.deltaTime);
    }
}
