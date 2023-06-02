using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarsDisplay : MonoBehaviour
{
    [SerializeField] private GameObject[] bars;

    public void AddBar()
    {
        for (int i = 0; i < bars.Length; i++)
        {
            if (!bars[i].activeInHierarchy)
            {
                bars[i].SetActive(true);
                break;
            }
        }
    }

    public void RemoveBar()
    {
        for (int i = bars.Length - 1; i >= 0; i--)
        {
            if (bars[i].activeInHierarchy)
            {
                bars[i].SetActive(false);
                break;
            }
        }
    }

    public void SetBars(int value)
    {
        for (int i = 0; i < value; i++)
        {
            bars[i].SetActive(true);
        }

        for (int i = value; i < bars.Length; i++)
        {
            bars[i].SetActive(false);
        }
    }
}
