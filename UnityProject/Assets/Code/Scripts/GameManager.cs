using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private SFXVolume sfxVolume;
    [SerializeField] private UISize uiSize;

    private void Start()
    {
        SettingsData data = SaveSystem.LoadSettings();
        //sfxVolume.UpdateVolume(data.sfxVolume);
        //uiSize.UpdateSize(data.uiSize);
    }

    public void SaveSettings()
    {
        SaveSystem.SaveSettings(sfxVolume, uiSize);
    }
}
