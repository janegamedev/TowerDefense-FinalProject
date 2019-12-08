using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveSystem
{ 
    public static void SaveGame(Game game)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = game.Path;
        /*Debug.Log(path);*/
        FileStream stream;
        
        if (File.Exists(path))
        {
            stream = File.Open(path, FileMode.Open);
        }
        else
        {
            stream = new FileStream(path, FileMode.Create);
        }
        
        GameData data = new GameData(game);
        
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static GameData LoadGame(string path)
    {
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
            Debug.LogWarning("Save file not found");
            return null;
        }
    }

    public static void DeleteSave(string path)
    {
        File.Delete(path);
    }
}
