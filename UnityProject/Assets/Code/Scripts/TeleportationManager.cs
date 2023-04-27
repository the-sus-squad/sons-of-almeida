using System.Numerics;
using System;
using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.UI;
using UnityEngine.Events;

using Vector3 = UnityEngine.Vector3;


public class TeleportationManager : MonoBehaviour
{
    // VR Teleport
    [SerializeField] private InputActionAsset actionAsset;
    [SerializeField] private XRRayInteractor leftRayInteractor;
    [SerializeField] private XRRayInteractor rightRayInteractor;
    private LineRenderer leftLineRenderer;
    private LineRenderer rightLineRenderer;

    private XRInteractorLineVisual leftLineVisual;
    private XRInteractorLineVisual rightLineVisual;

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

    // Teleportation state
    private bool teleportEnable = false;
    public float teleportCooldownWeight = 5.0f;
    private float teleportCooldown = 5.0f; // in seconds
    private float teleportCooldownTime = 0.0f;
    private float teleportDistance;
    public UnityEvent<bool> OnTeleportState;


    // Start is called before the first frame update
    void Start()
    {
        fade.SetActive(true);
        OnTeleportState.Invoke(teleportEnable);

        leftLineRenderer = leftRayInteractor.GetComponent<LineRenderer>();
        rightLineRenderer = rightRayInteractor.GetComponent<LineRenderer>();

        leftLineVisual = leftRayInteractor.GetComponent<XRInteractorLineVisual>();
        rightLineVisual = rightRayInteractor.GetComponent<XRInteractorLineVisual>();


        leftHandTriggerAction = actionAsset.FindActionMap("XRI LeftHand Interaction").FindAction("Activate");
        rightHandTriggerAction = actionAsset.FindActionMap("XRI RightHand Interaction").FindAction("Activate");

        leftRayInteractor.selectExited.AddListener(OnTeleport);
        rightRayInteractor.selectExited.AddListener(OnTeleport);

        // TODO maybe try to add a teleport.started function to create fade effect before teleportation had occured?

        defaultRayTime = leftRayHoldTime;
        leftRayInteractor.enabled = false;
        teleportCooldownTime = teleportCooldown;

    }

    // Update is called once per frame
    void Update()
    {

        // Filter cooldowns
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

        if (teleportCooldownTime < teleportCooldown) {
            teleportCooldownTime += Time.deltaTime;
        }
        else if (teleportCooldownTime > teleportCooldown) {
            teleportCooldownTime = teleportCooldown;
        }


        if (leftRayHoldTime < 0)
        {
            ValidateTeleportCooldown(leftRayInteractor, leftLineVisual, leftLineRenderer);
        }
        
        if (rightRayHoldTime < 0)
        {
            ValidateTeleportCooldown(rightRayInteractor, rightLineVisual, rightLineRenderer);
        }
    }

    private void ValidateTeleportCooldown(XRRayInteractor rayInteractor, XRInteractorLineVisual lineVisual, LineRenderer lineRenderer) {

        var cooldownCalc = teleportCooldownTime / teleportCooldown;

        // Based on teleportCooldownTime, change the transparency color of the lineVisual
        Color lineColor = Color.Lerp(Color.gray, Color.green, cooldownCalc);

        lineVisual.validColorGradient.SetKeys(
            new GradientColorKey[] { new GradientColorKey(lineColor, 0.0f)},
            new GradientAlphaKey[] { new GradientAlphaKey(cooldownCalc, 0.0f)}
        );

        rayInteractor.enabled = true;

        if (cooldownCalc >= 1.0f) {
            EnableTeleport(true);
            // Calculate teleport distance based on linerenderer point
            teleportDistance = Vector3.Distance(lineRenderer.GetPosition(0), lineRenderer.GetPosition(1));
        }
        else {
            EnableTeleport(false);
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
        if (teleportCooldownTime < teleportCooldown) return;
        fadeAnimator.Play("FadeIn");
        teleportCooldownTime = 0.0f;
        teleportCooldown = teleportDistance * teleportCooldownWeight;
    }

    private void EnableTeleport(bool val) {
        if (val != teleportEnable) {
            teleportEnable = val;
            OnTeleportState.Invoke(teleportEnable);
        }
    }

}
