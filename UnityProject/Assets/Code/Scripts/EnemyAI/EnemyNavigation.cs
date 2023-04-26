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


    private bool seenTarget = false;
    
    // Update is called once per frame
    void Update()
    {
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

}
