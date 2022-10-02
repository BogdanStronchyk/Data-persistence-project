using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class DataHandler : MonoBehaviour
{

    public static DataHandler Instance;

    public string Name = "Player";
    public int BestScore = 0;
    
    public List<SaveData> ScoreList = new List<SaveData>(10);

    string path;

    private void Awake()
    {
        path = Application.persistentDataPath + "/savefiletable.json";
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

        // adding player's data into a list if it's not overfilled
        if (ScoreList.Count < 10)
        {
            ScoreList.Add(data);
        }
        // but if it is:
        else if (ScoreList.Count == 10)
        {
            // we look for a smallest score
            int index = 0;
            int[] scores = new int[10];
            foreach (SaveData dataset in ScoreList)
            {
                scores[index] = dataset.HighScore;
                index += 1;
            }
            int min_index = Array.IndexOf(scores, scores.Min());

            // removing it and adding a new dataset to the list
            ScoreList.RemoveAt(min_index);
            ScoreList.Add(data);
        }

        // sorting the list
        List<SaveData> sorted = ScoreList.OrderByDescending(x => x.HighScore).ToList();

        // and then saving it
        SaveScoreToFile(sorted);
    }

    public void SaveScoreToFile(List<SaveData> data)
    {
        string json = "";
        foreach (SaveData dataset in data)
        {
            json += JsonUtility.ToJson(dataset);
            json += '\n';
        }
        Debug.Log(json);
        File.WriteAllText(path, json);
    }

    public void ReadFromSave()
    {
        if (File.Exists(path))
        {
            string[] json = File.ReadAllLines(path);
            
            ScoreList.Clear();
            
            foreach(string line in json)
            {
                SaveData data = JsonUtility.FromJson<SaveData>(line);
                
                if (line == json[0])
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
