using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenPath : MonoBehaviour
{
    //Classe qui permet d'ouvrir une porte lorsque l'on récupère la clé correspondante 
    public GameObject doorToOpen;
    public GameObject key;
    public GameObject message;


    void OnTriggerEnter()
    {
        Debug.Log("Clé trouvée");

        //Ouvre la porte et désactive la clé
        doorToOpen.SetActive(false);
        key.SetActive(false);

        //S'il y a un message d'aide devant la porte, le désactive
        if(message !=null)
            message.SetActive(false);

    }
}
