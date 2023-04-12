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
 * adding a Grab Point to the list of grab points of this object,
 * adding the right and left hand models as children of that grab point
 * object and then assigning these poses to the corresponding variables
 * in the inspector.
 */
public class GrabHandPose : MonoBehaviour
{
    [SerializeField] private float poseTransitionDuration = 0.2f;
    [SerializeField] private GrabPoint[] grabPoints;

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

    // Called when an interactor starts grabbing the object
    private void SetupPose(BaseInteractionEventArgs arg) 
    {
        if (arg.interactorObject is XRDirectInteractor) 
        {
            XRDirectInteractor interactor = arg.interactorObject.transform.GetComponent<XRDirectInteractor>();
            HandData startingHand = arg.interactorObject.transform.GetComponentInChildren<HandData>();
            startingHand.animator.enabled = false;

            GrabPoint grabPoint = GetClosestGrabPoint(startingHand.root.position);
            
            HandData handPose = grabPoint.leftHandPose;
            if (startingHand.handType == HandData.HandModelType.Right) 
            {
                handPose = grabPoint.rightHandPose;
            }

            SaveFingerRotations(startingHand, handPose);
            ApplyHandAttachOffset(interactor, handPose, startingHand);
            StartCoroutine(RotateFingers(startingHand, startingFingerRotations, finalFingerRotations));
        }
    }

    // Cache the starting and ending rotations of the finger for later use
    private void SaveFingerRotations(HandData startingHand, HandData finalHand)
    {
        startingFingerRotations = new Quaternion[startingHand.fingerBones.Length];
        finalFingerRotations = new Quaternion[startingHand.fingerBones.Length];
        for (int i = 0; i < startingHand.fingerBones.Length; i++)
        {
            startingFingerRotations[i] = startingHand.fingerBones[i].localRotation;
            finalFingerRotations[i] = finalHand.fingerBones[i].localRotation;
        }
    }

    // Offset the Attach Transform of the interactor to fit the object we're grabbing
    private void ApplyHandAttachOffset(XRDirectInteractor interactor, HandData pose, HandData hand)
    {
        // Divide the localPosition by the scale because the scale
        // of the parent affects the position of the child
        // Ref: https://docs.unity3d.com/ScriptReference/Transform-localPosition.html
        Vector3 posePosition = new Vector3(
            pose.root.localPosition.x / Math.Abs(pose.root.localScale.x),
            pose.root.localPosition.y / Math.Abs(pose.root.localScale.y),
            pose.root.localPosition.z / Math.Abs(pose.root.localScale.z)
        );
        // The attach position will be the inverse of the pose position, 
        // but I have to add the localPosition of the hand because the hand
        // has an offset
        Vector3 finalAttachPosition = (-1.0f * posePosition) + hand.root.localPosition;
        
        // I add 90 or -90 to the localRotation to nullify the hands starting with a rotation
        // of 90 or -90 degrees
        Quaternion finalAttachRotation = Quaternion.Inverse(pose.root.localRotation * Quaternion.Euler(0, 0, 90));
        if (pose.handType == HandData.HandModelType.Left)
            finalAttachRotation = Quaternion.Inverse(pose.root.localRotation * Quaternion.Euler(0, 0, -90));

        finalAttachPosition = RotatePointAroundPivot(finalAttachPosition, hand.root.localPosition, finalAttachRotation.eulerAngles);

        interactor.attachTransform.localPosition = finalAttachPosition;
        interactor.attachTransform.localRotation = finalAttachRotation;
    }

    private Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, Vector3 angles)
    {
        Vector3 direction = point - pivot;
        direction = Quaternion.Euler(angles) * direction;
        return direction + pivot;
    }

    // Rotate the fingers over time
    private IEnumerator RotateFingers(HandData handData, Quaternion[] startingFingerRotations, Quaternion[] finalFingerRotations, bool isUnsetting = false)
    {
        float timer = 0.0f;

        while (timer < poseTransitionDuration)
        {
            for (int i = 0; i < handData.fingerBones.Length; i++)
            {
                handData.fingerBones[i].localRotation = Quaternion.Lerp(
                    startingFingerRotations[i], 
                    finalFingerRotations[i], 
                    timer / poseTransitionDuration
                );
            }

            timer += Time.deltaTime;
            yield return null;
        }

        if (isUnsetting)
        {
            handData.animator.enabled = true;
        }
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

            StartCoroutine(RotateFingers(handData, finalFingerRotations, startingFingerRotations, true));
        }
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
