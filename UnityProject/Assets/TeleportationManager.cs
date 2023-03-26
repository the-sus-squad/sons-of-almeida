using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
// using UnityEngine.CoreModule;

public class TeleportationManager : MonoBehaviour
{
    [SerializeField] private InputActionAsset actionAsset;
    [SerializeField] private XRRayInteractor rayInteractor;
    private InputAction _thumbstick;


    // Start is called before the first frame update
    void Start()
    {
        // Used to movement instead of teleport...?
        // var activate = actionAsset.FindActionMap("XRI LeftHand Locomotion").FindAction("Teleport Mode Activate");
        // activate.Enable();

        // // .performed = list of methods when the told action is made.
        // activate.performed += OnTeleportActivate;

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
    }

}
