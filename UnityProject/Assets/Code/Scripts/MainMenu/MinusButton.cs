using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MinusButton : MonoBehaviour
{
    public UnityEvent HitMinusButton;

    private void OnTriggerEnter(Collider other)
    {
        HitMinusButton.Invoke();
    }
}
