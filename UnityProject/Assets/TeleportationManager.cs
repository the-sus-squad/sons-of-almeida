using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.CoreModule;

public class TeleportationManager : MonoBehaviour
{
    [SerializeField] private InputActionAsset actionAsset;
    [SerializeField] private XRRayInteractor rayInteractor;
    private InputAction _thumbstick;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Custom start");
        // rayInteractor.enabled = false;
        actionAsset = ScriptableOject.CreateInstance<InputActionAsset>();

        var activate = actionAsset.FindActionMap("XRI LeftHand").FindAction("Teleport Mode Activate");
        activate.Enable();
        // .performed = list of methods when the told action is made.
        activate.performed += OnTeleportActivate;

        var cancel = actionAsset.FindActionMap("XRI LeftHand").FindAction("Teleport Mode Activate");
        cancel.Enable();
        activate.performed += OnTeleportCancel;


        _thumbstick = actionAsset.FindActionMap("XRI LeftHand").FindAction("Teleport Mode Activate");
        _thumbstick.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTeleportActivate(InputAction.CallbackContext context) {
        Debug.Log("Sussy Teleport");
        // rayInteractor.enabled = true;
    }

    void OnTeleportCancel(InputAction.CallbackContext context) {
        // rayInteractor.enabled = false;

    }
}
