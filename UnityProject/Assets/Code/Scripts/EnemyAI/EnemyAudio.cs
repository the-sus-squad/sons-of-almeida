using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAudio : MonoBehaviour
{

    public AudioSource audioPlayer;
    private bool startedChaseSound = false;
    public AudioClip chaseLoopSound;
    public AudioClip chaseBeginSound;
    public AudioClip gameOverSound;

    void Update()
    {
        // Create the 2nd part loop
        if (startedChaseSound && !audioPlayer.isPlaying) {
            audioPlayer.clip = chaseLoopSound;
            audioPlayer.Play();
        }
    }

    public void PlayChaseTheme() {
        if (audioPlayer.isPlaying) { return; }
        audioPlayer.clip = chaseBeginSound;
        audioPlayer.Play();
        startedChaseSound = true;
    }

    public void StopTheme() {
        audioPlayer.Stop();
        startedChaseSound = false;
    }

    public void PlayGameOverTheme() {
        audioPlayer.clip = gameOverSound;
        audioPlayer.Play();
    }
}
