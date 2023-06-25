using System;
using System.Net.Mime;
using System.Text.RegularExpressions;
using System.Numerics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.Animations;
using UnityEngine.SceneManagement;
using Vector3 = UnityEngine.Vector3;
using Vector2 = UnityEngine.Vector2;
using Random = UnityEngine.Random;


public class EnemyNavigation : MonoBehaviour
{
    public NavMeshAgent agent;

    
    private Vector3 destination;
    public Vector3 destinationErrorMargin = new Vector3(0.5f, 0.5f, 0.5f);
    private Vector3 walkingErrorMargin = new Vector3(0.1f, 0.1f, 0.1f);
    private Delegate OnDestinationReached;
    private object[] args;


    // public UnityEvent OnTargetCaptured;
    public Animator animator;

    private bool wasWalking = false;

    // private bool isStopped = false;

    void Start() {
        animator = GetComponent<Animator>();
    }

    
    // Update is called once per frame
    void Update()
    {

        // O_O autocomplete deu ideia foda
        // TODO: change to state
        bool isWalking = HasDestination(walkingErrorMargin);

        if (isWalking) {
            animator.SetBool("isRunning", true);
        } else {
            if (OnDestinationReached != null && wasWalking && !isWalking) {
                animator.SetBool("isRunning", false);
                OnDestinationReached.DynamicInvoke(args);
            }
        }    
        wasWalking = isWalking;
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

    public void SetDestination(Vector3 destination) {
        this.destination = destination;
        agent.SetDestination(destination);
    }

    public void SetRandomDestination(float radius) {
        if (RandomPoint(transform.position, radius, out destination)) {
            SetDestination(destination);
        }
    }

    public void TeleportTo(Vector3 position) {
        // transform.position = targetCamera.transform.position + targetCamera.transform.forward;
        agent.Warp(position);
    }

    // Vector3 does not have default parameter :(
    private bool HasDestination() {
        return HasDestination(destinationErrorMargin);
    }

    private bool HasDestination(Vector3 errorMargin) {
        return !(destination == Vector3.zero || (transform.position.x < destination.x + errorMargin.x && transform.position.x > destination.x - errorMargin.x
            && transform.position.z < destination.z + errorMargin.z && transform.position.z > destination.z - errorMargin.z));
    }

    public void Stop() {
        agent.isStopped = true;
    }

    public void Resume() {
        agent.isStopped = false;
    }

    public void OnReachedDestination(Delegate callback, params object[] args) {
        OnDestinationReached = callback;
        this.args = args;
    }

    public void OnReachedDestination(Action callback) {
        OnDestinationReached = callback;
        this.args = null;
    }

    public void RemoveOnReachedDestination() {
        OnDestinationReached = null;
    }

    public void SetAnimationBool(string animationName, bool value) {
        animator.SetBool(animationName, value);
    }


}
