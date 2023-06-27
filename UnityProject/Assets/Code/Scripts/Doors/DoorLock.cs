using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DoorLock : MonoBehaviour
{
    [SerializeField] private Door door;
    [SerializeField] private Transform key;

    [SerializeField] private Vector3 keyStartingPosition;
    [SerializeField] private Vector3 keyStartingRotation;

    private Rigidbody keyRB;
    private bool keyIsInserted = false;
    private bool isLocked = true;

    private void Start()
    {
        keyRB = key.GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (ReferenceEquals(other.gameObject, key.gameObject))
        {
            key.position = keyStartingPosition;
            key.eulerAngles = keyStartingRotation;
            keyRB.constraints = RigidbodyConstraints.FreezePosition 
                | RigidbodyConstraints.FreezeRotationX 
                | RigidbodyConstraints.FreezeRotationY;
            keyIsInserted = true;
        }
    }

    private void FixedUpdate()
    {
        if (isLocked && keyIsInserted && (Vector3.Dot(key.up, new Vector3(1, 0, 0)) > 0.95f || Vector3.Dot(key.up, new Vector3(-1, 0, 0)) > 0.95f))
        {
            isLocked = false;
            key.GetComponent<XRGrabInteractable>().enabled = false;
            keyRB.constraints = RigidbodyConstraints.None;
            keyRB.isKinematic = true;
            keyRB.detectCollisions = false;
            key.SetParent(transform);
            door.isLocked = isLocked;
        }
    }
}
