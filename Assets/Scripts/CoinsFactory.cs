using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsFactory : MonoBehaviour
{
    public List<GameObject> coinsPrefabs = new List<GameObject>();
    public List<Transform> coinsEmplacements = new List<Transform>();
    public Transform Parent;
    private int index;
    void Start()
    {
        InitCoins();
    }

    public void InitCoins()
    {
        System.Random rdn = new System.Random();
        foreach (Transform coin in coinsEmplacements)
        {
            index = rdn.Next(0, coinsPrefabs.Count);
            Instantiate(coinsPrefabs[index], new Vector3(coin.position.x, coin.position.y, coin.position.z), Quaternion.Euler(-90, 0, 0), Parent);

        }
    }

}
