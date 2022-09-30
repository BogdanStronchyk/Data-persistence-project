using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using UnityEngine.UI;

public class DataHandler : MonoBehaviour
{

    public static DataHandler Instance;

    public string Name;
    public int BestScore;


    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

    }

    [System.Serializable]
    class SaveData
    {
        public int HighScore;
        public string PlayerName;
    }

    public void SaveScore()
    {
        SaveData data = new SaveData();
        data.HighScore = BestScore;
        data.PlayerName = Name;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);

        Debug.Log("Saved.");
    }

    public void LoadScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            Name = data.PlayerName;
            BestScore = data.HighScore;
        }
    }

}
