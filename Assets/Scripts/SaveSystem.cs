using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public static class SaveSystem
{
    public static readonly string SAVE_DIR = Application.dataPath + "/Saves/";
    private const string SAVE_EXTENSION = "json";
    public static void Init()
    {
        if(!Directory.Exists(SAVE_DIR))
        {
            Directory.CreateDirectory(SAVE_DIR);
        }
    }

    public static void Save(string saveData)
    {
        int saveNumber = 1;
        while (File.Exists(SAVE_DIR + "save_" + saveNumber + "." + SAVE_EXTENSION)) {
            saveNumber++;
        }
        // saveNumber is unique
        File.WriteAllText(SAVE_DIR + "save_" + saveNumber + "." + SAVE_EXTENSION, saveData);
    }
    public static string Load()
    {
        DirectoryInfo directoryInfo = new DirectoryInfo(SAVE_DIR);
        // Get all save files
        FileInfo[] saveFiles = directoryInfo.GetFiles("*." + SAVE_EXTENSION);
        // Cycle through all save files and identify the most recent one
        FileInfo mostRecentFile = null;
        foreach (FileInfo fileInfo in saveFiles) {
            if (mostRecentFile == null) {
                mostRecentFile = fileInfo;
            } else {
                if (fileInfo.LastWriteTime > mostRecentFile.LastWriteTime) {
                    mostRecentFile = fileInfo;
                }
            }
        }

        // If theres a save file, load it, if not return null
        if (mostRecentFile != null) {
            string saveString = File.ReadAllText(mostRecentFile.FullName);
            return saveString;
        } else {
            return null;
        }   
    }
}
