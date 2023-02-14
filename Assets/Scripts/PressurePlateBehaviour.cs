using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PressurePlateBehaviour : MonoBehaviour
{

    //Classe qui permet d'abaisser une plateforme lorsque l'on marche sur la plaque de pression 
    public static PressurePlateBehaviour PressurePlateSingleton;
    public GameObject elementToMove; //Element à déplacer pour ouvrir le chemin
    public GameObject message; //Message d'aide à désactiver une fois le chemin ouvert

    public float yValue; //Valeur Y d'arrivée d'elementToMove

    // ----------- Light ------------
    public Light lt;
    Color redColor = Color.red;
    Color greenColor = Color.green;
    // ------------------------------

    // ----------- Déplacement Camera ------------
    public bool moveCamera; //Est-ce qu'on bouge la camera pour montrer la plateforme ?
    public GameObject cinemachine;
    public Transform playerTransform;

    // --------------------------------
    [HideInInspector] public bool canBeActivated = true; //pour s'assurer qu'on ne peut appuyer sur la plaque de pression qu'une fois
    [HideInInspector] public float actualX, actualY, actualZ; //Coordonnées d'origine de la plateforme 

    // ----------- Audio ------------
    public AudioSource pressurePlateSound;

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
            if (moveCamera)
            {
                //Désactive les mouvements du joueur
                PlayerMovements.movementEnabled = false;

                //Déplace la caméra jusqu'à la plateforme qui se déplace
                cinemachine.GetComponent<CinemachineFreeLook>().LookAt = elementToMove.transform;
            }

            pressurePlateSound.Play();

            //Déplacement de la plateforme
            StartCoroutine(OpenPath());

            //Changement de couleur de la plaque de pression
            lt.color = greenColor;

            canBeActivated = false;
            message.SetActive(false);

        }

    }

    //Coroutine de déplacement de la plateforme
    IEnumerator OpenPath()
    {
        for (float y = actualY; y > yValue; y -= 0.01f)
        {
            elementToMove.transform.position = new Vector3(actualX, y, actualZ);
            yield return null;
        }

        if (moveCamera)
        {
            //Replace la caméra sur le player et réactive les déplacements
            cinemachine.GetComponent<CinemachineFreeLook>().LookAt = playerTransform;
            PlayerMovements.movementEnabled = true;
        }
    }
}
