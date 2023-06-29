using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{

    public SubtitleInteraction subtitle;
    public EnemyRevealer enemyRevealer;

    public GameObject door;
    private bool closingDoor = false;
    private float closingTime = 0f;
    private float closingSpeed = 3f;
    private float initialRotation = -79.34f;
    private float finalRotation = 0;

    private AudioSource audioSource;

    void Start() {
        audioSource = GetComponent<AudioSource>();
    }

    void Update() {

        if (closingDoor) {
            var rotation = door.transform.rotation;
            door.transform.rotation = Quaternion.Euler(-90, rotation.y, Mathf.Lerp(initialRotation, finalRotation, closingTime * closingSpeed));
            closingTime += Time.deltaTime;
        }

        if (closingTime > 1f) {
            closingDoor = false;
            closingTime = 0f;
        }

    }

    public void StartDialogue() {
        // StartCoroutine(StartDialogueRoutine());
        subtitle.ShowMessage("I need to find a way out of here...");

    }

    IEnumerator StartDialogueRoutine() {
        subtitle.ShowMessage("I need to find a way out of here...");
        return null;
    }

    public void StartChurch() {
        StartCoroutine(StartChurchRoutine());
    }

    IEnumerator StartChurchRoutine() {
        subtitle.ShowMessage("What is this place? I don't think I should be here longer than I need to...");
        yield return new WaitForSeconds(5f);
        subtitle.ShowMessage("That on the floor... Is that blood?");
    }

    public void EndOfChurch() {
        StartCoroutine(EndOfChurchRoutine());
    }

    void CloseDoor(float initialRotation = -79.34f) {
        // door.transform.rotation = Quaternion.Euler(0, initialRotation, 0);
        closingDoor = true;
        audioSource.Play();
    }

    IEnumerator EndOfChurchRoutine() {
        CloseDoor();
        enemyRevealer.ActivateEnemies();
        yield return new WaitForSeconds(1f);
        subtitle.ShowMessage("What are those noises?");
        yield return new WaitForSeconds(5f);

        subtitle.ShowMessage("Oh no, the exit is locked? I need to find the key to open it somewhere!");
        yield return new WaitForSeconds(6f);

        subtitle.ShowMessage("Oh no...");
    }
}
