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
    [SerializeField] private XRRayInteractor rayInteractor;
    private InputAction _thumbstick;

    // Fade Control
    [SerializeField] private Image fadeImage;
    [SerializeField] private Animator fadeAnimator;
    [SerializeField] private GameObject fade;

    // Hold to show ray variables
    [SerializeField] private float rayHoldTime = 1.0f;
    private float defaultRayTime;
    private Boolean isTimeRunning = false;


    // Start is called before the first frame update
    void Start()
    {
        fade.SetActive(true);

        var hold = actionAsset.FindActionMap("XRI LeftHand Interaction").FindAction("Activate");
        hold.Enable();
        hold.performed += OnTriggerPressed;
        hold.canceled += OnTriggerRelease;

        defaultRayTime = rayHoldTime;
        rayInteractor.enabled = false;


    }

    // Update is called once per frame
    void Update()
    {

        // Process ray hold timer
        if (isTimeRunning) {
            rayHoldTime -= Time.deltaTime;
        }
        else if (defaultRayTime != rayHoldTime) {
            rayHoldTime = defaultRayTime;
        }

        if (rayHoldTime < 0) { 
            Debug.Log("Time is 0!");
            isTimeRunning = false;
            rayInteractor.enabled = true;
        }
    }

    public void OnTeleportRelease() {
        Debug.Log("Teleport Ended!");
        rayInteractor.enabled = false;
    }

    public void OnTriggerPressed(InputAction.CallbackContext context) {
        Debug.Log("Trigger pressed!");
        isTimeRunning = true;
    }

    public void OnTriggerRelease(InputAction.CallbackContext context) {
        Debug.Log("Trigger released!");
        isTimeRunning = false;
    }

    public void OnRayEnabled() {
        // TODO: add fade effect here.
    }

}
