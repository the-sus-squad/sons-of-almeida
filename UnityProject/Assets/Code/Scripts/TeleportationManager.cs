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


    // Start is called before the first frame update
    void Start()
    {

        var activate = actionAsset.FindActionMap("XRI LeftHand Locomotion").FindAction("Teleport Mode Cancel");
        activate.Enable();
        activate.performed += OnTeleport;

        fade.SetActive(true);

        var release = actionAsset.FindActionMap("XRI LeftHand Locomotion").FindAction("Teleport Mode Activate");
        release.Enable();
        release.performed += OnTeleportRelease;

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTeleport(InputAction.CallbackContext context) {
        if (!rayInteractor.enabled) return;
        Debug.Log("Teleportus");
        Debug.Log(context);
        // rayInteractor.enabled = false;
    }

    void OnTeleportRelease(InputAction.CallbackContext context) {
        Debug.Log("Teleport release");
    }

    public void OnSussy() {
        Debug.Log("Sus");
    }

}
