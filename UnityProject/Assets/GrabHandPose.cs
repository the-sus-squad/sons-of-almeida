using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System;
using System.Drawing;

#if UNITY_EDITOR
using UnityEditor;
#endif

/*
 * Component attached to grabable objects to define what hand pose
 * should be used when grabing the object.
 * 
 * The hand poses for the right and left hands should be defined by
 * adding the right and left hand models as children of the grabable
 * object and then assigning these poses to the corresponding variables
 * in the inspector.
 * 
 * TODO: Maybe instead of moving and rotating hand I should move and rotate the object
 */
public class GrabHandPose : MonoBehaviour
{
    [SerializeField] private float poseTransitionDuration = 0.2f;
    [SerializeField] private GrabPoint[] grabPoints;

    private Vector3 startingHandPosition;
    private Vector3 finalHandPosition;
    private Quaternion startingHandRotation;
    private Quaternion finalHandRotation;

    private Quaternion[] startingFingerRotations;
    private Quaternion[] finalFingerRotations;

    // Start is called before the first frame update
    void Start()
    {
        XRGrabInteractable grabInteractable = GetComponent<XRGrabInteractable>();

        // Listen to the grab and stop grabbing events
        grabInteractable.selectEntered.AddListener(SetupPose);
        grabInteractable.selectExited.AddListener(UnsetPose);
    }

    /*
     * Called when an interactor starts grabbing the object
     * 
     * 1) Stores the hand data from the interacting hand in a local variable
     * 2) Disables the animator of the hand
     * 3) Sets this component variables to the appropriate hand data values
     * 4) Starts a coroutine to change the values of the interactor hand over time
     */
    private void SetupPose(BaseInteractionEventArgs arg) 
    {
        if (arg.interactorObject is XRDirectInteractor) 
        {
            XRDirectInteractor interactor = arg.interactorObject.transform.GetComponent<XRDirectInteractor>();
            HandData handData = arg.interactorObject.transform.GetComponentInChildren<HandData>();
            handData.animator.enabled = false;

            GrabPoint grabPoint = GetClosestGrabPoint(handData.root.position);

            if (handData.handType == HandData.HandModelType.Right) 
            {
                //SetHandDataValues(handData, grabPoint.rightHandPose);
                RotateFingers(handData, grabPoint.rightHandPose);
                ApplyHandAttachOffset(interactor, grabPoint.rightHandPose, handData);
            }
            else 
            {
                SetHandDataValues(handData, grabPoint.leftHandPose);
            }

            //StartCoroutine(SetHandDataRoutine(handData, finalHandPosition, finalHandRotation, finalFingerRotations, startingHandPosition, startingHandRotation, startingFingerRotations));
        }
    }

    private void RotateFingers(HandData handStart, HandData handFinal)
    {
        for (int i = 0; i < handStart.fingerBones.Length; i++)
        {
            handStart.fingerBones[i].localRotation = handFinal.fingerBones[i].localRotation;
        }
    }

    private void ApplyHandAttachOffset(XRDirectInteractor interactor, HandData pose, HandData hand)
    {
        Vector3 finalPosition = -1.0f * (pose.root.localPosition / 10.0f) + hand.root.localPosition;
        Quaternion finalRotation = Quaternion.Inverse(pose.root.localRotation * Quaternion.Euler(0, 0, 90));

        Vector3 direction = finalPosition - hand.root.localPosition;
        direction = Quaternion.Euler(finalRotation.eulerAngles) * direction;
        finalPosition = direction + hand.root.localPosition;

        interactor.attachTransform.localPosition = finalPosition;
        interactor.attachTransform.localRotation = finalRotation;
    }

    private GrabPoint GetClosestGrabPoint(Vector3 handWorldPosition)
    {
        GrabPoint closestGrabPoint = grabPoints[0];
        float dist = (closestGrabPoint.transform.position - handWorldPosition).sqrMagnitude;

        for (int i = 1; i < grabPoints.Length; i++)
        {
            float newDist = (grabPoints[i].transform.position - handWorldPosition).sqrMagnitude;
            if (newDist < dist)
            {
                dist = newDist;
                closestGrabPoint = grabPoints[i];
            }
        }

        return closestGrabPoint;
    }

    // Called when an interactor drops the object
    private void UnsetPose(BaseInteractionEventArgs arg) {
        if(arg.interactorObject is XRDirectInteractor) {
            HandData handData = arg.interactorObject.transform.GetComponentInChildren<HandData>();
            // WARN: There might be a bug here because I'm enabling the animator before 
            //       running the coroutine but it's hard to notice on my computer so I'll
            //       wait to test on the VR
            handData.animator.enabled = true;

            StartCoroutine(SetHandDataRoutine(handData, startingHandPosition, startingHandRotation, startingFingerRotations, finalHandPosition, finalHandRotation, finalFingerRotations));
        }
    }

    // Setup the variables of this components using h1 as the
    // starting pose and h2 as the final pose
    private void SetHandDataValues(HandData h1, HandData h2) 
    {
        // Divide the localPosition by the scale because the scale
        // of the parent affects the position of the child
        // Ref: https://docs.unity3d.com/ScriptReference/Transform-localPosition.html
        startingHandPosition = new Vector3(
            h1.root.localPosition.x / h1.root.localScale.x, 
            h1.root.localPosition.y / h1.root.localScale.y, 
            h1.root.localPosition.z / h1.root.localScale.z
        );
        finalHandPosition = new Vector3(
            h2.root.localPosition.x / h2.root.localScale.x, 
            h2.root.localPosition.y / h2.root.localScale.y, 
            h2.root.localPosition.z / h2.root.localScale.z
        );

        startingHandRotation = h1.root.localRotation;
        finalHandRotation = h2.root.localRotation;

        startingFingerRotations = new Quaternion[h1.fingerBones.Length];
        finalFingerRotations = new Quaternion[h2.fingerBones.Length];

        for (int i = 0; i < h1.fingerBones.Length; i++) 
        {
            startingFingerRotations[i] = h1.fingerBones[i].localRotation;
            finalFingerRotations[i] = h2.fingerBones[i].localRotation;
        }
    }

    // Corroutine that changes the values of the interactor hand over time
    private IEnumerator SetHandDataRoutine(
        HandData h, 
        Vector3 newPosition, Quaternion newRotation, Quaternion[] newBonesRotation, 
        Vector3 startingPosition, Quaternion startingRotation, Quaternion[] startingBonesRotation
    ) 
    {
        float timer = 0.0f;

        while (timer < poseTransitionDuration)
        {
            Vector3 p = Vector3.Lerp(startingPosition, newPosition, timer / poseTransitionDuration);
            Quaternion r = Quaternion.Lerp(startingRotation, newRotation, timer / poseTransitionDuration);

            h.root.localPosition = p;
            h.root.localRotation = r;

            for(int i = 0; i < newBonesRotation.Length; i++) 
            {
                h.fingerBones[i].localRotation = Quaternion.Lerp(startingBonesRotation[i], newBonesRotation[i], timer / poseTransitionDuration);
            }

            timer += Time.deltaTime;
            yield return null;
        }

        // WARN: Possible bug because the lerp won't always reach 100%
        //       If this becomes a problem maybe add some code here that sets
        //       all the hand data to their final values
    }

//#if UNITY_EDITOR
//    [MenuItem("Tools/Mirror Selected Right Grab Pose")]
//    public static void MirrorRightPose() 
//    {
//        GrabHandPose handPose = Selection.activeGameObject.GetComponent<GrabHandPose>();
//        handPose.MirrorPose(handPose.leftHandPose, handPose.rightHandPose);
//    }
//#endif

    //private void MirrorPose(HandData poseToMirror, HandData poseUsedToMirror) 
    //{
    //    Vector3 mirroredPosition = poseUsedToMirror.root.localPosition;
    //    mirroredPosition.x *= -1;

    //    Quaternion mirroredQuaternion = poseUsedToMirror.root.localRotation;
    //    mirroredQuaternion.y *= -1;
    //    mirroredQuaternion.z *= -1;

    //    poseToMirror.root.localPosition = mirroredPosition;
    //    poseToMirror.root.localRotation = mirroredQuaternion;

    //    for(int i = 0; i < poseUsedToMirror.fingerBones.Length; i++) {
    //        poseToMirror.fingerBones[i].localRotation = poseUsedToMirror.fingerBones[i].localRotation;
    //    }
    //}
}
