using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;
using System.Linq;

public class SaveManager
{
    private string _saveDirectory;

    public SaveManager()
    {
        _saveDirectory = Path.Combine(Application.dataPath, "Saves");

        if (!Directory.Exists(_saveDirectory))
        {
            Directory.CreateDirectory(_saveDirectory);
        }
    }

    public void SaveGame(GameModel gameModel, string saveName)
    {
        SaveData saveData = new SaveData
        {
            saveName = saveName,
            saveDate = DateTime.UtcNow.ToString("o"),
            gameModel = gameModel
        };

        JsonSerializerSettings settings = new JsonSerializerSettings
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            TypeNameHandling = TypeNameHandling.Auto,
        };

        string json = JsonConvert.SerializeObject(saveData, Formatting.Indented, settings);

        string safeDate = saveData.saveDate.Replace(":", "-").Replace("/", "-");
        string filePath = Path.Combine(_saveDirectory, saveName + "_" + safeDate + ".json");

        File.WriteAllText(filePath, json);
    }

    public GameModel LoadGame(string saveName)
    {
        string filePath = Path.Combine(_saveDirectory, saveName + ".json");

        if (!File.Exists(filePath))
        {
            Debug.LogError("A megadott mentés nem található: " + filePath);
            return null;
        }

        JsonSerializerSettings settings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.Auto
        };

        string json = File.ReadAllText(filePath);
        SaveData saveData = JsonConvert.DeserializeObject<SaveData>(json, settings);

        return saveData.gameModel;
    }

    public List<SaveData> GetSaveList()
    {
        List<SaveData> saves = new List<SaveData>();

        foreach (string file in Directory.GetFiles(_saveDirectory, "*.json"))
        {
            try
            {
                string json = File.ReadAllText(file);

                if (string.IsNullOrWhiteSpace(json))
                {
                    Debug.LogWarning($"Üres vagy sérült mentés kihagyva: {file}");
                    continue;
                }

                JsonSerializerSettings settings = new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Auto
                };

                SaveData saveData = JsonConvert.DeserializeObject<SaveData>(json, settings);

                if (saveData == null)
                {
                    Debug.LogError($"HIBA: `saveData` null értékû lett! ({file})");
                    continue;
                }

                if (saveData.gameModel == null)
                {
                    Debug.LogError($"HIBA: `saveData.gameModel` null lett! ({file})");
                    continue;
                }

                saves.Add(saveData);
            }
            catch (Exception ex)
            {
                Debug.LogError($"Hiba történt a mentés betöltésekor ({file}): {ex.Message}\n{ex.StackTrace}");
            }
        }

        saves = saves.OrderByDescending(s => DateTime.Parse(s.saveDate)).ToList();

        return saves;
    }
}

[System.Serializable]
public class SaveData
{
    public string saveName;
    public string saveDate;
    public GameModel gameModel;

    public DateTime GetSaveDate()
    {
        return DateTime.Parse(saveDate);
    }
}
