using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovements : MonoBehaviour
{
    public float speed = 5.0f;
    public float jumpSpeed = 5;
    public float rotationSpeed;
    public float jumpButtonGracePeriod;
    private float horizontalInput;
    private float verticalInput;
    public float groundDistance = 0.5f;
    private float ySpeed;
    private float? lastGroundedTime;
    private float? jumpButtonPressedTime;
    private float gravity = -20f;

    Vector3 verticalMovement;
    Vector3 horizontalMovement;
    private Animator animator;
    public GameObject player;
    private Rigidbody playerRb;

    [SerializeField]
    private Transform cameraTransform;
    public CharacterController characterController;



    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        //characterController = GetComponent<CharacterController>();

        //Désactivation visuelle du curseur
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    //Vérifie que le Player est au sol
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
/*
        if (player.transform.position.y < -5)
        {
            player.transform.position = new Vector3(0, 0, 0);
        }*/
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
        velocity.y = ySpeed + gravity * Time.deltaTime;

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
