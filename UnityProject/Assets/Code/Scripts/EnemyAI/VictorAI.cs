using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictorAI : EnemyAI
{

    public float searchTime = 2.5f;
    private Timer searchTimer;

    // private bool hasSearched = false; // TODO: maybe it can be removed?

    void Start() {
        searchTimer = gameObject.AddComponent<Timer>();
        SearchForTargetTime();
    }

    public override void seeTarget(bool value) {
        base.seeTarget(value);
        
        if (value) {
            audioPlayer.PlayChaseTheme();
            navigation.SetDestination(target.transform.position);
            navigation.RemoveOnReachedDestination();
        }

        else {
            // If lost sight of target, find a new destination.
            SearchForTargetTime();
        }  
    }

    public override void HearTarget(Vector3 position) {

        // Check if the target is within the search radius.
        if (Vector3.Distance(transform.position, position) < searchRadius) {
            navigation.SetDestination(position);
            navigation.PlayAnimation("Running");
            hasTarget = false;
            // hasSearched = true;
        }
    }

    void SearchForTarget() {
        if (hasTarget) return;

        navigation.SetRandomDestination(searchRadius);
        navigation.OnReachedDestination(SearchForTargetTime);

    }

    void SearchForTargetTime() {
        navigation.PlayAnimation("Searching");
        searchTimer.SetTimer(searchTime, SearchForTarget);
    }
}
