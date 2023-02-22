using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static GameObject gameOverCanvas; //Canvas Game Over
    public static GameObject gameOverScoreText;
    public static GameObject gameOverHighScoreText;
    public static GameObject MessageCanvas; //Canvas Message d'aide (devant les portes / plateformes bloquées)

    public GameObject player;

    [HideInInspector] public static TMP_Text coinCounterText;
    public static int coinValue;
    public static TMP_Text keyCounterText;
    public static int keyValue;

    public int pvMax;
    [HideInInspector] public int actualPV;

    public static TMP_Text pvMaxValueText;
    public static TMP_Text actualPVValueText;

    public List<Transform> keysEmplacements = new List<Transform>(); //Contient toutes les clés pour ouvrir les portes
    public List<Transform> pressurePlatesEmplacements = new List<Transform>(); //Contient toutes les plaques de pression pour ouvrir les portes
    public List<Transform> coinsEmplacements = new List<Transform>();


    void Awake()
    {
        // UI Canvas
        coinCounterText = GameObject.Find("CoinCounter").GetComponent<TMP_Text>();
        coinValue = Convert.ToInt32(coinCounterText.text);
        keyCounterText = GameObject.Find("KeysCounter").GetComponent<TMP_Text>();
        keyValue = Convert.ToInt32(keyCounterText.text);
        pvMaxValueText = GameObject.Find("PVMaxValue").GetComponent<TMP_Text>();
        actualPVValueText = GameObject.Find("PVValue").GetComponent<TMP_Text>();
        actualPV = Convert.ToInt32(actualPVValueText.text);
        pvMaxValueText.text = pvMax.ToString();

        //Game Over Canvas
        gameOverScoreText = GameObject.Find("GameOverScoreValue");
        gameOverHighScoreText = GameObject.Find("GameOverHighScoreValue");
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
        //Réinitialisation position player
        player.transform.position = new Vector3(0, 0, 0);
        player.transform.rotation = Quaternion.Euler(0, 0, 0);

        //Réinitialisation nbre keys & coins
        coinValue = 0;
        keyValue = 0;

        //Réinitialisation Timer
        Timer.singleton.currentTime = Timer.singleton.startingTime;

        coinCounterText.text = coinValue.ToString();
        keyCounterText.text = keyValue.ToString();

        // Réactive les clés
        foreach (Transform key in keysEmplacements)
        {
            key.gameObject.SetActive(true);
            key.gameObject.GetComponent<OpenPath>().doorToOpen.SetActive(true); //referme également les portes ouvertes
        }

        //Détruit les pièces encore existantes, puis en recréé de nouvelles
        for(int i = 0; i < CoinsFactory.coinFactorySingleton.Parent.childCount; i++)
        {
            if (CoinsFactory.coinFactorySingleton.Parent.GetChild(i).tag == "Coin" || CoinsFactory.coinFactorySingleton.Parent.GetChild(i).tag == "Diamond")
                Destroy(CoinsFactory.coinFactorySingleton.Parent.GetChild(i).gameObject/*.SetActive(false)*/);
        }
        CoinsFactory.coinFactorySingleton.InitCoins();

        //Réinitialise les plateformes bloquées et les plaques de pression
        foreach (Transform pressurePlate in pressurePlatesEmplacements)
        {
            pressurePlate.gameObject.transform.GetChild(0).GetComponent<Light>().color = Color.red;
            pressurePlate.gameObject.GetComponent<PressurePlateBehaviour>().elementToMove.transform.position = new Vector3(pressurePlate.gameObject.GetComponent<PressurePlateBehaviour>().actualX, pressurePlate.gameObject.GetComponent<PressurePlateBehaviour>().actualY, pressurePlate.gameObject.GetComponent<PressurePlateBehaviour>().actualZ);
            pressurePlate.gameObject.GetComponent<PressurePlateBehaviour>().message.SetActive(true);
            pressurePlate.gameObject.GetComponent<PressurePlateBehaviour>().canBeActivated = true;
        }

        //Audio
        Timer.singleton.gameOverMusic.Stop();
        Timer.singleton.bgm.Play();
    }

    public static void GameOver()
    {
        gameOverScoreText.GetComponent<TMP_Text>().text = coinValue.ToString();

        //Gestion du High Score
        if (!PlayerPrefs.HasKey("highScore")) //Création du Player Pref High Score si première partie
        {
            PlayerPrefs.SetInt("highScore", coinValue);
        }
        else //Sinon comparaison du score de la partie avec le High Score
        {
            if (coinValue > PlayerPrefs.GetInt("highScore"))
            {
                PlayerPrefs.SetInt("highScore", coinValue);
            }
        }

        gameOverHighScoreText.GetComponent<TMP_Text>().text = PlayerPrefs.GetInt("highScore").ToString();

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

    }

}