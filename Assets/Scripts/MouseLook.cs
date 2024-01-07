using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MouseLook : MonoBehaviour
{

    public float mouseSensitivity = 100f;
    public float range = 5f;
    public bool use_it_clicked = false;
    public float timeStamp = 1f;

    public Animator pickaxeAnim;

    public GameObject pickaxe;
    public Material woodMaterial;
    public Material stoneMaterial;
    public Material metalMaterial;
    public Material goldMaterial;
    public Material lavaMaterial;

    public Transform playerBody;

    public GameObject gameManager;
    public GameManagerScript gameManagerScript;

  
    public Text money_text;
    public Text hp_text;

    float xRotation = 0f;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        gameManagerScript = gameManager.GetComponent<GameManagerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        money_text.text = "Money: " + gameManagerScript.money;
        if (Input.GetKeyDown("r")) Update_Stats();

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        if (Input.GetButtonDown("Fire1") && pickaxeAnim.GetCurrentAnimatorStateInfo(0).IsName("PickaxeDefault"))
        {
            Try_do_dmg();
        }
        else if (!use_it_clicked && Input.GetButtonDown("Fire2") && pickaxeAnim.GetCurrentAnimatorStateInfo(0).IsName("PickaxeDefault"))
        {
            use_it_clicked = true;
            timeStamp = Time.deltaTime;
            Use_it();
           
        }
        else
        {
           Select();
        }


        Update_Stats();
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        //pickaxe.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        playerBody.Rotate(Vector3.up * mouseX);
        if(use_it_clicked && Time.time > timeStamp + 2f)
        {
            use_it_clicked = false;
        } 
    }

    private void Select()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.forward, out hit, range)){
            blockScript target = hit.transform.GetComponent<blockScript>();
            if (target != null)
            {
                hp_text.text = target._HP + "/" + target._MaxHP;
                target.LookAt();
            }
            else {
                hp_text.text = "";
                shopScript target2 = hit.transform.GetComponent<shopScript>();
                if (target2 != null)
                {
                    target2.LookAt();
                }
            }
        }

    }

    private void Try_do_dmg()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, range)){
            pickaxeAnim.Play("PickaxeHit");
            Debug.Log("Do dmg");
            blockScript target = hit.transform.GetComponent<blockScript>();
            if (target != null)
            {
                target.LookAt();
                target.Hit(gameManagerScript.pickaxeDmg, "pickaxe");
            }
        }
    }

    private void Use_it()
    {
        RaycastHit hit;
       
        Debug.Log("use_it_clicked: " + use_it_clicked);
        if (Physics.Raycast(transform.position, transform.forward, out hit, range))
        {
            Debug.Log("Use it");
            shopScript target = hit.transform.GetComponent<shopScript>();
            if (target != null)
            {
                target.UseIt();
            }
        }
    }

    public void Update_Stats()
    {
        pickaxeAnim.speed = gameManagerScript.pickaxeSpeed;
        range = gameManagerScript.pickaxeRange;

        switch (gameManagerScript.pickaxeStage)
        {
            case 0:
                pickaxe.GetComponent<MeshRenderer>().material = woodMaterial;
                break;
            case 1:
                pickaxe.GetComponent<MeshRenderer>().material = stoneMaterial;
                break;
            case 2:
                pickaxe.GetComponent<MeshRenderer>().material = metalMaterial;
                break;
            case 3:
                pickaxe.GetComponent<MeshRenderer>().material = goldMaterial;
                break;
            case 4:
                pickaxe.GetComponent<MeshRenderer>().material = lavaMaterial;
                break;
        }
    }
}
