using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{   
    public static GameObject gameOverCanvas;
    public static GameObject gameOverScoreText;
    public static GameObject MessageCanvas;

    public GameObject player;

    [HideInInspector] public static TMP_Text coinCounterText;
    public static int coinValue;
    public static TMP_Text keyCounterText;
    public static int keyValue;

    public List<Transform> keysEmplacements = new List<Transform>(); //Contient toutes les clés pour ouvrir les portes
    public List<Transform> pressurePlatesEmplacements = new List<Transform>(); //Contient toutes les plaques de pression pour ouvrir les portes

    void Awake()
    {
        // UI Canvas
        coinCounterText = GameObject.Find("CoinCounter").GetComponent<TMP_Text>();
        coinValue = Convert.ToInt32(coinCounterText.text);
        keyCounterText = GameObject.Find("KeysCounter").GetComponent<TMP_Text>();
        keyValue = Convert.ToInt32(keyCounterText.text);

        //Game Over Canvas
        gameOverScoreText = GameObject.Find("GameOverScoreValue");
        gameOverCanvas = GameObject.Find("GameOverCanvas");
        gameOverCanvas.SetActive(false);

        //Message Canvas
        MessageCanvas = GameObject.Find("MessageCanvas");
        MessageCanvas.SetActive(false);

    }

    //Augmente la valeur du score (nbre de pièces)
    public static void AddCoinToCounter(int value)
    {
        coinValue += value;
        Debug.Log("Add Coin");
        coinCounterText.text = coinValue.ToString();
    }

    //Augmente la valeur du compteur de clés
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
        Timer.singleton.currentTime = Timer.singleton.startingTime;
        coinCounterText.text = coinValue.ToString();
        keyCounterText.text = keyValue.ToString();

        // Réactive les clés
        foreach (Transform key in keysEmplacements)
        {
            key.gameObject.SetActive(true);
            key.gameObject.GetComponent<OpenPath>().doorToOpen.SetActive(true); //referme également les portes ouvertes
        }

        //Réinitialise les plateformes bloquées et les plaques de pression
        foreach(Transform pressurePlate in pressurePlatesEmplacements)
        {
            pressurePlate.gameObject.transform.GetChild(0).GetComponent<Light>().color = Color.red;
            pressurePlate.gameObject.GetComponent<PressurePlateBehaviour>().elementToMove.transform.position = new Vector3(PressurePlateBehaviour.PressurePlateSingleton.actualX, PressurePlateBehaviour.PressurePlateSingleton.actualY, PressurePlateBehaviour.PressurePlateSingleton.actualZ);
            pressurePlate.gameObject.GetComponent<PressurePlateBehaviour>().message.SetActive(true);
            pressurePlate.gameObject.GetComponent<PressurePlateBehaviour>().canBeActivated = true;
        }
    }

    public static void GameOver()
    {
        gameOverScoreText.GetComponent<TMP_Text>().text = coinValue.ToString();
    }

}