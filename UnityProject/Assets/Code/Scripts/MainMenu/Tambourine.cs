using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tambourine : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Right Hand Tambourine Collider" || other.name == "Left Hand Tambourine Collider")
        {
            GetComponent<AudioSource>().Play();
        }
    }
}
