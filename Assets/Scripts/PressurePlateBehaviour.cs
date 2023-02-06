using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlateBehaviour : MonoBehaviour
{

    //########## Script qui permet d'abaisser une plateforme lorsque l'on marche sur la plaque de pression ##########
    public static PressurePlateBehaviour PressurePlateSingleton;
    public GameObject elementToMove; //Element à déplacer pour ouvrir le chemin
    public GameObject message; //Message d'aide à désactiver une fois le chemin ouvert
    
    public float yValue; //Valeur Y d'arrivée d'elementToMove
    
    // ----------- Light ------------
    public Light lt;
    Color redColor = Color.red;
    Color greenColor = Color.green;
    // ------------------------------

    bool canBeActivated = true; //pour s'assurer qu'on ne peut appuyer sur la plaque de pression qu'une fois
    [HideInInspector] public float actualX, actualY, actualZ; //Coordonnées d'origine de la plateforme 

    void Awake()
    {
        PressurePlateSingleton = this;
    }

    void Start()
    {
        lt.color = redColor;
        actualX = elementToMove.transform.position.x;
        actualY = elementToMove.transform.position.y;
        actualZ = elementToMove.transform.position.z;
    }

    void OnTriggerEnter()
    {
        if (canBeActivated)
        {
            StartCoroutine(OpenPath());
            lt.color = greenColor;
            canBeActivated = false;
            message.SetActive(false);
        }

    }

    IEnumerator OpenPath()
    {
        for(float y = actualY; y > yValue; y -= 0.1f)
        {
            elementToMove.transform.position = new Vector3(actualX, y, actualZ);
            yield return null;
        }
    }
}
