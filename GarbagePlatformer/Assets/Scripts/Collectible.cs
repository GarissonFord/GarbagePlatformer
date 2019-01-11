using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    Renderer rend;
    public bool collected;

    public GameController gc;

    public AudioSource audio;

    // Start is called before the first frame update
    void Start()
    {
        collected = false;
        rend = GetComponent<Renderer>();
        gc = FindObjectOfType<GameController>();
        audio = GetComponent<AudioSource>();
    }

    /*
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !collected)
        {
            collected = true;
            //rend.material.shader = Shader.Find("_Color");
            //rend.material.SetColor("_Color", new Color(50.0f, 50.0f, 50.0f, 0.0f));
            //gc.CollectiblePickedUp();
            Destroy(gameObject);
        }
    }
    */
}
