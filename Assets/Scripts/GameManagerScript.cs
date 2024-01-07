
using UnityEditor;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    //cannonpoint
    public GameObject cannonspawn;
    public GameObject cannon;
    public GameObject floor;
    public floorManager floorManager;
    
    public float next_point = 0f;
    //Money
    public double money = 0f;

    //PickAxe Stats
    public float pickaxeDmg = 1f;
    public float pickaxeSpeed = 4f;
    public float pickaxeStageMax = 4;
    public float pickaxeStage = 0f;
    public float pickaxeRange = 10f;

    //Cannon Stats
    public float cannonFireRate = 2f;
    public float cannonDmg = 2f;
    public float numberOfCannonsMax = 5f;
    public float numberOfCannons = 0f;

    //Player Stats
    public float playerSpeed = 12f;
    public float playerJump = 3f;

    //Mine Generator Stats
    public GameObject MineBeginSpawn;
    public float MineSizeN = 60f;
    public int oreValue = 2;
    public int crateValue = 10;
    public float crateChange = 3f;

    void Start()
    {
      
        floor = GameObject.FindGameObjectWithTag("Floor");
        floorManager = floor.GetComponent<floorManager>();
        floorManager.Init(0);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Equals))
        {
            money += 10000;
        }
    }

    //Pickaxe Upgrades
    public double Level_up_pickaxe_dmg(double price)
    {
        if(money > price)
        {
            money -= price;
            pickaxeDmg *= 2f;
            return price * 2;
        }
        return price;
    }

    public double Level_up_pickaxe_speed(double price)
    {
        if (money > price)
        {
            money -= price;
            pickaxeSpeed *= 1.20f;
            return price * 2;
        }
        return price;
    }
    public double Level_up_pickaxe_range(double price)
    {
        if (money > price)
        {
            money -= price;
            pickaxeRange += 1f;
            return price * 2;
        }
        return price;
    }
    public double Level_up_pickaxe_stage(double price)
    {
     
        if (pickaxeStage < pickaxeStageMax)
        {
            if (money > price)
            {
                money -= price;
                pickaxeStage += 1;
                if (pickaxeStage == pickaxeStageMax) return -1;
                return price * 10;
            }
            return price;
        }
        return -1;
    }
    //Cannon Upgrades
    public double Level_up_cannon_dmg(double price)
    {
        if (money > price)
        {
            money -= price;
            cannonDmg *= 2f;
            return price * 2;
        }
        return price;
    }

    public double Level_up_cannon_fire_rate(double price)
    {
        if (money > price)
        {
            money -= price;
            cannonFireRate /= 1.2f;
            return price * 2;
        }
        return price;
    }

    public double More_cannons(double price)
    {
        if (numberOfCannons < numberOfCannonsMax)
        {
            if (money > price)
            {
                money -= price;
                numberOfCannons += 1;
                Instantiate(cannon, cannonspawn.transform.position + new Vector3(next_point, 0, 0), cannonspawn.transform.rotation);
                next_point += 10f;
                if (numberOfCannons == numberOfCannonsMax) return -1;
                return price * 2;
            }
            return price;
        }
        
        return -1;
    }

    //crate chance
    public double Level_up_crate_chance(double price)
    {
        if (money > price)
        {
            money -= price;
            crateChange += 1f;
            return price * 2;
        }
        return price;
    }

    public void Update_Money(string type, int mineDeepness)
    {
        if (type == "crate") money += mineDeepness * crateValue;
        else money += mineDeepness * oreValue;
    }

    public bool Will_crate_next()
    {
       return Random.Range(0.0f, 100.0f) < crateChange;
    }
}
