using System.Net.Mime;
using System.Text.RegularExpressions;
using System.Numerics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using Vector3 = UnityEngine.Vector3;
using Vector2 = UnityEngine.Vector2;


public class EnemyNavigation : MonoBehaviour
{

    public NavMeshAgent agent;
    public GameObject target;
    public GameObject targetCollider;
    
    // When searching, the maximum distance in the mesh to move.
    [Range(0, 10)]
    public float searchRadius;
    public float searchTime = 2.5f;
    private float searchTimer = 0.0f;
    private Vector3 destination;
    private bool hasSearched = false;

    // public UnityEvent OnTargetCaptured;

    private bool seenTarget = false;
    private bool isCapturingPlayer = false;
    private bool isOnCamera = false;
    public Camera targetCamera;

    public float jumpscareTime = 10.0f;
    private float jumpscareTimer = 0.0f;
    public AudioSource gameOverSound;



    
    // Update is called once per frame
    void Update()
    {
        if (isCapturingPlayer) {
            jumpscareTimer += Time.deltaTime;
            if (jumpscareTimer >= jumpscareTime) {
                jumpscareTimer = 0.0f;
                isCapturingPlayer = false;
                GameOver();
            }
        }
        else {
            if (seenTarget) {
                agent.SetDestination(target.transform.position);
            }
            else {
                if (!hasSearched) {
                   // While searching, stop the agent from moving.
                    if (searchTimer < searchTime) {
                        Debug.Log("Searching");
                        searchTimer += Time.deltaTime;
                        agent.isStopped = true;
                    }
                    else {
                        searchTimer = 0.0f;
                        agent.isStopped = false;
                        hasSearched = true;
                    }
                }

                else {
                    // If the agent has reached the destination, or if there is no destination, find a new one.
                    if ((destination == Vector3.zero || transform.position.x == destination.x && transform.position.z == destination.z) 
                    && RandomPoint(transform.position, searchRadius, out destination)) {
                        agent.SetDestination(destination);
                        hasSearched = false;
                    }
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
        if (!gameObject.activeSelf) return;
        // Check if the target is within the search radius.
        if (Vector3.Distance(transform.position, position) < searchRadius) {
            destination = position;
            agent.SetDestination(destination);
            seenTarget = false;
        }
    }

    private void OnTriggerEnter(Collider t) {
        if (t.gameObject == targetCollider) {
            CapturePlayer();
        }
    }

    private void CapturePlayer() {
        if (isCapturingPlayer) return;

        gameOverSound.Play();
        isCapturingPlayer = true;

        agent.isStopped = true;
        // OnTargetCaptured.Invoke();

        // Change destination to targets's front
        if (isOnCamera) {
        }
        else {
            transform.position = targetCamera.transform.position + targetCamera.transform.forward;
        }

    }

    void OnBecameInvisible() {
        isOnCamera = false;
    }

    void OnBecameVisible() {
        isOnCamera = true;
    }

    void GameOver() {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

}
