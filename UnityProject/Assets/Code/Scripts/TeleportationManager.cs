using System;
using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.UI;

public class TeleportationManager : MonoBehaviour
{
    // VR Teleport
    [SerializeField] private InputActionAsset actionAsset;
    [SerializeField] private XRRayInteractor leftRayInteractor;
    [SerializeField] private XRRayInteractor rightRayInteractor;
    private InputAction leftHandTriggerAction;
    private InputAction rightHandTriggerAction;

    // Fade Control
    [SerializeField] private Image fadeImage;
    [SerializeField] private Animator fadeAnimator;
    [SerializeField] private GameObject fade;

    // Hold to show ray variables
    [SerializeField] private float leftRayHoldTime = 1.0f;
    [SerializeField] private float rightRayHoldTime = 1.0f;
    private float defaultRayTime;

    // Start is called before the first frame update
    void Start()
    {
        fade.SetActive(true);

        leftHandTriggerAction = actionAsset.FindActionMap("XRI LeftHand Interaction").FindAction("Activate");
        rightHandTriggerAction = actionAsset.FindActionMap("XRI RightHand Interaction").FindAction("Activate");

        leftRayInteractor.selectExited.AddListener(OnTeleport);
        rightRayInteractor.selectExited.AddListener(OnTeleport);

        // TODO maybe try to add a teleport.started function to create fade effect before teleportation had occured?

        defaultRayTime = leftRayHoldTime;
        leftRayInteractor.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsTriggerPressed(leftHandTriggerAction))
        {
            leftRayHoldTime -= Time.deltaTime;
        }
        else
        {
            leftRayHoldTime = defaultRayTime;
            leftRayInteractor.enabled = false;
        }

        if (IsTriggerPressed(rightHandTriggerAction))
        {
            rightRayHoldTime -= Time.deltaTime;
        }
        else
        {
            rightRayHoldTime = defaultRayTime;
            rightRayInteractor.enabled = false;
        }

        if (leftRayHoldTime < 0)
        {
            leftRayInteractor.enabled = true;
        }
        
        if (rightRayHoldTime < 0)
        {
            rightRayInteractor.enabled = true;
        }
    }

    private bool IsTriggerPressed(InputAction triggerAction)
    {
        if (triggerAction.ReadValue<float>() > 0.1f)
        {
            return true;
        }
        return false;
    }

    private void OnTeleport(BaseInteractionEventArgs arg)
    {
        Debug.Log(arg.interactableObject.transform.name);
        fadeAnimator.Play("FadeIn");
        GetComponent<FootstepsSoundManager>().PlayFootsteps(arg.interactableObject.transform);
    }

    public void OnRayEnabled() {
        // TODO: add color fade effect here.
    }
}
