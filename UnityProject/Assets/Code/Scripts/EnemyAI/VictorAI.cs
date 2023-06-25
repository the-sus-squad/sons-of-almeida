using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictorAI : EnemyAI
{

    public float searchTime = 2.5f;
    private Timer searchTimer;

    override protected void Start() {
        base.Start();
        searchTimer = gameObject.AddComponent<Timer>();
        SearchForTargetTime();
        navigation.OnReachedDestination(SearchForTargetTime);

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

        if (!gameObject.activeSelf) return;
        // Check if the target is within the search radius.
        if (Vector3.Distance(transform.position, position) < searchRadius) {
            navigation.SetDestination(position);
            hasTarget = true;
        }
    }

    protected override void CapturePlayer() {
        navigation.SetAnimationBool("isSearching", false);
        navigation.SetAnimationBool("isRunning", false);
        base.CapturePlayer();
        
    }

    void SearchForTarget() {
        if (hasTarget) {return;}
        navigation.SetRandomDestination(searchRadius);
    }

    void SearchForTargetTime() {
        hasTarget = false;
        navigation.SetAnimationBool("isSearching", true);
        searchTimer.SetTimer(searchTime, SearchForTarget);
    }
}
