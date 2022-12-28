using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovements : MonoBehaviour
{
    public float speed = 5.0f;
    public float jumpForce = 5;
    public float turnSpeed;
    private float horizontalInput;
    private float forwardInput;
    public float groundDistance = 0.5f;

    public bool isRunning;

    public GameObject player;

    private Rigidbody playerRb;

    void Awake()
    {
        playerRb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, groundDistance);
    }

    void Update()
    {
        //Get Player Inputs
        horizontalInput = Input.GetAxis("Horizontal");
        forwardInput = Input.GetAxis("Vertical");

        MovePlayer();

        //Repop player if fall
        if(player.transform.position.y <= -5)
        {
            player.transform.position = new Vector3(0, 0, 0);
        }
        

        //Jump
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (IsGrounded())
            {
                playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }

        }

        //Turn
        float y = Input.GetAxis("Mouse X") * turnSpeed;
        player.transform.eulerAngles = new Vector3(0, player.transform.eulerAngles.y + y, 0);
    }

    void MovePlayer()
    {
        //Move Player forward
        transform.Translate(Vector3.forward * Time.deltaTime * speed * forwardInput);
        transform.Translate(Vector3.right * Time.deltaTime * speed * horizontalInput);



    }

}
