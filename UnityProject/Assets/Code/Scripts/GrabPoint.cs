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
}
