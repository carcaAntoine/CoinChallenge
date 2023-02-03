using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenPath : MonoBehaviour
{
    //########## Script qui permet d'ouvrir une porte lorsque l'on récupère la clé correspondante ##########
    public GameObject doorToOpen;
    public GameObject key;
    public GameObject message;


    void OnTriggerEnter()
    {
        Debug.Log("Clé trouvée");
        doorToOpen.SetActive(false);
        key.SetActive(false);
        message.SetActive(false);

    }
}
