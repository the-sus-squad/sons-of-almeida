using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNavigation : MonoBehaviour
{

    public NavMeshAgent agent;
    public GameObject target;

    private bool seenTarget = false;
    

    // Update is called once per frame
    void Update()
    {
        if (seenTarget) {
            agent.SetDestination(target.transform.position);
        }
    }

    public void seeTarget(bool value) {
        seenTarget = value;
    }



    

}
