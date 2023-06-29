using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using System.Collections.Generic;

public static class SaveSystem
{
    public static void SaveSettings(int sfxVolume, int uiSize)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/settings.data"; // on windows the path is %userprofile%\AppData\LocalLow\<companyname>\<productname>
        FileStream stream = new FileStream(path, FileMode.Create);

        SettingsData data = new SettingsData(sfxVolume, uiSize);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static SettingsData LoadSettings()
    {
        string path = Application.persistentDataPath + "/settings.data";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SettingsData data = formatter.Deserialize(stream) as SettingsData;
            stream.Close();

            return data;
        }
        else
        {
            return null;
        }
    }

    public static void SaveCollectables(List<string> collectableTags)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/collectables.data"; // on windows the path is %userprofile%\AppData\LocalLow\<companyname>\<productname>
        FileStream stream = new FileStream(path, FileMode.Create);

        CollectablesData data = new CollectablesData(collectableTags);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static CollectablesData LoadCollectables()
    {
        string path = Application.persistentDataPath + "/collectables.data";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            CollectablesData data = formatter.Deserialize(stream) as CollectablesData;
            stream.Close();

            return data;
        }
        else
        {
            return null;
        }
    }
}
