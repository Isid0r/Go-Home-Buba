using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveManager
{
    static SaveManager()
    {
        Levels.GetInstance();
    }
    public static void SaveLevelsProgress()
    {
        BinaryFormatter bf = new BinaryFormatter();
        string path = Application.persistentDataPath + "/levels.sv";
        FileStream fs = new FileStream(path, FileMode.Create);
        bf.Serialize(fs, Levels.instance);
        fs.Close();
    }
    public static void LoadLevelsProgress()
    {
        string path = Application.persistentDataPath + "/levels.sv";
        if (File.Exists(path))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = new FileStream(path, FileMode.Open);
            Levels.instance = bf.Deserialize(fs) as Levels;
            fs.Close();
        }     
    }
}
