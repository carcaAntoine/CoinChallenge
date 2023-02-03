using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShowMessage : MonoBehaviour
{
    //########## Script qui permet d'afficher un message dans le jeu (Utilisé principalement devant les portes fermées) ##########
    [TextArea(1, 3)] public string textToShow;

    void OnTriggerEnter(Collider other)
    {
        UIManager.MessageCanvas.SetActive(true);
        GameObject.Find("MessageToShow").GetComponent<TMP_Text>().text = textToShow;
    }

    void OnTriggerExit()
    {
        UIManager.MessageCanvas.SetActive(false);

    }
}
