using System;

[Serializable]
public class SettingsData
{
    public int sfxVolume;
    public int uiSize;

    public SettingsData(int sfx, int ui)
    {
        sfxVolume = sfx;
        uiSize = ui;
    }
}
