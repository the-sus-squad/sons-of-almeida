using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject fadeOut;

    [SerializeField] private HingeJoint door;
    [SerializeField] private float doorExitAngle = 10.0f;

    private Animator fadeOutAnimator;

    void Start()
    {
        fadeOutAnimator = fadeOut.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (door.angle > doorExitAngle)
        {
            StartCoroutine(ExitGameRoutine());
        }
    }

    private IEnumerator ExitGameRoutine()
    {
        fadeOut.SetActive(true);
        fadeOutAnimator.Play("FadeOut");

        yield return new WaitForSeconds(fadeOutAnimator.GetCurrentAnimatorStateInfo(0).length + fadeOutAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime);

        // The functions for exiting editor play mode and quitting a game build are different
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void ContinueGamePressed()
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

    public void NewGamePressed()
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

    public void IncreaseSFXVolume()
    {
        Debug.Log("plus");
    }

    public void DecreaseSfxVolume()
    {
        Debug.Log("minus");
    }
}
