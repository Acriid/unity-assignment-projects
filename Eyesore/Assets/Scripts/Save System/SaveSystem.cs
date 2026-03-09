//Title: EVERYTHING you need to know to SAVE and LOAD your game in Unity
//Author: Sasquatch B Studios
//Date: 04/03/2026
//https://www.youtube.com/watch?v=1mf730eb5Wo

using UnityEngine;
using System.IO;

public class SaveSystem 
{
    private static SaveData _saveData = new();

    [System.Serializable]
    public struct SaveData
    {
        public DialogSaveData DialogData;
    }

    public static string SaveFileName()
    {
        string saveFile = Application.persistentDataPath + "/save" + ".save";
        return saveFile;
    }

    public static void Save()
    {
        HandleSaveData();

        File.WriteAllText(SaveFileName(),JsonUtility.ToJson(_saveData,true));
    }

    private static void HandleSaveData()
    {
        GameOverSystem.Instance.SaveData(ref _saveData.DialogData);
    } 

    public static void Load()
    {
        string saveContent = File.ReadAllText(SaveFileName());

        _saveData = JsonUtility.FromJson<SaveData>(saveContent);
        HandleLoadData();
    }
    private static void HandleLoadData()
    {
        DialogManager.Instance.LoadData(_saveData.DialogData);
    }
}
