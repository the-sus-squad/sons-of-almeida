using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveSystem
{
    public static void SaveSettings(SFXVolume sfx, UISize ui)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/settings.data";
        FileStream stream = new FileStream(path, FileMode.Create);

        SettingsData data = new SettingsData(sfx, ui);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    //public static SettingsData LoadSettings()
    //{
    //    BinaryFormatter formatter = new BinaryFormatter();
    //    string path = Application.persistentDataPath + "/settings.data";
    //}
}
