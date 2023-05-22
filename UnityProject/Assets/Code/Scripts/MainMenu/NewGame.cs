using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;

public class NewGame : MonoBehaviour
{
    [SerializeField] private GameObject fadeOut;

    private Animator fadeOutAnimator;

    void Start()
    {
        fadeOutAnimator = fadeOut.GetComponent<Animator>();

        GetComponent<XRGrabInteractable>().selectEntered.AddListener(NewGamePressed);
    }

    private void NewGamePressed(SelectEnterEventArgs args)
    {
        StartCoroutine(NewGameRoutine());
    }

    private IEnumerator NewGameRoutine()
    {
        fadeOut.SetActive(true);
        fadeOutAnimator.Play("FadeOut");

        yield return new WaitForSeconds(fadeOutAnimator.GetCurrentAnimatorStateInfo(0).length + fadeOutAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime);

        SceneManager.LoadScene("ImportModels");
    }
}
