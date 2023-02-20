using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsFactory : MonoBehaviour
{
    //Classe qui instantie toutes les pièces du niveau aléatoirement entre plusieurs prefabs 
    public GameObject coinPrefab;
    public GameObject diamondPrefab;
    public int diamondProbability; //probabilité (sur 100) de voir apparaître un diamant
    public List<Transform> coinsEmplacements = new List<Transform>(); //Liste des emplacements de toutes les pièces
    public Transform Parent;
    private int index;

    public static CoinsFactory coinFactorySingleton;
    void Awake()
    {
        coinFactorySingleton = this;
    }
    void Start()
    {
        InitCoins();
    }

    public void InitCoins()
    {
        System.Random rdn = new System.Random();
        foreach (Transform coin in coinsEmplacements)
        {
            index = rdn.Next(0, 100);
            if (index <= diamondProbability)
            {
                //Instantie diamant
                Instantiate(diamondPrefab, new Vector3(coin.position.x, coin.position.y, coin.position.z), Quaternion.Euler(-90, 0, 0), Parent);

            }
            else
            {
                //Instantie coin
                Instantiate(coinPrefab, new Vector3(coin.position.x, coin.position.y, coin.position.z), Quaternion.Euler(-90, 0, 0), Parent);
            }


        }
    }

}
