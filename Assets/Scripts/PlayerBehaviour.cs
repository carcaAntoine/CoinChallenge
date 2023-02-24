using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public static PlayerBehaviour playerSingleton;
    private float horizontalInput;
    private float verticalInput;

    public float speed = 5.0f; //Vitesse
    public float rotationSpeed;
    public float groundDistance = 0.5f;
    public float jumpForce = 300; //Force de saut
    public int playerPV; //Nombre de PV au démarrage (PV Max)
    [HideInInspector] public int playerActualPV; //Nombre de PV restants au joueur

    [HideInInspector] public static bool movementEnabled = true; //Active/Désactive les mouvements du joueur

    //------------------------------------

    Vector3 verticalMovement;
    Vector3 horizontalMovement;
    private Animator animator;
    public GameObject player;
    private Rigidbody playerRb;

    [SerializeField] private Transform cameraTransform;

    private bool isMoving;
    

    //------ Audio ------
    public AudioSource walkSound;
    public AudioSource jumpSound;
    
    //-------------------

    void Awake()
    {
        playerSingleton = this;
    }

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        playerActualPV = playerPV;

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
        
    }

    void MovePlayer()
    {
        verticalMovement = Vector3.forward * Time.deltaTime * speed * verticalInput;
        horizontalMovement = Vector3.right * Time.deltaTime * speed * horizontalInput;


        Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput);
        float magnitude = Mathf.Clamp01(movementDirection.magnitude) * speed;
        movementDirection = Quaternion.AngleAxis(cameraTransform.rotation.eulerAngles.y, Vector3.up) * movementDirection;
        movementDirection.Normalize();

        if (movementEnabled)
            transform.Translate(movementDirection * speed * Time.deltaTime, Space.World);

        if (movementDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);

        }

        //----- Jump -----

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            walkSound.Stop();
            jumpSound.Play();
            playerRb.AddForce(new Vector3(0, jumpForce, 0));

        }


        //Animations & Audio
        if (verticalMovement != Vector3.zero || horizontalMovement != Vector3.zero)
        {
            animator.SetBool("isMoving", true);
            if(!isMoving)
            {
                walkSound.Play();
                isMoving = true;
            }
            
                
        }
        else
        {
            animator.SetBool("isMoving", false);
            walkSound.Stop();
            isMoving = false;
        }

    }

}
