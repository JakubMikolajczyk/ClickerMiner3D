using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestOre : MonoBehaviour
{
    public Material selfColor;
    public bool color_changed = false;
    public float timeStamp = 1f;
    public Color colortest = Color.blue;
    public float coolDown = 1f;
    // Start is called before the first frame update
    void Start()
    {
        selfColor = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeStamp < Time.time) color_changed = false;
        if (!color_changed) selfColor.color = Color.white;
    }
    public void Change_Color()
    {
        if (!color_changed)
        {
            color_changed = true;
            timeStamp = Time.time + coolDown;
            selfColor.color = colortest;
        }
        else
        {
            timeStamp = Time.time + coolDown;
        }
        
    }
    public void Destroy_Test(float x)
    {
        Debug.Log(x);
        Destroy(gameObject);
    }

    public void Use(float x)
    {
        Debug.Log(x);
        colortest = Color.red;
    }
}

