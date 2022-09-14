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

    private bool CheckIfGameDataExists()
    {
        return dataManager.CheckIfFileExists(gameDataFileName.ToString());
    }

    private void CreateNewGameData()
    {
        dataManager.SaveDataManager.SaveNewData(gameDataFileName, GameData);
    }

    private void LoadGameData()
    {
        string dataToLoad = dataManager.LoadDataManager.LoadFileData(gameDataFileName.ToString());

        JsonUtility.FromJsonOverwrite(dataToLoad, GameData);
    }

    public void OverwriteGameDataLanguageInfo(string languageCode, int languageIndex)
    {
        GameData.currentGameLanguageCode = languageCode;
        GameData.currentGameLanguageIndex = languageIndex;

        dataManager.SaveDataManager.SaveNewData(gameDataFileName, GameData);
    }

    public void OverWriteGameDataAudioInfo(bool playerHasMutedGameMusic, bool playerHasMutedGameSounds)
    {
        GameData.isGameMusicMuted = playerHasMutedGameMusic;
        GameData.isGameAudioMuted = playerHasMutedGameSounds;

        dataManager.SaveDataManager.SaveNewData(gameDataFileName, GameData);
    }

    public void ResetGameData()
    {
        bool hasBuyedNoAds = GameData.hasBuyedNoAds;

        GameData = new GameData();

        GameData.hasBuyedNoAds = hasBuyedNoAds;

        CreateNewGameData();
    }
}


public class GameData 
{
    public string currentGameLanguageCode;
    public int currentGameLanguageIndex;

    public bool isGameMusicMuted;
    public bool isGameAudioMuted;

    public bool hasBuyedNoAds;


    public GameData()
    {
        currentGameLanguageCode = LocalizationSettings.SelectedLocale.Identifier.Code;
        currentGameLanguageIndex = LocalizationSettings.AvailableLocales.Locales.IndexOf(LocalizationSettings.SelectedLocale);
    }
}

