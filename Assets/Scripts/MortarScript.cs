using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MortarScript : MonoBehaviour
{
    public GameObject gameManager;
    public GameManagerScript gameManagerScript;
    public float timeStamp = 1f;

    public GameObject cannonBall;
    public GameObject explosionEffect;
    public AudioSource audio;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        gameManagerScript = gameManager.GetComponent<GameManagerScript>();
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timeStamp < Time.time)
        {
            GameObject clone = Instantiate(explosionEffect, transform.position, transform.rotation);
            ParticleSystem.MainModule particle = clone.GetComponent<ParticleSystem>().main;
            Destroy(clone, particle.duration);
            audio.Play();
            Instantiate(cannonBall, transform.position + new Vector3(-3.5f ,0, 10f), Quaternion.identity);
            timeStamp = Time.time + gameManagerScript.cannonFireRate;
        }
    }
}
