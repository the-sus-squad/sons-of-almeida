using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class FootstepsSoundManager : MonoBehaviour
{
    [SerializeField] private AudioClip concreteFootsteps;
    [SerializeField] private AudioClip reverbFootsteps;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayFootsteps(Transform ground)
    {
        // Check what kinda floor I'm landing on
        switch (ground.tag)
        {
            case "ConcreteFootsteps":
                audioSource.clip = concreteFootsteps;
                break;
            case "ReverbFootsteps":
                audioSource.clip = reverbFootsteps;
                break;
            default:
                audioSource.clip = concreteFootsteps;
                break;
        }

        // Play the sound
        audioSource.Play();
    }
}
