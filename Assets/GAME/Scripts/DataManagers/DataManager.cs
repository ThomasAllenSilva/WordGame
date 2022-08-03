using System.IO;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class DataManager : MonoBehaviour
{
    public SaveData SaveDataManager { get; private set; }

    public LoadData LoadDataManager { get; private set; }

    public GameDataManager GameDataManager { get; private set; }

    public static DataManager Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }

        else
        {
            Destroy(gameObject);
        }

        SaveDataManager = GetComponentInChildren<SaveData>();
        LoadDataManager = GetComponentInChildren<LoadData>();
        GameDataManager = GetComponentInChildren<GameDataManager>();

        DontDestroyOnLoad(Instance.gameObject);
    }

    public bool CheckIfFileExists(string fileName)
    {
        string fullPath = Path.Combine(Application.persistentDataPath, fileName.ToString());

        return File.Exists(fullPath);
    }


    public string GetCurrentGameLanguageIdentifierCode()
    {
        return LocalizationSettings.SelectedLocale.Identifier.Code;
    }
}
