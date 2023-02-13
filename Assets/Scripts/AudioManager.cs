using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [SerializeField] Slider volumeSlider; //Slider de changement de volume
    void Start()
    {
        if(!PlayerPrefs.HasKey("musicVolume")) //Si c'est le premier lancement du jeu, initialise volume à 1 (100%)
        {
            PlayerPrefs.SetFloat("musicVolume", 1);
            Load();
        }
        else //Sinon récupère la valeur du volume sauvegardée dans le playerPref
        {
            Load();
        }
    }

    public void ChangeVolume()
    {
        AudioListener.volume = volumeSlider.value;
        Save();
    }

    private void Load() //Positionne le slider à la dernière valeur de volume sauvegardée
    {
        volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
    }

    private void Save() //Sauvegarde la valeur du volume dans le playerPref
    {
        PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
    }
}
