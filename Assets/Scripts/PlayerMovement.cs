using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController characterController;
    public GameObject gameManager;
    public GameManagerScript gameManagerScript;
    public GameObject SpawnPoint;
    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundLayerMask;

    Vector3 velocity;
    bool isGrounded;
    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        gameManagerScript = gameManager.GetComponent<GameManagerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire3"))
        {
            Debug.Log("teleport: " + transform.position);
            transform.position = SpawnPoint.transform.position;
            Debug.Log("teleport: " + transform.position);
        }
        else
        {
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundLayerMask);

            if (isGrounded && velocity.y < 0)
            {
                velocity.y = -2f;
            }
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            Vector3 move = transform.right * x + transform.forward * z;

            characterController.Move(move * speed * Time.deltaTime);

            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }

            velocity.y += gravity * Time.deltaTime;

            characterController.Move(velocity * Time.deltaTime);
        }
    }

    public void Update_Stats()
    {
        speed = gameManagerScript.playerSpeed;
        jumpHeight = gameManagerScript.playerJump;
    }
}
