using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SFXVolume : MonoBehaviour
{
    [SerializeField] private AudioMixerGroup soundEffectsMixerGroup;

    [SerializeField] private BarsDisplay barsDisplay;

    private int maxSFXVolume = 10;
    private int minSFXVolume = 0;
    public int currentSFXVolume = 10;

    public void IncreaseSFXVolume()
    {
        if (currentSFXVolume < maxSFXVolume)
        {
            currentSFXVolume++;
        }

        UpdateSFXVolume();
    }

    public void DecreaseSfxVolume()
    {
        if (currentSFXVolume > minSFXVolume)
        {
            currentSFXVolume--;
        }

        UpdateSFXVolume();
    }

    private void UpdateSFXVolume()
    {
        float dbVolume = 20.0f * Mathf.Log10(currentSFXVolume / 10.0f);
        if (currentSFXVolume == 0)  // the value can't be exactly zero couse the decibels will wrap around... yeah idk
        {
            dbVolume = 20.0f * Mathf.Log10(0.0001f / 10.0f);
        }
        soundEffectsMixerGroup.audioMixer.SetFloat("Sound Effects Volume", dbVolume);
    }

    public void LoadVolume(int volume)
    {
        currentSFXVolume = volume;
        barsDisplay.SetBars(currentSFXVolume);
        UpdateSFXVolume();
    }
}
