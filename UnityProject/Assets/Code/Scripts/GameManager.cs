using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private SFXVolume sfxVolume;
    [SerializeField] private UISize uiSize;
    
    private void Start()
    {
        SettingsData settingsData = SaveSystem.LoadSettings();
        if (settingsData != null)
        {
            sfxVolume.LoadVolume(settingsData.sfxVolume);
            uiSize.LoadSize(settingsData.uiSize);
        }
        else
        {
            SaveSettings();
        }
    }

    public void SaveSettings()
    {
        SaveSystem.SaveSettings(sfxVolume, uiSize);
    }
}
