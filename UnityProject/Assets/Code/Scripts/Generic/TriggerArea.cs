using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerArea : MonoBehaviour
{
    public GameObject targetCollider;
    public bool oneUseOnly = false;

    public UnityEvent onTriggerEnter;
    public UnityEvent onTriggerExit;
    

    void OnTriggerEnter(Collider other) {
        if (other.gameObject == targetCollider) {
            onTriggerEnter.Invoke();
            if (oneUseOnly) {
                Destroy(gameObject);
            }
        }
    }

    void OnTriggerExit(Collider other) {
        if (other.gameObject == targetCollider) {
            onTriggerExit.Invoke();
        }
    }
}
