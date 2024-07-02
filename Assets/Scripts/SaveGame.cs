using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveGame
{

    public static bool load = false; 
   public static void SaveSystem(PlayerUI player)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = "C:\\Users\\Sarah\\Documents\\player.txt";
        //string path = Application.persistentDataPath + "/player.txt";
        FileStream stream = new FileStream(path, FileMode.Create);
        GameData data = new GameData(player);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static GameData LoadSystem()
    {
        string path = "C:\\Users\\Sarah\\Documents\\player.txt";
        //string path = Application.persistentDataPath + "/player.txt";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            GameData data = formatter.Deserialize(stream) as GameData;
            stream.Close();
            return data;
        }

        else
        {
            Debug.LogWarning("Wrong Path!!");
            return null;
        }

    }
}
