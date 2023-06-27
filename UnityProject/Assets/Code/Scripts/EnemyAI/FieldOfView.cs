using System.Numerics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using Vector3 = UnityEngine.Vector3;

public class FieldOfView : MonoBehaviour
{
    public float radius;

    [Range(0, 360)]
    public float angle;
    public GameObject targetObject;
    public LayerMask targetMask;
    public LayerMask obstructionMask;
    public bool canSeePlayer;

    public UnityEvent OnTargetSeen;
    public UnityEvent OnTargetHidden;
    
    private bool first_time = true;

    void Update() {
        // For some reason on the Start method the coroutine does not work anymore. I surrender and did this, sorry comrades.
        if (first_time) {
            StartCoroutine(FOVRoutine());
            first_time = false;
        }
    }

    // Not call every frame
    private IEnumerator FOVRoutine() {
        float delay = 0.0f;
        var wait = new WaitForSeconds(delay);

        while (true) {
            // Pause until the next frame
            yield return wait;
            Check();
            
        }
    }

    private void Check() {
        void FailCheck() {
            canSeePlayer = false;

            OnTargetHidden.Invoke();
        }

        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);

        if (rangeChecks.Length != 0) {
            Transform target = rangeChecks[0].transform; // Cursed hardcoded index, however it is because there is just one targetObject
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            float distanceToTarget = Vector3.Distance(transform.position, target.position);

            // If not obstructed
            if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask)) {

                if (canSeePlayer) {
                    OnTargetSeen.Invoke();
                    return;
                }

                // If in vision angle
                if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2) {
                    Debug.Log("Can see player");
                    canSeePlayer = true;
                    OnTargetSeen.Invoke();
                }

                else {
                    FailCheck();
                }
                
            }
            
            else {
                FailCheck();
            }
        }
        else if (canSeePlayer) {
            FailCheck();
        }

    }
}
