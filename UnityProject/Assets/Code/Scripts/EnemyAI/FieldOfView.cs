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
    public GameObject player;
    public LayerMask targetMask;
    public LayerMask obstructionMask;
    public bool canSeePlayer;

    public UnityEvent OnTargetSeen;
    public UnityEvent OnTargetHidden;
    
    

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FOVRoutine());
    }

    // Not call every frame
    private IEnumerator FOVRoutine() {
        float delay = 0.2f;
        WaitForSeconds wait = new WaitForSeconds(delay);

        while (true) {
            // wtf
            yield return wait;
            Check();
            
        }
    }

    private void Check() {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);

        if (rangeChecks.Length != 0) {
            Transform target = rangeChecks[0].transform; // Cursed hardcoded index, however it is because there is just one player
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2) {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask)) {
                    canSeePlayer = true;
                    OnTargetSeen.Invoke();
                }
                else {
                    canSeePlayer = false;
                    OnTargetHidden.Invoke();

                }
            }
            else {
                // Player not here
                canSeePlayer = false;
                OnTargetHidden.Invoke();
            }
        }
        else if (canSeePlayer) {
            canSeePlayer = false;
            OnTargetHidden.Invoke();
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
