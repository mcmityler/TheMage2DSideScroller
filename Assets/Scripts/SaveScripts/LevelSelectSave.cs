using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
public class LevelSelectSave : MonoBehaviour
{
    //Save Player function called when you want to Save Player file
    public static void SaveLS(PlayerInfoScript player)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "LevelSelectInfo.magess";
        FileStream stream = new FileStream(path, FileMode.Create);

        LevelSelectData data = new LevelSelectData(player);

        formatter.Serialize(stream, data);
        stream.Close();
    }
    //Load player function called when you want to load player file
    public static LevelSelectData LoadLS()
    {
        string path = Application.persistentDataPath + "LevelSelectInfo.magess";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            LevelSelectData data = formatter.Deserialize(stream) as LevelSelectData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save file no found in " + path);
            return null;
        }

    }
}
