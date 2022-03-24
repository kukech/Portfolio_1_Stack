using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance { get; private set; }
    public static int Score { get; set; }
    public static int Crystall { get; private set; }
    private void Awake()
    {
        // start of new code
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        // end of new code

        Instance = this;
        LoadCrystall();
        DontDestroyOnLoad(gameObject);
    }
    [System.Serializable]
    class SaveData
    {
        public int crystall;
    }
    public void SaveCrystall()
    {
        SaveData data = new SaveData();
        data.crystall = Crystall;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }
    public void LoadCrystall()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            Crystall = data.crystall;
        }
        else { Crystall = 0; }
    }
    public void AddCrystall()
    {
        Crystall += Score / 10;
    }
}
