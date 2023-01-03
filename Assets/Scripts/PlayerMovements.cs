using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovements : MonoBehaviour
{
    public float speed = 5.0f;
    public float jumpForce = 5;
    public float turnSpeed;
    private float horizontalInput;
    private float verticalInput;
    public float groundDistance = 0.5f;

    Vector3 verticalMovement;
    Vector3 horizontalMovement;

    private Animator animator;

    public GameObject player;

    private Rigidbody playerRb;



    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();


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
        verticalInput = Input.GetAxis("Vertical");


        MovePlayer();

        //Repop player if fall
        if (player.transform.position.y <= -5)
        {
            player.transform.position = new Vector3(0, 0, 0);
        }


        //Jump
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (IsGrounded())
            {
                playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                animator.SetBool("isJumping", true);
            }
            animator.SetBool("isJumping", false);
        }

        //Turn
        float y = Input.GetAxis("Mouse X") * turnSpeed;
        player.transform.eulerAngles = new Vector3(0, player.transform.eulerAngles.y + y, 0);
    }

    void MovePlayer()
    {
        while (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Debug.Log("gas gas gas");
            float maxSpeed = speed * 2.0f;
            verticalMovement = Vector3.forward * Time.deltaTime * maxSpeed * verticalInput;
            horizontalMovement = Vector3.right * Time.deltaTime * maxSpeed * horizontalInput;
        }
        
            verticalMovement = Vector3.forward * Time.deltaTime * speed * verticalInput;
            horizontalMovement = Vector3.right * Time.deltaTime * speed * horizontalInput;
        

        transform.Translate(verticalMovement);
        transform.Translate(horizontalMovement);

        if (verticalMovement != Vector3.zero || horizontalMovement != Vector3.zero)
        {
            animator.SetBool("isMoving", true);
            //Debug.Log("true");
        }
        else
        {
            animator.SetBool("isMoving", false);
            //Debug.Log("false");
        }

    }

}
