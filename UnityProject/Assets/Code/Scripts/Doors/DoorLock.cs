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

    private bool keyIsInserted = false;
    private bool isLocked = true;

    private void OnTriggerEnter(Collider other)
    {
        if (ReferenceEquals(other.gameObject, key.gameObject))
        {
            key.position = keyStartingPosition;
            key.eulerAngles = keyStartingRotation;
            key.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition 
                | RigidbodyConstraints.FreezeRotationX 
                | RigidbodyConstraints.FreezeRotationY;
            keyIsInserted = true;
        }
    }

    private void Update()
    {
        if (isLocked && keyIsInserted && (Vector3.Dot(key.up, new Vector3(1, 0, 0)) > 0.95f || Vector3.Dot(key.up, new Vector3(-1, 0, 0)) > 0.95f))
        {
            //Debug.Log(key.eulerAngles.z > 90.0f || key.eulerAngles.z < -90.0f);
            Debug.Log(Vector3.Dot(key.up, new Vector3(1, 0, 0)));
            isLocked = false;
            key.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            key.GetComponent<XRGrabInteractable>().enabled = false;
            door.isLocked = isLocked;
        }
    }
}
