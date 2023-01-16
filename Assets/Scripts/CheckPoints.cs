using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoints : MonoBehaviour
{
    [SerializeField] GameObject player; 
    [SerializeField] List<GameObject> checkPoints;
    [SerializeField] Vector3 vectorPoint;
    [SerializeField] public static float killerAltitude;

    void Update()
    {/*
        if(player.transform.position.y < -killerAltitude)
        {
            player.transform.position = vectorPoint;
        }*/
    }

    public void OnTriggerEnter(Collider other)
    {
        vectorPoint = player.transform.position;
    }
}
