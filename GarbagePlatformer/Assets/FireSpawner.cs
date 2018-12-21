using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSpawner : MonoBehaviour
{
    public float timeSinceLastSpawn, timeBetweenSpawns;
    //Y will be constant
    public float x, y, z; 
    //The range at which we randomly spawn fireballs
    public float xMin, xMax, yMin, yMax, zMin, zMax;
    public ParticleSystem fireball;

    //public GameObject parent;

	// Use this for initialization
	void Start ()
    {
       
	}
	
	// Update is called once per frame
	void Update ()
    {
        timeSinceLastSpawn += Time.deltaTime;
        x = Random.Range(xMin, xMax);
        y = Random.Range(yMin, yMax);
        z = Random.Range(zMin, zMax);

        if (timeSinceLastSpawn > timeBetweenSpawns)
        {
            Instantiate(fireball, this.transform.position, new Quaternion(x, y, z, this.transform.rotation.w));
            timeSinceLastSpawn = 0.0f;
        }
	}
}
