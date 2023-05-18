using System.Text.RegularExpressions;
using System.Numerics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Vector3 = UnityEngine.Vector3;
using Vector2 = UnityEngine.Vector2;


public class EnemyNavigation : MonoBehaviour
{

    public NavMeshAgent agent;
    public GameObject target;
    
    // When searching, the maximum distance in the mesh to move.
    [Range(0, 4)]
    public float searchRadius;
    public Vector3 destination;

    // public UnityEvent OnTargetCaptured;

    private bool seenTarget = false;
    private bool isCapturingPlayer = false;
    private bool isOnCamera = false;
    
    // Update is called once per frame
    void Update()
    {
        if (isCapturingPlayer) {
        }
        else {
            if (seenTarget) {
                agent.SetDestination(target.transform.position);
            }
            else {
                // If the agent has reached the destination, or if there is no destination, find a new one.
                if ((destination == Vector3.zero || transform.position.x == destination.x && transform.position.z == destination.z) 
                && RandomPoint(transform.position, searchRadius, out destination)) {
                    agent.SetDestination(destination);
                }
            }
        }
        
    }

    public void seeTarget(bool value) {
        seenTarget = value;

        // If lost sight of target, find a new destination.
        if (!value && RandomPoint(transform.position, searchRadius, out destination)) {
            agent.SetDestination(destination);
        }
    }

     bool RandomPoint(Vector3 center, float range, out Vector3 result, int nInteractions = 30)
    {
        for (int i = 0; i < nInteractions; i++)
        {
            Vector2 randomPoint2D = Random.insideUnitCircle * range;
            Vector3 randomPoint = new Vector3(center.x + randomPoint2D.x, center.y, center.z + randomPoint2D.y);

            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
            {
                result = hit.position;
                return true;
            }
        }
        result = Vector3.zero;
        return false;
    }

    public void HearTarget(Vector3 position) {
        // Check if the target is within the search radius.
        if (Vector3.Distance(transform.position, position) < searchRadius) {
            destination = position;
            agent.SetDestination(destination);
            seenTarget = false;
        }
    }

    private void OnTriggerEnter(Collider t) {
        Debug.Log("Triggered collision with " + t.gameObject.name);
        if (t.gameObject == target) {
            CapturePlayer();
        }
    }

    private void CapturePlayer() {
        Debug.Log("Captured player");
        isCapturingPlayer = true;

        agent.isStopped = true;
        // OnTargetCaptured.Invoke();
        // Change destination to targets's front
        if (isOnCamera) {
            // agent.SetDestination(target.transform.position + target.transform.forward * 2);
            Debug.Log("Is on camera");
        }
        else {
            Debug.Log("Is not on camera");
            transform.position = target.transform.position + target.transform.forward * 2;
        }

    }

    void OnBecameInvisible() {
        // #if UNITY_EDITOR
        // if (Camera.current.name == "SceneCamera") 
        //     return;
        // #endif

        Debug.Log("Became invisible");
        isOnCamera = false;
    }

    void OnBecameVisible() {
        // #if UNITY_EDITOR
        // if (Camera.current.name == "SceneCamera") 
        //     return;
        // #endif

        Debug.Log("Became visible");
        isOnCamera = true;
    }

}
