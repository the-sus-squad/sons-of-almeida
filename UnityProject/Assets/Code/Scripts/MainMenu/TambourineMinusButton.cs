using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TambourineMinusButton : MonoBehaviour
{
    public UnityEvent DecreaseSFXVolume;

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Right Hand Tambourine Collider" || other.name == "Left Hand Tambourine Collider")
        {
            DecreaseSFXVolume.Invoke();
        }
    }
}
