using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TambourineVolumeDisplay : MonoBehaviour
{
    [SerializeField] private GameObject[] volumeBars;

    public void AddSoundBar()
    {
        for (int i = 0; i < volumeBars.Length; i++)
        {
            if (!volumeBars[i].activeInHierarchy)
            {
                volumeBars[i].SetActive(true);
                break;
            }
        }
    }

    public void RemoveSoundBar()
    {
        for (int i = volumeBars.Length - 1; i >= 0; i--)
        {
            if (volumeBars[i].activeInHierarchy)
            {
                volumeBars[i].SetActive(false);
                break;
            }
        }
    }
}
