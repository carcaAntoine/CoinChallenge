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
    public float speed;

    private Rigidbody enemyRb;


    private void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
		agent = GetComponent<NavMeshAgent>();
        currentPoint = 0;
    }
    void Update()
    {
		//Patrouille de l'ennemi
        if (transform.position != points[currentPoint].transform.position)
        {
			agent.SetDestination(points[currentPoint].position);
            //transform.position = Vector3.MoveTowards(transform.position, points[currentPoint].position, speed * Time.deltaTime);
			/*Vector3 lookAt = points[currentPoint].transform.position;
			transform.rotation = Quaternion.LookRotation(lookAt, Vector3.up);*/
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
    }
}
