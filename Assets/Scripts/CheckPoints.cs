using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoints : MonoBehaviour
{
    [SerializeField] GameObject player; 
    [SerializeField] List<GameObject> checkPoints;
    [SerializeField] Vector3 vectorPoint;
    [SerializeField] float killerAltitude;

    void Update()
    {
        //Fait réapparaître le joueur s'il chute
        if(player.transform.position.y < -killerAltitude)
        {
            Debug.Log(player.transform.position.y);
            player.transform.position = vectorPoint;
        }
    }

    //Actualise le dernier checkpoint franchi
    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "CheckPoint")
        {
            vectorPoint = player.transform.position;
        }
        
    }
}
