using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            StartCoroutine(ExitGame());
        }
    }

    private IEnumerator ExitGame()
    {
        fadeOut.SetActive(true);
        fadeOutAnimator.Play("FadeOut");

        yield return new WaitForSeconds(fadeOutAnimator.GetCurrentAnimatorStateInfo(0).length + fadeOutAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime);

        // the functions for exiting editor play mode and quitting a game build are different
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
