using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovements : MonoBehaviour
{
    public float speed = 5.0f;
    //public float jumpSpeed = 5;
    public float rotationSpeed;
    public float jumpButtonGracePeriod;
    private float horizontalInput;
    private float verticalInput;
    public float groundDistance = 0.5f;
    //private float ySpeed;
    //private float? lastGroundedTime;
    //private float? jumpButtonPressedTime;
    //private float gravity = -20f;

    //----------Jump variables-------------
    public float jumpAmount = 7f;
    public float gravityScale = 20f;
    public float maxDuration = 0.3f;

    private float jumpTime;
    private bool jumping;

    //------------------------------------

    Vector3 verticalMovement;
    Vector3 horizontalMovement;
    private Animator animator;
    public GameObject player;
    private Rigidbody playerRb;

    [SerializeField]
    private Transform cameraTransform;




    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        //Désactivation visuelle du curseur
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    //Vérifie que le Player est au sol
    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, groundDistance);

    }

    void FixedUpdtae()
    {
        playerRb.AddForce(Physics.gravity * (gravityScale - 1) * playerRb.mass);
    }

    void Update()
    {
        //Get Player Inputs
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        MovePlayer();
    }

    void MovePlayer()
    {

        verticalMovement = Vector3.forward * Time.deltaTime * speed * verticalInput;
        horizontalMovement = Vector3.right * Time.deltaTime * speed * horizontalInput;

        Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput);
        float magnitude = Mathf.Clamp01(movementDirection.magnitude) * speed;
        movementDirection = Quaternion.AngleAxis(cameraTransform.rotation.eulerAngles.y, Vector3.up) * movementDirection;
        movementDirection.Normalize();

        transform.Translate(movementDirection * speed * Time.deltaTime, Space.World);

        if (movementDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);

        }

        //----- Jump -----

        /*if (Input.GetKeyDown(KeyCode.Space))
        {
            jumping = true;
            jumpTime = 0;
        }
        if (jumping)
        {
            playerRb.velocity = new Vector2(playerRb.velocity.x, jumpAmount);
            jumpTime += Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.Space) | jumpTime > maxDuration)
        {
            jumping = false;
        }*/

        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerRb.velocity = new Vector2(playerRb.velocity.x, jumpAmount);
           
        }





        /*
                ySpeed += Physics.gravity.y * Time.deltaTime;

                //Jump
                if (IsGrounded())
                {
                    lastGroundedTime = Time.time;
                }

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    jumpButtonPressedTime = Time.time;
                }

                if (Time.time - lastGroundedTime <= jumpButtonGracePeriod)
                {
                    if (Time.time - jumpButtonPressedTime <= jumpButtonGracePeriod)
                    {
                        ySpeed = jumpSpeed;
                        jumpButtonPressedTime = null;
                        lastGroundedTime = null;
                    }
                }

                Vector3 velocity = movementDirection * magnitude;
                velocity.y = ySpeed + gravity * Time.deltaTime;*/

        //characterController.Move(velocity * Time.deltaTime);
        //transform.Translate(velocity * Time.deltaTime);



        //Animations
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
