using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveSystem
{
    private static string _path1 = Application.persistentDataPath + "/slot1.txt";
    private static string _path2 = Application.persistentDataPath + "/slot2.txt";
    private static string _path3 = Application.persistentDataPath + "/slot3.txt";
    
    public static void SaveGame(Game game)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = game.path;
        FileStream stream = new FileStream(path, FileMode.Create);
        
        GameData data = new GameData(game);
        
        formatter.Serialize(stream, data);
        stream.Close();
        Debug.Log("Game saved");
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
}
