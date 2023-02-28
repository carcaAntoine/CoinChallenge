using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpObjects : MonoBehaviour
{
    //########## Classe qui gère les évènements selon ce que le joueur récupère ##########

    //----------Audio----------
    public AudioSource getCoin;
    public AudioSource getDiamond;
    public AudioSource getKey;
    //--------------------------    
    
    private void OnTriggerEnter(Collider collider)
    {
        //Si le joueur récupère une pièce
        if (collider.gameObject.tag == "Coin")
        {
            Debug.Log("Pièce récupérée");
            collider.gameObject.SetActive(false);
            UIManager.AddCoinToCounter(1);
            getCoin.Play();
        }

        //Si le joueur récupère un diamant
        if (collider.gameObject.tag == "Diamond")
        {
            collider.gameObject.SetActive(false);
            UIManager.AddCoinToCounter(5);
            getDiamond.Play();
        }

        //Si le joueur récupère une clé
        if (collider.gameObject.tag == "PrimalKey")
        {
            Debug.Log("Clé récupérée");
            collider.gameObject.SetActive(false);
            UIManager.AddKeyToCounter();
            getKey.Play();
        }

        if (collider.gameObject.tag == "Enemy'sHead")
        {
            Debug.Log("Ennemi touché");
            EnemiesBehaviour.animator.SetBool("isDead", true);
            collider.gameObject.SetActive(false);
        }
    }
}
