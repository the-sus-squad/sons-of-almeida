using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Transform doorHandle;
    
    public bool isLocked = false;

    private HingeJoint doorHandleHinge;
    private Rigidbody doorRB;
    private HingeJoint doorHinge;

    private float threshold = 1;

    // Start is called before the first frame update
    void Start()
    {
        doorHandleHinge = doorHandle.GetComponent<HingeJoint>();
        doorRB = GetComponent<Rigidbody>();
        doorHinge = GetComponent<HingeJoint>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLocked)
        {
            if (doorHinge.angle <= doorHinge.limits.min + threshold)
            {
                doorRB.freezeRotation = true;
            }

            if (doorHandleHinge.angle >= doorHandleHinge.limits.max - threshold)
            {
                doorRB.freezeRotation = false;
            }
        }
    }
}
