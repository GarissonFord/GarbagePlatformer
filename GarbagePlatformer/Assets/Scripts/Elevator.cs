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
        distanceFromOrigin = transform.position.y - origin;
        if (distanceFromOrigin >= maxDistance || distanceFromOrigin <= 0)
        {
            StartCoroutine(Wait());
            moveSpeed = -moveSpeed;
        }

        transform.Translate(transform.up * moveSpeed * Time.deltaTime);
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(2.0f);
    }
}
