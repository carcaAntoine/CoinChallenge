using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpCoin : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.tag == "Coin")
        {
            Debug.Log("Pièce récupérée");
            collider.gameObject.SetActive(false);
            UIManager.AddCoinToCounter();
        }

        if(collider.gameObject.tag == "PrimalKey")
        {
            Debug.Log("Clé récupérée");
            collider.gameObject.SetActive(false);
            UIManager.AddKeyToCounter();
        }
    }
}
