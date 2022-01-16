using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;

    public TMP_InputField playerInput;

    public string playerName;
    public string highScoreHolder;
    public int highScore;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadInfo();
    }

    public void NameUpdate()
    {
        playerName = playerInput.text;
    }

    [System.Serializable]
    class SaveData
    {
        public string highScoreHolder;
        public int highScore;
    }

    public void SaveInfo()
    {
        SaveData data = new SaveData();
        data.highScoreHolder = highScoreHolder;
        data.highScore = highScore;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadInfo()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            highScoreHolder = data.highScoreHolder;
            highScore = data.highScore;
        }
    }
}
