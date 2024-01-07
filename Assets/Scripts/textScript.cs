using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class textScript : MonoBehaviour
{
    public GameObject camera;


    // Start is called before the first frame update
    void Start()
    {
        camera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = camera.transform.rotation;
    }
    
    
}
