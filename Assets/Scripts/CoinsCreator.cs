using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsCreator : MonoBehaviour
{
    public List<GameObject> coinsPrefabs = new List<GameObject>();
    public List<GameObject> coinsEmplacements = new List<GameObject>();
    public Transform Parent;
    private int index;
    void Start()
    {
        System.Random rdn = new System.Random();
        foreach(GameObject coin in coinsEmplacements)
        {
            index = rdn.Next(0, coinsPrefabs.Count);
            var myCoin = Instantiate(coinsPrefabs[index], new Vector3(coin.transform.position.x,coin.transform.position.y,coin.transform.position.z), Quaternion.Euler(-90, 0, 0));
            myCoin.transform.SetParent(Parent);
            if(index == 0)
            {
                coin.transform.rotation = Quaternion.Euler(-90, 0, 0);
                
            }
        }
    }

}
