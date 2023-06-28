using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{

    public SubtitleInteraction subtitle;
    public EnemyRevealer enemyRevealer;


    public void SayHello() {
        subtitle.ShowMessage("Hello Prologers!");
    }

    public void EndOfChurch() {
        StartCoroutine(EndOfChurchRoutine());
    }

    IEnumerator EndOfChurchRoutine() {
        Debug.Log("Routine");
        subtitle.ShowMessage("Oh no, the exit is locked?");
        yield return new WaitForSeconds(6f);

        subtitle.ShowMessage("I need to find a way out of here!");
        yield return new WaitForSeconds(6f);

        subtitle.ShowMessage("What are those noises?");
        yield return new WaitForSeconds(1f);
        enemyRevealer.ActivateEnemies();
        yield return new WaitForSeconds(5f);
        subtitle.ShowMessage("Oh no...");
    }
}
