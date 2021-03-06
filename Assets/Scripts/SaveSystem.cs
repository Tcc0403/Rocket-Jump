using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SaveData(Player player)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string savePath = Application.persistentDataPath + "/savedata";
        FileStream stream = new FileStream(savePath, FileMode.Create);

        PlayerData playerData = new PlayerData(player);
        
        formatter.Serialize(stream, playerData);
        stream.Close();        
    }

    public static PlayerData LoadData()
    {
        string savePath = Application.persistentDataPath + "/savedata";
        if(File.Exists(savePath))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(savePath, FileMode.Open);

            PlayerData playerData = formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            return playerData;
        }
        else
        {
            Debug.LogError("Save file does not exist");
            return null;
        }
    }
}
