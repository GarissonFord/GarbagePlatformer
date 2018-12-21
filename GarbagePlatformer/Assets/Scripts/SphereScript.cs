using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereScript : MonoBehaviour {

    public float timeSinceSpawn;
    public float destroyTime;

    public ParticleSystem particle;

    void Awake()
    {
        timeSinceSpawn = 0.0f;
    }

    // Update is called once per frame
    void Update ()
    {
        timeSinceSpawn += Time.deltaTime;
        if (timeSinceSpawn >= destroyTime)
        {
            Instantiate(particle, this.transform.position, this.transform.rotation);
            Destroy(gameObject);
        }
	}
}
