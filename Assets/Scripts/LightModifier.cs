using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightModifier : MonoBehaviour
{
    public GameObject directionalLight;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (directionalLight.activeSelf)
            {
                directionalLight.SetActive(false);
            }
            else
            {
                directionalLight.SetActive(true);
            }

        }
    }
}
