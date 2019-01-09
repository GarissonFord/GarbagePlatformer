using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    public float moveSpeed;
    public float origin;
    public float distanceFromOrigin;
    public float maxDistance;

    private void Start()
    {
        origin = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        distanceFromOrigin = Mathf.Abs(transform.position.y - origin);
        if (distanceFromOrigin >= maxDistance /*|| distanceFromOrigin <= 0*/)
        {
            //Elevators get stuck in one place, will get back to
            //StartCoroutine(Wait());
            moveSpeed = -moveSpeed;
        }

        transform.Translate(transform.up * moveSpeed * Time.deltaTime);
    }

    private IEnumerator Wait()
    {
        float oldSpeed = moveSpeed;
        moveSpeed = 0.0f;
        yield return new WaitForSeconds(2.0f);
        moveSpeed = oldSpeed;
    }
}
