using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class AppManager : MonoBehaviour
{
    // Persistence across sessions

    // Persistance across scenes
    public static AppManager Instance;
    public string playerName;
    public string highScoreName;
    public int highScore;

    // Set up object persistance across scenes
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadData();
    }

    [System.Serializable]
    public class PlayerData
    {
        public string highScoreName;
        public int highScore;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SaveData()
    {
        PlayerData data = new PlayerData();
        data.highScoreName = highScoreName;
        data.highScore = highScore;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadData()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            PlayerData data = JsonUtility.FromJson<PlayerData>(json);

            highScoreName = data.highScoreName;
            highScore = data.highScore;
        }
    }
}
