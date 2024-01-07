using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class floorManager : MonoBehaviour
{
    public bool _wasBlockDestroyed = false;
    public int xSize = 100;
    public int floor;
    public int nsize = 4;
    public int zSize = 100;
    public bool initiated = false;
    public GameObject floorPref;
    public GameObject blockPref;
    public GameObject chestPref;
    public GameObject borderPref;
    public GameObject gameManager;
    public GameManagerScript gameManagerScript;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Init(int floor)
    {
        if (initiated) return;

        initiated = true;
        this.floor = floor;
        for (int x = -1; x <= xSize; x++)
        {
            for (int z = -1; z <= zSize; z++)
            {
                Vector3 temp = transform.position + new Vector3(x * nsize, 0, z * nsize);
                if (x == -1 || z == -1 || x == xSize || z == zSize)
                {
                    Instantiate(borderPref, temp, Quaternion.identity, this.transform);
                    Debug.Log("border");
                }
                else
                {
                    gameManager = GameObject.FindGameObjectWithTag("GameManager");
                    gameManagerScript = gameManager.GetComponent<GameManagerScript>();
                    if (gameManagerScript.Will_crate_next())
                    {
                        Debug.Log("crate");
                        blockScript newBlock =
                            Instantiate(blockPref, temp, Quaternion.identity, this.transform)
                                .GetComponent<blockScript>();
                        newBlock.Init(this.GetComponent<floorManager>(), "crate");
                    }
                    else
                    {
                        Debug.Log("newblock");
                        blockScript newBlock =
                            Instantiate(blockPref, temp, Quaternion.identity, this.transform)
                                .GetComponent<blockScript>();
                        newBlock.Init(this.GetComponent<floorManager>(), "block");
                    }
                }
                
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
    }

    private void MakeNextFloor()
    {
        var nextFloor = Instantiate(floorPref, transform.position + Vector3.down * nsize, Quaternion.identity).GetComponent<floorManager>();
        nextFloor.name = "floor" + (floor + 1).ToString();
        nextFloor.Init(floor + 1);
        nextFloor.floorPref = floorPref;
    }

    public void BlockDestroyed()
    {
        if (_wasBlockDestroyed) return;
        _wasBlockDestroyed = true;
        MakeNextFloor();
    }
}