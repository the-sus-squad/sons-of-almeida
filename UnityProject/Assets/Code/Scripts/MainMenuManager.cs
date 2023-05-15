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
            StartCoroutine(PlayFadeOut());
            ExitGame();
        }

        // Check for continue game stuff
        // StartCoroutine(PlayFadeOut());
        // ContinueGame();

        // Check for new game stuff
        // StartCoroutine(PlayFadeOut());
        // NewGame();
    }

    private IEnumerator PlayFadeOut()
    {
        fadeOut.SetActive(true);
        fadeOutAnimator.Play("FadeOut");

        yield return new WaitForSeconds(fadeOutAnimator.GetCurrentAnimatorStateInfo(0).length + fadeOutAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime);
    }

    private void ExitGame()
    {
        // The functions for exiting editor play mode and quitting a game build are different
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void ContinueGame()
    {
        SceneManager.LoadScene("ImportModels");
    }

    public void NewGame()
    {
        SceneManager.LoadScene("ImportModels");
    }

    public void HoverEnteredButton(HoverEnterEventArgs args)
    {
        //Debug.Log("HoverEnteredButton");
    }

    public void HoverExitedButton(HoverExitEventArgs args)
    {
        //Debug.Log("HoverExitedButton");
    }
}
