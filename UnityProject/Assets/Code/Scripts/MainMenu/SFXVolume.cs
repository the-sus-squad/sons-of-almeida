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

    private void Start()
    {
        barsDisplay.SetBars(SettingsManager.Instance.sfxVolume);
        UpdateSFXVolume();
    }

    public void IncreaseSFXVolume()
    {
        if (SettingsManager.Instance.sfxVolume < maxSFXVolume)
        {
            SettingsManager.Instance.sfxVolume++;
        }

        UpdateSFXVolume();
    }

    public void DecreaseSfxVolume()
    {
        if (SettingsManager.Instance.sfxVolume > minSFXVolume)
        {
            SettingsManager.Instance.sfxVolume--;
        }

        UpdateSFXVolume();
    }

    private void UpdateSFXVolume()
    {
        float dbVolume = 20.0f * Mathf.Log10(SettingsManager.Instance.sfxVolume / 10.0f);
        if (SettingsManager.Instance.sfxVolume == 0)  // the value can't be exactly zero couse the decibels will wrap around... yeah idk
        {
            dbVolume = 20.0f * Mathf.Log10(0.0001f / 10.0f);
        }
        soundEffectsMixerGroup.audioMixer.SetFloat("Sound Effects Volume", dbVolume);
    }
}
