using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
public class PlayerInfoSaveScript : MonoBehaviour
{
    //Save Player function called when you want to Save Player file
    public static void SavePI(PlayerInfoScript player)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "PlayerInfo.magess";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerInfoData data = new PlayerInfoData(player);

        formatter.Serialize(stream, data);
        stream.Close();
    }
    //Load player function called when you want to load player file
    public static PlayerInfoData LoadPI()
    {
        string path = Application.persistentDataPath + "PlayerInfo.magess";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerInfoData data = formatter.Deserialize(stream) as PlayerInfoData;
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
