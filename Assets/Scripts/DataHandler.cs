using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataHandler : MonoBehaviour
{

    public static DataHandler Instance;

    public string Name = "Player";
    public int BestScore = 0;
    
    public List<SaveData> ScoreList;


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
    public class SaveData
    {
        public int HighScore;
        public string PlayerName;
    }

    public void SaveScoreTable()
    {
        SaveData data = new SaveData(); // creating new player data instance
        
        // saving player's data
        data.HighScore = BestScore;
        data.PlayerName = Name;
        
        
        ScoreList.Add(data); // adding player's data into a list
        List<SaveData> sorted = ScoreList.OrderBy(x => x.HighScore).ToList(); // sorting the list
        
        string path = Application.persistentDataPath + "/savefiletable.json";
        File.Delete(path);
        foreach (SaveData dataset in sorted)
        {
            SaveScoreToFile(dataset);
        }
        // serializing data and saving them into file
        
    }

    public void SaveScoreToFile(SaveData data)
    {
        string path = Application.persistentDataPath + "/savefiletable.json";
        
        // serializing data and saving them into file
        string json = JsonUtility.ToJson(data);
        File.AppendAllText(Application.persistentDataPath + "/savefiletable.json", json);
    }

    public void ReadFromSave()
    {
        string path = Application.persistentDataPath + "/savefiletable.json";
        if (File.Exists(path))
        {
            string[] json = File.ReadAllLines(path);
            ScoreList.Clear();
            foreach(string line in json)
            {
                SaveData data = JsonUtility.FromJson<SaveData>(line);
                if (line == json.First())
                {
                    Name = data.PlayerName;
                    BestScore = data.HighScore;
                }
                ScoreList.Add(data);

            }
                
        }
    }

    public void ResetScore()
    {
        //ReadFromSave();
        string path = Application.persistentDataPath + "/savefiletable.json";
        File.Delete(path);
        ScoreList.Clear();
        Name = "Player";
        BestScore = 0;
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
