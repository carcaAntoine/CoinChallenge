using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //public static GameObject coinCounter;
    public static GameObject gameOverCanvas;
    [HideInInspector]
    public static TMP_Text coinCounterText;
    public static int coinValue;

    void Awake()
    {
        coinCounterText = GameObject.Find("CoinCounter").GetComponent<TMP_Text>();
        coinValue = Convert.ToInt32(coinCounterText.text);
        Debug.Log("number coin : " + coinValue);
        gameOverCanvas = GameObject.Find("GameOverCanvas");
        gameOverCanvas.SetActive(false);


    }

    public static void AddCoinToCounter()
    {
        coinValue += 1;
        Debug.Log("Add Coin");
        coinCounterText.text = coinValue.ToString();
    }


}