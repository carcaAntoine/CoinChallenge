using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemiesBehaviour : MonoBehaviour
{
    public Transform[] points; //Liste des points de patrouille de l'ennemi
    private int currentPoint; //Point de patrouille à atteindre
    public NavMeshAgent agent;
    public static Animator animator;
    public float speed;
    public int atk; //Valeur d'attaque, indique le nombre de PV de dégâts infligés 
    public GameObject player;
    public float visionDistance; //Distance de vision de l'ennemi
    private float distance;

    private Rigidbody enemyRb;

    Vector3 verticalMovement, horizontalMovement;


    private void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        currentPoint = 0;
    }
    void Update()
    {
        verticalMovement = Vector3.forward * Time.deltaTime * speed;
        horizontalMovement = Vector3.right * Time.deltaTime * speed;
        Debug.Log(verticalMovement + horizontalMovement);

        distance = Vector3.Distance(player.transform.position, transform.position);

        //Patrouille de l'ennemi
        if (transform.position.x != points[currentPoint].transform.position.x || transform.position.z != points[currentPoint].transform.position.z)
        {
            if (distance > visionDistance)
            {
                agent.SetDestination(points[currentPoint].position);
                transform.position = Vector3.MoveTowards(transform.position, points[currentPoint].position, speed * Time.deltaTime);

            }
            if (distance <= visionDistance)
            {
                Debug.Log("joueur repéré");
                agent.SetDestination(player.transform.position);
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);

            }

        }
        else
        {
            if (currentPoint < points.Length - 1)
            {
                currentPoint += 1;
            }
            else
            {
                currentPoint = 0;
            }

        }

        if (verticalMovement != Vector3.zero || horizontalMovement != Vector3.zero)
        {
            animator.SetBool("EnemyIsMoving", true);
        }
        else
        {
            animator.SetBool("EnemyIsMoving", false);
        }
    }

    public void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            Debug.Log("Joueur Touché");
            PlayerBehaviour.playerSingleton.animator.SetBool("GetHit", true);
            PlayerBehaviour.playerSingleton.playerActualPV -= atk;
            Debug.Log(PlayerBehaviour.playerSingleton.playerActualPV);
            UIManager.LosePV(atk);
            //PlayerBehaviour.playerSingleton.animator.SetBool("GetHit", false);
        }
    }
}
