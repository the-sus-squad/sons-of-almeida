using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlusButton : MonoBehaviour
{
    public UnityEvent HitPlusButton;

    private void OnTriggerEnter(Collider other)
    {
        GetComponentInChildren<Image>().color = new Color(0.5283019f, 0.5283019f, 0.5283019f);
        HitPlusButton.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        GetComponentInChildren<Image>().color = new Color(1.0f, 1.0f, 1.0f);
    }
}
