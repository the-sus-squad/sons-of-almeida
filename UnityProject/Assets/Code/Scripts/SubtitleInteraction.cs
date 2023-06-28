using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.UI;

public class SubtitleInteraction : MonoBehaviour
{

    CanvasGroup canvas_group;
    void Start() {
        canvas_group = GetComponent<CanvasGroup>();
    }

    // void Hide()
    // {
    //     float fadeTime = 1f;
    //     // gameObject.transform.Find("Subtitle").gameObject.SetActive(false);
    //     StartCoroutine(FadeOut(fadeTime));
    // } 

    IEnumerator ProcessMessage(string text, float waitTime=5f, float fadeTime = 1f) {

        transform.Find("Subtitle").gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = text;
        yield return StartCoroutine(Fade(fadeTime));        
        
        yield return new WaitForSeconds(waitTime);
        
        yield return StartCoroutine(Fade(fadeTime, false));
    }

    IEnumerator Fade(float fadeTime=1f, bool fadeIn=true) {
        float fade = 0;

        while (fade < fadeTime) {
            float newAlpha = (fade/fadeTime);
            if (!fadeIn) newAlpha = 1 - newAlpha;
            canvas_group.alpha = newAlpha;
            fade += Time.deltaTime;
            yield return null;
        }
        canvas_group.alpha = fadeIn ? 1 : 0;
        yield return null;
    }


    public void ShowMessage(string text, float time = 5f) {
        StartCoroutine(ProcessMessage(text, time-2f));
    }
}
