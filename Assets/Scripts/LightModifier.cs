using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightModifier : MonoBehaviour
{
    public GameObject directionalLight;
    public GameObject[] fireColumns;
    private bool localLights;

    void Start()
    {
        //Désactive les Locals Lights
        foreach (GameObject column in fireColumns)
        {
            column.transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
            localLights = false;
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //Active/Désactive la directional Light
            if (directionalLight.activeSelf)
            {
                directionalLight.SetActive(false);
            }
            else
            {
                directionalLight.SetActive(true);
            }

            //Active les Locals Lights
            if (localLights)
            {
                foreach (GameObject column in fireColumns)
                {
                    column.transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
                    localLights = false;
                }

            }
            else
            {
                foreach (GameObject column in fireColumns)
                {
                    column.transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
                    localLights = true;
                }
            }


        }
    }
}
