using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public static Timer singleton;
    [HideInInspector]public float currentTime; //Temps restant
    public float startingTime; //Temps au début d'une partie

    [SerializeField] TMP_Text countdownText;

    //------ Audio ------
    public AudioSource bgm;
    public AudioSource gameOverMusic;

    void Awake()
    {
        singleton = this;
    }

    void Start()
    {
        currentTime = startingTime;
    }

    void Update()
    {
        if (currentTime > 0)
        {
            currentTime -= 1 * Time.deltaTime;
            countdownText.text = currentTime.ToString("0");
        }
        else
        {
            Debug.Log("fin de partie");
            UIManager.gameOverCanvas.SetActive(true);
            bgm.Stop();
            UIManager.GameOver();
        }


        /*if (currentTime <= 0)
        {
            UIManager.gameOverCanvas.SetActive(true);
            currentTime = 0;
        }*/
    }
}