using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBallScript : MonoBehaviour
{
    public GameObject gameManager;
    public GameManagerScript gameManagerScript;
    public float speed = 30f;
    public bool falling = false;
    public GameObject explosionEffect;
    public blockScript target = null;
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
        if (!falling) transform.position += new Vector3(0, 1 * speed, -1 * speed) * Time.deltaTime;
        else transform.position += new Vector3(0, -1 * speed * 2, 0) * Time.deltaTime;
        if (!falling && transform.position.y > 200)
        {
            falling = true;
            Vector3 temp = gameManagerScript.MineBeginSpawn.transform.position;
            Vector3 hitPlace = new Vector3(temp.x + Random.Range(2.0f, gameManagerScript.MineSizeN), 200, temp.z + Random.Range(2.0f, gameManagerScript.MineSizeN));
            RaycastHit hit;
            if (Physics.Raycast(hitPlace, new Vector3(0,-1,0), out hit, 1000000))
            { 
                Debug.Log("Do dmg");
                target = hit.transform.GetComponent<blockScript>();
                if (target != null)
                {
                    transform.position = hitPlace + new Vector3(-3.5f, 0, 10f);
                }
            }
            
        }

        if(target != null)
        {
            if (transform.position.y <= target.transform.position.y)
            {
                speed = 0f;
                target.Hit(gameManagerScript.cannonDmg, "cannonball");
                
                GameObject clone = Instantiate(explosionEffect, target.transform.position, target.transform.rotation);
                ParticleSystem.MainModule particle = clone.GetComponent<ParticleSystem>().main;
                Destroy(clone, particle.duration);
                target = null;
                audio.Play();
                Destroy(gameObject,0.5f);
            }
        }
       
    }
}
