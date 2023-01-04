using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenPath : MonoBehaviour
{

    public GameObject elementToMove;
    public GameObject key;
    public float xValue;
    public float yValue;
    public float zValue;
    public bool openDoor;

    void OnTriggerEnter()
    {
        Debug.Log("Clé trouvée");
        if (openDoor)
        {
            elementToMove.SetActive(false);
        }
        else
        {
            elementToMove.transform.position = new Vector3(xValue, yValue, zValue);
        }

        key.SetActive(false);

    }
}
