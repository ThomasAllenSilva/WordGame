using UnityEngine;
using UnityEngine.Localization.Settings;

public class GameDataManager : MonoBehaviour
{
    private const string gameDataFileName = "GameData";

    public GameData GameData {get; private set;}

    private DataManager dataManager;

    private void Start()
    {
        GameData = new GameData();
        dataManager = DataManager.Instance;

        InitializeGameData();
    }

    private void InitializeGameData()
    {
        if (CheckIfGameDataExists())
        {
            LoadGameData();
        }

        else
        {
            CreateNewGameData();
            LoadGameData();
        }
    }

    public bool CheckIfGameDataExists()
    {
        return dataManager.CheckIfFileExists(gameDataFileName);
    }

    private void LoadGameData()
    {
        string dataToLoad = dataManager.LoadDataManager.LoadFileData(gameDataFileName);

        JsonUtility.FromJsonOverwrite(dataToLoad, GameData);
    }


    private void CreateNewGameData()
    {
        dataManager.SaveDataManager.SaveNewData(gameDataFileName, GameData);
    }

    public void UpdateGameData(int newLanguageIndex, string newLanguageCode)
    {
        GameData.currentGameLanguageCode = newLanguageCode;
        GameData.currentGameLanguageIndex = newLanguageIndex;

        dataManager.SaveDataManager.SaveNewData(gameDataFileName, GameData);
    }
}


public class GameData 
{
    public string currentGameLanguageCode;
    public int currentGameLanguageIndex;

    public GameData()
    {
        currentGameLanguageCode = LocalizationSettings.SelectedLocale.Identifier.Code;
        currentGameLanguageIndex = LocalizationSettings.AvailableLocales.Locales.IndexOf(LocalizationSettings.SelectedLocale);
    }
}

