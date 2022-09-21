using UnityEngine;
using UnityEngine.Localization.Settings;

public class GameDataManager : MonoBehaviour
{
    private const string gameDataFileName = "GameData";

    private GameLocalData GameData;

    private DataManager dataManager;

    #region GameData Properties

    public string CurrentGameLanguageCode { get { return GameData.currentGameLanguageCode; } private set { GameData.currentGameLanguageCode = value; } }

    public int CurrentGameLanguageLocalizationIndex { get { return GameData.currentGameLanguageIndex; } private set { GameData.currentGameLanguageIndex = value; } }

    public bool IsGameMusicMuted { get { return GameData.isGameMusicMuted; } private set { GameData.isGameMusicMuted = value; } }

    public bool IsGameSoundsMuted { get { return GameData.isGameSoundsMuted; } private set { GameData.isGameSoundsMuted = value; } }

    public bool HasBuyedNoAds { get { return GameData.hasBuyedNoAds; } private set { GameData.hasBuyedNoAds = value; } }

    #endregion

    private void Awake() => GameData = new GameLocalData();

    private void Start()
    {
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
        CurrentGameLanguageCode = languageCode;
        CurrentGameLanguageLocalizationIndex = languageIndex;

        dataManager.SaveDataManager.SaveNewData(gameDataFileName, GameData);
    }

    public void OverWriteGameDataAudioInfo(bool playerHasMutedGameMusic, bool playerHasMutedGameSounds)
    {
        IsGameMusicMuted = playerHasMutedGameMusic;
        IsGameSoundsMuted = playerHasMutedGameSounds;

        dataManager.SaveDataManager.SaveNewData(gameDataFileName, GameData);
    }

    public void SetAdsAsBuyed()
    {
        HasBuyedNoAds = true;

        dataManager.SaveDataManager.SaveNewData(gameDataFileName, GameData);

        Debug.Log("save game data");
        Debug.Log(HasBuyedNoAds);
    }
    public void ResetGameData()
    {
        bool hasBuyedNoAds = HasBuyedNoAds;

        GameData = new GameLocalData();

        HasBuyedNoAds = hasBuyedNoAds;

        CreateNewGameData();
    }

    private class GameLocalData
    {
        public string currentGameLanguageCode;
        public int currentGameLanguageIndex;

        public bool isGameMusicMuted;
        public bool isGameSoundsMuted;

        public bool hasBuyedNoAds;


        public GameLocalData()
        {
            currentGameLanguageCode = LocalizationSettings.SelectedLocale.Identifier.Code;
            currentGameLanguageIndex = LocalizationSettings.AvailableLocales.Locales.IndexOf(LocalizationSettings.SelectedLocale);
        }
    }
}


