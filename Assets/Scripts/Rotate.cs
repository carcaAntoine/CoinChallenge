using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    //Classe qui permet la rotation d'un objet (pièce, clé, ...) 
    public int rotateSpeed;

    void Update()
    {
       transform.Rotate(0, rotateSpeed, 0, Space.World);
    }
    
}