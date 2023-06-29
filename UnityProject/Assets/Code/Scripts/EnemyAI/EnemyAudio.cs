using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAudio : MonoBehaviour
{

    public AudioSource musicPlayer;
    public AudioSource sfxPlayer;
    private bool startedChaseSound = false;
    public AudioClip chaseLoopSound;
    public AudioClip chaseBeginSound;
    public AudioClip gameOverSound;
    public AudioClip footStepSound;
    
    

    void Update()
    {
        // Create the 2nd part loop
        if (startedChaseSound && !musicPlayer.isPlaying) {
            musicPlayer.clip = chaseLoopSound;
            musicPlayer.Play();
        }
    }

    public void PlayChaseTheme() {
        if (musicPlayer.isPlaying) { return; }
        musicPlayer.clip = chaseBeginSound;
        musicPlayer.Play();
        startedChaseSound = true;
    }

    public void StopTheme() {
        musicPlayer.Stop();
        startedChaseSound = false;
    }

    public void PlayGameOverTheme() {
        musicPlayer.clip = gameOverSound;
        musicPlayer.Play();
    }

    public void PlayFootSteps() {
        var randomPitch = Random.Range(0.6f, 1f);
        sfxPlayer.pitch = randomPitch;
        sfxPlayer.Play();
        StartCoroutine(StopFootSteps());
    }

    IEnumerator StopFootSteps() {
        yield return new WaitForSeconds(sfxPlayer.clip.length);
        sfxPlayer.Stop();
    }
}
