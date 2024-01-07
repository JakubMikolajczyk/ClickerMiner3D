using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum ItemType
{
    PickaxeDmg,
    PickaxeSpeed,
    Stage,
    Range,
    CannonFireRate,
    CannonDMG,
    NumberOfCannons,
    CreateChance
};
public class shopScript : MonoBehaviour
{

    public ItemType itemType;
    public TextMeshPro text;
    private GameObject gameManager;
    private GameManagerScript gameManagerScript;
    private AudioSource audio;
    private string _name;
    private double _price;
    private float _stat;
    private bool _colorChange = false;
    private float _timeStamp = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        gameManagerScript = gameManager.GetComponent<GameManagerScript>();
        audio = GetComponent<AudioSource>();
        _price = 25;
        switch (itemType)
        {
            case ItemType.PickaxeDmg:
                _name = "PickaxeDMG"; 
                break;
            
            case ItemType.PickaxeSpeed:
                _name = "PickaxeSpeed";
                break;
            case ItemType.Stage:
                _name = "Stage";
                _price = 1000;
                break;
            case ItemType.Range:
                _name = "Range";
                break;
            case ItemType.CannonFireRate:
                _name = "CannonFireRate";
                break;
            case ItemType.CannonDMG:
                _name = "CannonDMG";
                break;
            case ItemType.NumberOfCannons:
                _name = "NumberOfCannons";
                break;
            case ItemType.CreateChance:
                _name = "CreateChance";
                break;
            
        }
        update_stats();
        UpdateText();
        
    }

    void Update()
    {
        if (_colorChange)
            if (_timeStamp < Time.time)
            {
                _colorChange = false;
                text.color = Color.white;
            }

    }

    // Update is called once per frame
    void UpdateText()
    {
        String temp = _name + "\n Actual value: " + _stat;
        if(_price != -1) text.text = temp + "\n Price for upgrade: " + _price.ToString();
        else text.text = temp;
    }

    private void update_stats()
    {
        switch (itemType)
        {
            case ItemType.PickaxeDmg:
                _stat = gameManagerScript.pickaxeDmg;
                break;
            case ItemType.PickaxeSpeed:
                _stat = gameManagerScript.pickaxeSpeed;
                break;
            case ItemType.Stage:
                _stat = gameManagerScript.pickaxeStage;
                break;
            case ItemType.Range:
                _stat = gameManagerScript.pickaxeRange;
                break;
            case ItemType.CannonFireRate:
                _stat = gameManagerScript.cannonFireRate;
                break;
            case ItemType.CannonDMG:
                _stat = gameManagerScript.cannonDmg;
                break;
            case ItemType.NumberOfCannons:
                _stat = gameManagerScript.numberOfCannons;
                break;
            case ItemType.CreateChance:
                _stat = gameManagerScript.crateChange;
                break;

        }
    }

    public void UseIt()
    {
        double previous_price = _price;
        switch (itemType)
        {
            case ItemType.PickaxeDmg:
                _price = gameManagerScript.Level_up_pickaxe_dmg(_price);
                break;
            case ItemType.PickaxeSpeed:
                _price = gameManagerScript.Level_up_pickaxe_speed(_price);
                break;
            case ItemType.Stage:
                _price = gameManagerScript.Level_up_pickaxe_stage(_price);
                break;
            case ItemType.Range:
                _price = gameManagerScript.Level_up_pickaxe_range(_price);
                break;
            case ItemType.CannonFireRate:
                _price = gameManagerScript.Level_up_cannon_fire_rate(_price);
                break;
            case ItemType.CannonDMG:
                _price = gameManagerScript.Level_up_cannon_dmg(_price);
                break;
            case ItemType.NumberOfCannons:
                _price = gameManagerScript.More_cannons(_price);
                break;
            case ItemType.CreateChance:
                _price = gameManagerScript.Level_up_crate_chance(_price);
                break;
        }
        if (_price != previous_price) audio.Play();
        update_stats();
        UpdateText();
    }

    public void LookAt()
    {
        if (_price < gameManagerScript.money) text.color = Color.green;
        else text.color = Color.red;
        _colorChange = true;
        _timeStamp = Time.time + 0.2f;
    }
}
