using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SubtitleInteraction : MonoBehaviour
{

    void Hide()
    {
        gameObject.transform.Find("Subtitle").gameObject.SetActive(false);
    } 

    public void ShowMessage(string text, float time = 5f) {
        transform.Find("Subtitle").gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = text;
        gameObject.transform.Find("Subtitle").gameObject.SetActive(true);
        Invoke("Hide", time);
    }
}
