using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PickUpObjects : MonoBehaviour
{
    //########## Classe qui gère les évènements selon ce que le joueur récupère ##########

    //----------Audio----------
    public AudioSource getCoin;
    public AudioSource getDiamond;
    public AudioSource getKey;
    public GameObject playerPrefab;
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

        if (collider.gameObject.tag == "Portal")
        {
            LoadLevel();
        }


    }

    void LoadLevel()
    {
        SceneManager.LoadScene("FinalScene");
        Instantiate(playerPrefab, new Vector3(8, 0, 8), Quaternion.identity);
    }

}

