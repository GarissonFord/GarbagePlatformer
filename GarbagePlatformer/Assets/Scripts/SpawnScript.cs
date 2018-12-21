using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnScript : MonoBehaviour
{
    public float timeSinceLastSpawn, timeBetweenSpawns;
    public GameObject go;

    private void Start()
    {
        Instantiate(go, this.transform.position, this.transform.rotation);
    }

    // Update is called once per frame
    void Update ()
    {
        timeSinceLastSpawn += Time.deltaTime;
        
        if (timeSinceLastSpawn > timeBetweenSpawns)
        {
            Instantiate(go, this.transform.position, this.transform.rotation);
            timeSinceLastSpawn = 0.0f;
        }
	}
}
