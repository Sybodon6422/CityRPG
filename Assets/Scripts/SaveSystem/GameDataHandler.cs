using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;
using UnityEngine;

public class GameDataHandler
{
    private static string saveLoc = "/UnityProjectSaves/TimmyTown/";
    string fileLocation = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

    public void SaveGame(PlayerStats stats, String _saveName)
    {
        if (!File.Exists(fileLocation + saveLoc))
        {
            Directory.CreateDirectory(fileLocation + saveLoc);
        }
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(fileLocation + saveLoc + _saveName + ".dat");
        SavedStats data = new SavedStats(stats, _saveName);

        bf.Serialize(file, data);
        file.Close();
        Debug.Log("Game data saved!");
    }

    public void SaveGame(SavedStats saveData)
    {
        if (!File.Exists(fileLocation + saveLoc))
        {
            Directory.CreateDirectory(fileLocation + saveLoc);
        }
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(fileLocation + saveLoc + saveData.saveName + ".dat");

        bf.Serialize(file, saveData);
        file.Close();
        Debug.Log("Game data saved!");
    }

    public SavedStats LoadGame(String _saveName)
    {
        if (File.Exists(fileLocation + saveLoc + _saveName + ".dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file =
                       File.Open(fileLocation + saveLoc + _saveName + ".dat", FileMode.Open);
            SavedStats data = (SavedStats)bf.Deserialize(file);
            file.Close();

            return data;
        }

        Debug.LogError("There is no save data!");
        return null;
    }
    public SavedStats LoadLastGame()
    {
        var saveFiles = Directory.GetFiles(fileLocation + saveLoc);

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file =
                   File.Open(saveFiles[0], FileMode.Open);
        SavedStats data = (SavedStats)bf.Deserialize(file);
        file.Close();

        return data;
    }
}
