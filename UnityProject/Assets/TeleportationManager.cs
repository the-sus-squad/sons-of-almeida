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


    // Start is called before the first frame update
    void Start()
    {

        var activate = actionAsset.FindActionMap("XRI LeftHand Locomotion").FindAction("Teleport Mode Cancel");
        activate.Enable();
        activate.performed += OnTeleport;

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTeleport(InputAction.CallbackContext context) {
        Debug.Log("Teleportus");
        fadeAnimator.Play("FadeIn");
    }

}
