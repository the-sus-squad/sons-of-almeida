using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SubtitleInteraction : MonoBehaviour
{

    private string[] exampleTexts = { 
    "This is a legend",
    "This is also a legend",
    "This is a reeaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaally long legend",
    };

    private int current = 0;
    private int onTime = 1;


    // Start is called before the first frame update
    void Start()
    {
        Invoke("Show", 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Show()
    {
        transform.Find("Subtitle").gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = exampleTexts[current];
        current++;
        gameObject.transform.Find("Subtitle").gameObject.SetActive(true);
        onTime *= 2;
        Invoke("Hide", onTime);
    }

    void Hide()
    {
        gameObject.transform.Find("Subtitle").gameObject.SetActive(false);
        if (current >= exampleTexts.Length)
        {
            current = 0;
            onTime = 1;
        }
        Invoke("Show", 1);
    } 
}
