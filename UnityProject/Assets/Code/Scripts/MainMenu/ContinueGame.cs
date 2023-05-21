using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;

public class ContinueGame : MonoBehaviour
{
    [SerializeField] private GameObject fadeOut;

    private Animator fadeOutAnimator;

    void Start()
    {
        fadeOutAnimator = fadeOut.GetComponent<Animator>();

        GetComponent<XRGrabInteractable>().selectEntered.AddListener(ContinueGamePressed);
    }

    private void ContinueGamePressed(SelectEnterEventArgs args)
    {
        StartCoroutine(ContinueGameRoutine());
    }

    private IEnumerator ContinueGameRoutine()
    {
        fadeOut.SetActive(true);
        fadeOutAnimator.Play("FadeOut");

        yield return new WaitForSeconds(fadeOutAnimator.GetCurrentAnimatorStateInfo(0).length + fadeOutAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime);

        SceneManager.LoadScene("ImportModels");
    }
}
