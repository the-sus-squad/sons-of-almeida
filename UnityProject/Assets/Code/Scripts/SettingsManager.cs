using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    private static SettingsManager _instance;

    public static SettingsManager Instance
    {
        get
        {
            return _instance;
        }
    }

    public int sfxVolume;
    public int uiSize;

    private void Awake()
    {
        if (!_instance)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);

        SettingsData data = SaveSystem.LoadSettings();
        if (data != null)
        {
            sfxVolume = data.sfxVolume;
            uiSize = data.uiSize;
        }
        else
        {
            sfxVolume = 10;
            uiSize = 5;
            SaveSystem.SaveSettings(sfxVolume, uiSize);
        }
    }
}
