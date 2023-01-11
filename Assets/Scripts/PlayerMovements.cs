using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovements : MonoBehaviour
{
    public float speed = 5.0f;
    public float jumpSpeed = 5;
    public float turnSpeed;
    private float horizontalInput;
    private float verticalInput;
    public float groundDistance = 0.5f;
    private float ySpeed;

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
        characterController = GetComponent<CharacterController>();

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

        //Turn
        /*float y = Input.GetAxis("Mouse X") * turnSpeed;
        player.transform.eulerAngles = new Vector3(0, player.transform.eulerAngles.y + y, 0);*/
    }

    void MovePlayer()
    {
        
        verticalMovement = Vector3.forward * Time.deltaTime * speed * verticalInput;
        horizontalMovement = Vector3.right * Time.deltaTime * speed * horizontalInput;
        /*

        transform.Translate(verticalMovement);
        transform.Translate(horizontalMovement);*/

        Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput);
        float magnitude = Mathf.Clamp01(movementDirection.magnitude) * speed;
        //movementDirection = Quaternion.AngleAxis(cameraTransform.rotation.eulerAngles.y, Vector3.up) * movementDirection;
        movementDirection.Normalize();

        ySpeed += Physics.gravity.y * Time.deltaTime;

        //Jump
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ySpeed = jumpSpeed;
        }

        Vector3 velocity = movementDirection * magnitude;
        velocity.y = ySpeed;

        characterController.Move(velocity * Time.deltaTime);

        
       

        if(movementDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, turnSpeed * Time.deltaTime);
        }

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
