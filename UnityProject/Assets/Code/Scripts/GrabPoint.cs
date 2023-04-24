using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabPoint : MonoBehaviour
{
    public HandData rightHandPose;
    public HandData leftHandPose;

    // Start is called before the first frame update
    void Start()
    {
        rightHandPose.gameObject.SetActive(false);
        leftHandPose.gameObject.SetActive(false);

        // Reparent the hands to the grabbable object so that the position calculations
        // are done relative to the object and not to the grab point
        rightHandPose.transform.SetParent(transform.parent);
        leftHandPose.transform.SetParent(transform.parent);
    }

    public void MirrorHandPose(HandData handToMirror)
    {
        HandData mirroredHand = rightHandPose;
        if (handToMirror.handType == HandData.HandModelType.Right)
        {
            mirroredHand = leftHandPose;
        }

        mirroredHand.root.localPosition = new Vector3(
            -handToMirror.root.localPosition.x,
            handToMirror.root.localPosition.y,
            handToMirror.root.localPosition.z
        );

        mirroredHand.root.localRotation = new Quaternion(
            handToMirror.root.localRotation.x,
            -handToMirror.root.localRotation.y,
            -handToMirror.root.localRotation.z,
            handToMirror.root.localRotation.w
        );

        for (int i = 0; i < handToMirror.fingerBones.Length; i++)
        {
            mirroredHand.fingerBones[i].localRotation = new Quaternion(
                handToMirror.fingerBones[i].localRotation.x,
                handToMirror.fingerBones[i].localRotation.y,
                handToMirror.fingerBones[i].localRotation.z,
                handToMirror.fingerBones[i].localRotation.w
            );
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawIcon(transform.position, "Hand.png", false);
    }
#endif
}
