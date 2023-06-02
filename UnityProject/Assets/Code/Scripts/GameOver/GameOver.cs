using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public class GameOver : MonoBehaviour
{

    [SerializeField] private InputActionAsset actionAsset;
    private InputAction leftHandTriggerAction;
    private InputAction rightHandTriggerAction;

    [SerializeField] private GameObject fadeOut;

    private Animator fadeOutAnimator;

   
    // Start is called before the first frame update
    void Start()
    {
        leftHandTriggerAction = actionAsset.FindActionMap("XRI LeftHand Interaction").FindAction("Activate");
        rightHandTriggerAction = actionAsset.FindActionMap("XRI RightHand Interaction").FindAction("Activate");

        fadeOutAnimator = fadeOut.GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if (IsTriggerPressed(leftHandTriggerAction) || IsTriggerPressed(rightHandTriggerAction))
        {
            ResetGame();
        }
    }

    void ResetGame() {
        StartCoroutine(ResetGameRoutine());
    }

    private bool IsTriggerPressed(InputAction triggerAction)
    {
        if (triggerAction.ReadValue<float>() > 0.1f)
        {
            return true;
        }
        return false;
    }


    private IEnumerator ResetGameRoutine()
    {
        fadeOut.SetActive(true);
        fadeOutAnimator.Play("FadeOut");

        yield return new WaitForSeconds(fadeOutAnimator.GetCurrentAnimatorStateInfo(0).length + fadeOutAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime);

        SceneManager.LoadScene("MainMenu");
    }

}
