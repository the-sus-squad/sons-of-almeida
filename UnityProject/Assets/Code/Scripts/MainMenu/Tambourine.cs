using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tambourine : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("UI Direct Interactor"))
        {
            GetComponent<AudioSource>().Play();
        }
    }
}
