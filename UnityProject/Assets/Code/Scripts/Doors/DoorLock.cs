using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorLock : MonoBehaviour
{
    [SerializeField] private Door door;
    [SerializeField] private Transform key;

    [SerializeField] private Vector3 keyStartingPosition;
    [SerializeField] private Vector3 keyStartingRotation;

    private void OnTriggerEnter(Collider other)
    {
        if (ReferenceEquals(other.gameObject, key.gameObject))
        {
            key.position = keyStartingPosition;
            key.eulerAngles = keyStartingRotation;
            key.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition 
                | RigidbodyConstraints.FreezeRotationX 
                | RigidbodyConstraints.FreezeRotationY;
        }
    }

    private void Update()
    {
        
    }
}
