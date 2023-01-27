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
    public GameObject PauseCanvas;
    public static GameObject MessageCanvas;

    public GameObject player;

    [HideInInspector] public static TMP_Text coinCounterText;
    public static int coinValue;
    public static TMP_Text keyCounterText;
    public static int keyValue;

    void Awake()
    {
        coinCounterText = GameObject.Find("CoinCounter").GetComponent<TMP_Text>();
        coinValue = Convert.ToInt32(coinCounterText.text);
        keyCounterText = GameObject.Find("KeysCounter").GetComponent<TMP_Text>();
        keyValue = Convert.ToInt32(keyCounterText.text);

        Debug.Log("number coin : " + coinValue);
        gameOverCanvas = GameObject.Find("GameOverCanvas");
        gameOverCanvas.SetActive(false);
        
        PauseCanvas.SetActive(false);

        MessageCanvas = GameObject.Find("MessageCanvas");
        MessageCanvas.SetActive(false);


    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //PauseCanvas.SetActive(true);
            Restart();
        }
    }

    public static void AddCoinToCounter(int value)
    {
        coinValue += value;
        Debug.Log("Add Coin");
        coinCounterText.text = coinValue.ToString();
    }

    public static void AddKeyToCounter()
    {
        keyValue += 1;
        keyCounterText.text = keyValue.ToString();
    }

    public void Restart()
    {
        player.transform.position = new Vector3(0, 0, 0);
        coinValue = 0;
        keyValue = 0;
        coinCounterText.text = coinValue.ToString();
        keyCounterText.text = keyValue.ToString();
    }

}