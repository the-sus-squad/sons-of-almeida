using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlusButton : MonoBehaviour
{
    public UnityEvent HitPlusButton;

    private void OnTriggerEnter(Collider other)
    {
        HitPlusButton.Invoke();
    }
}
