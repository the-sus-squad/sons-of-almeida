using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEditor;
using System;
using Unity.VisualScripting;

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
    public List<GrabPoint> grabPoints;

    private Quaternion[] startingFingerRotations;
    private Quaternion[] finalFingerRotations;
    private Quaternion startingHandRotation;
    private Quaternion finalHandRotation;
    private Vector3 startingHandPosition;
    private Vector3 finalHandPosition;

    // Start is called before the first frame update
    void Start()
    {
        XRGrabInteractable grabInteractable = GetComponent<XRGrabInteractable>();

        // Listen to the grab and stop grabbing events
        grabInteractable.selectEntered.AddListener(SetupPose);
        grabInteractable.selectExited.AddListener(UnsetPose);

        grabInteractable.hoverExited.AddListener(ReleaseObject);
    }

    // Called when an interactor starts grabbing the object
    private void SetupPose(BaseInteractionEventArgs arg) 
    {
        XRDirectInteractor interactor = arg.interactorObject.transform.GetComponent<XRDirectInteractor>();
        
        HandData startingHand = interactor.transform.GetComponentInChildren<HandData>();
        startingHand.animator.enabled = false;
        PlayGrabSound(startingHand);

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

        // If the hand is dropping an object then activate the animator
        // but if it is grabbing an object then play the grab sound
        if (isUnsetting)
        {
            handData.animator.enabled = true;
        }
    }

    private GrabPoint GetClosestGrabPoint(Vector3 handWorldPosition)
    {
        GrabPoint closestGrabPoint = grabPoints[0];
        float dist = (closestGrabPoint.transform.position - handWorldPosition).sqrMagnitude;

        for (int i = 1; i < grabPoints.Count; i++)
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
        HandData handData = arg.interactorObject.transform.GetComponentInChildren<HandData>();

        StartCoroutine(RotateFingers(handData, finalFingerRotations, startingFingerRotations, true));
    }

    private void PlayGrabSound(HandData hand)
    {
        AudioSource audioSource = hand.transform.GetComponent<AudioSource>();
        audioSource.pitch = UnityEngine.Random.Range(0.5f, 1.5f);
        audioSource.Play();
    }

    private void ReleaseObject(HoverExitEventArgs arg)
    {
        XRDirectInteractor interactor = arg.interactorObject.transform.GetComponent<XRDirectInteractor>();

        if (interactor.hasSelection)
        {
            StartCoroutine(ReleaseObjectRoutine(interactor));
        }
    }

    private IEnumerator ReleaseObjectRoutine(XRDirectInteractor interactor)
    {
        interactor.allowSelect = false;
        yield return new WaitForSeconds(0.2f);
        interactor.allowSelect = true;
    }

#if UNITY_EDITOR
    // Function called by the "Add Grab Button" inspector button
    public void AddGrabPoint()
    {
        string path = "Assets/Level/Prefabs/GrabPoint.prefab";
        UnityEngine.Object grabPoint = AssetDatabase.LoadAssetAtPath(path, typeof(GameObject));

        if (grabPoint)
        {
            UnityEngine.Object clone = PrefabUtility.InstantiatePrefab(grabPoint, transform);
            Undo.RegisterCreatedObjectUndo(clone, "Instantiated GrabPoint");

            Undo.RecordObject(this, "Add GrabPoint to the list of GrabPoints");
            grabPoints.Add(clone.GetComponent<GrabPoint>());
        }
        else
        {
            Debug.LogError("GrabPoint prefab could not be loaded at: " + path);
        }
    }
#endif
}
