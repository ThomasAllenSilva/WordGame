using System.IO;

using UnityEngine;


public class DataManager : MonoBehaviour
{
    public SaveData SaveDataManager { get; private set; }

    public LoadData LoadDataManager { get; private set; }

    public GameDataManager GameDataManager { get; private set; }

    public PlayerDataManager PlayerDataManager { get; private set; }

    public FirebaseDataManager FirebaseDataManager { get; private set; }

    public static DataManager Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }

        else
        {
            Destroy(this.gameObject);
        }

        SaveDataManager = GetComponentInChildren<SaveData>();
        LoadDataManager = GetComponentInChildren<LoadData>();
        GameDataManager = GetComponentInChildren<GameDataManager>();
        PlayerDataManager = GetComponentInChildren<PlayerDataManager>();
        FirebaseDataManager = GetComponentInChildren<FirebaseDataManager>();

        DontDestroyOnLoad(Instance.gameObject);
    }

    public bool CheckIfFileExists(string fileName)
    {
        string fullPath = Path.Combine(Application.persistentDataPath, fileName);

        return File.Exists(fullPath);
    }

    public string GetCurrentGameLanguageIdentifierCode()
    {
        return GameDataManager.CurrentGameLanguageCode;
    }
}
