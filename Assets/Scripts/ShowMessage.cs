using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShowMessage : MonoBehaviour
{
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
