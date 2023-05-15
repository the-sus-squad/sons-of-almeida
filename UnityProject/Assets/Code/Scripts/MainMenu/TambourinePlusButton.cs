using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TambourinePlusButton : MonoBehaviour
{
    public UnityEvent IncreaseSFXVolume;

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Right Hand Tambourine Collider" || other.name == "Left Hand Tambourine Collider")
        {
            IncreaseSFXVolume.Invoke();
        }
    }
}
