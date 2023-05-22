public class SettingsData
{
    public int sfxVolume;
    public int uiSize;

    public SettingsData(SFXVolume sfx, UISize ui)
    {
        sfxVolume = sfx.currentSFXVolume;
        uiSize = ui.currentUISize;
    }
}
