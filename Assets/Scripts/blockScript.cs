using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blockScript : MonoBehaviour
{
    public float _HP;
    public float _MaxHP;
    public floorManager _floorManager;
    public bool _colorChange = false;
    public float _timeStamp = 0;
    public Material _material;
    public Material _lookAtMaterial;
    public GameObject gameManager;
    public GameManagerScript gameManagerScript;
    public Material sand;
    public Material sandLookat;
    public Material dirt;
    public Material dirtLookat;
    public Material stone;
    public Material stoneLookat;
    public Material granite;
    public Material graniteLookat;
    public Material bedrock;
    public Material bedrockLookat;
    public GameObject parts;
    public string type;
    public Material crate;
    public AudioSource audio;
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        gameManagerScript = gameManager.GetComponent<GameManagerScript>();
        audio = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (_colorChange)
            if (_timeStamp < Time.time)
            {
                GetComponent<MeshRenderer>().material = _material;
                _timeStamp = 0;
                _colorChange = false;
            }
    }

    public void Init(floorManager floor, string type)
    {
        
        _floorManager = floor;
        _MaxHP = _floorManager.floor + 1;
        _HP = _floorManager.floor + 1;
        this.type = type;

        if (type == "crate")
        {
            _MaxHP = 1;
            _HP = 1;
            _material = crate;
            _lookAtMaterial = crate;
            GetComponent<MeshRenderer>().material = _material;
            return;
        }

        switch (_floorManager.floor)
        {
            case > -1 and <= 10:
                _material = sand;
                _lookAtMaterial = sandLookat;
                break;
            case > 10 and <= 20:
                _material = dirt;
                _lookAtMaterial = dirtLookat;
                break;

            case > 20 and <= 30:
                _material = stone;
                _lookAtMaterial = stoneLookat;
                break;

            case > 30 and <= 40:
                _material = granite;
                _lookAtMaterial = graniteLookat;
                break;

            default:
                _material = bedrock;
                _lookAtMaterial = bedrockLookat;
                break;
        }
        GetComponent<MeshRenderer>().material = _material;
    }

    public void make_particle()
    {
        GameObject clone = Instantiate(parts, transform.position, transform.rotation);
        ParticleSystem.MainModule particle = clone.GetComponent<ParticleSystem>().main;
        Destroy(clone, particle.duration);
    }
    public void Hit(float dmg, string type)
    {
        Invoke("make_particle", 1/gameManagerScript.pickaxeSpeed);
        if(type == "pickaxe") audio.Play();
        _HP -= dmg;
        if (_HP <= 0)
            DestroyBlock();
    }

    private void DestroyBlock()
    {
        gameManagerScript.Update_Money(type, _floorManager.floor + 1);
        _floorManager.BlockDestroyed();
        Invoke("HideBlock", 1/gameManagerScript.pickaxeSpeed);
        
        Destroy(gameObject,1f);
    }

    private void HideBlock()
    {
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        gameObject.GetComponent<Collider>().enabled = false;
    }
    public void LookAt()
    {
        GetComponent<MeshRenderer>().material = _lookAtMaterial;
        _colorChange = true;
        _timeStamp = Time.time + 0.2f;
    }
}