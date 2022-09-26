using System.Text;
using System.Collections;

using UnityEngine;

public class PlayerDataManager : MonoBehaviour
{
    private StringBuilder playerDataFileName = new StringBuilder("PlayerData");

    private DataManager dataManager;

    private PlayerLocalData PlayerData;

    #region PlayerData Properties

    public int CurrentGameLevel { get { return PlayerData.currentGameLevel; } private set { PlayerData.currentGameLevel = value; } }

    public int CurrentSelectedBackground { get { return PlayerData.selectedBackground; } private set { PlayerData.selectedBackground = value; } }

    public int CurrentPlayerCoins { get { return PlayerData.playerCoins; } private set { PlayerData.playerCoins = value; } }

    public bool[] PurchasedBackgrounds { get { return PlayerData.purchasedBackgrounds; } private set { PlayerData.purchasedBackgrounds = value; } }

    public int MaxGameLevelPlayed { get { return PlayerData.maxGameLevelPlayed; } private set { PlayerData.maxGameLevelPlayed = value; } }

    public bool HasResetGameData { get; private set; }
    #endregion

    private void Start()
    {
        dataManager = DataManager.Instance;

        InitializePlayerData();

        ScenesManager.Instance.onFirebaseSceneLoaded += InitializePlayerData;
        ScenesManager.Instance.onAnySceneLoaded += CheckIfCanSubscribeToEvent;
    }

    private void InitializePlayerData()
    {
        PlayerData = new PlayerLocalData();

        if (CheckIfPlayerDataExists())
        {
            LoadPlayerData();
        }

        else
        {
            CreateNewPlayerData();
            LoadPlayerData();
        }
    }

    private bool CheckIfPlayerDataExists()
    {
        playerDataFileName.Append(dataManager.GetCurrentGameLanguageIdentifierCode());
        bool fileExist = dataManager.CheckIfFileExists(playerDataFileName.ToString());
        playerDataFileName.Clear();
        playerDataFileName.Append("PlayerData");
        return fileExist;
    }

    private void LoadPlayerData()
    {

        playerDataFileName.Append(dataManager.GetCurrentGameLanguageIdentifierCode());
      
        string dataToLoad = dataManager.LoadDataManager.LoadFileData(playerDataFileName.ToString());

        playerDataFileName.Clear();
        playerDataFileName.Append("PlayerData");


        JsonUtility.FromJsonOverwrite(dataToLoad, PlayerData);
    }

    private void CreateNewPlayerData()
    {
        dataManager.SaveDataManager.SaveNewData(playerDataFileName, PlayerData);
    }

    public void IncreaseGameLevel()
    {
        MaxGameLevelPlayed = CurrentGameLevel == MaxGameLevelPlayed ? ++MaxGameLevelPlayed : MaxGameLevelPlayed;

        CurrentGameLevel++;

        SavePlayerData();
    }

    private void SavePlayerData()
    {
        dataManager.SaveDataManager.SaveNewData(playerDataFileName, PlayerData);
    }


    public void ResetPlayerData()
    {
        PlayerData = new PlayerLocalData();
        SavePlayerData();
    }

    public void SavePurchasedBackground(int backgroundIndex)
    {
        PurchasedBackgrounds[backgroundIndex] = true;
        SavePlayerData();
    }

    public void SaveSelectedBackground(int selectedBackgroundIndex)
    {
        CurrentSelectedBackground = selectedBackgroundIndex;
        SavePlayerData();
    }

    public void SpendPlayerCoins(int amountToSpend)
    {
        CurrentPlayerCoins -= amountToSpend;

        SavePlayerData();
    }

    public void IncreasePlayerCoins(int amountToIncrease)
    {
        CurrentPlayerCoins += amountToIncrease;
        SavePlayerData();
    }

    public void ChangeCurrentPlayerLevel(int newCurrentLevel)
    {
        CurrentGameLevel = newCurrentLevel;
        SavePlayerData();
    }

    private void CheckIfCanSubscribeToEvent()
    {
        if(ScenesManager.Instance.LoadedSceneIndex == 2 || ScenesManager.Instance.LoadedSceneIndex == 3)
        {
            SubscribeToLevelCompletedEvent();
        }
    }
    private void SubscribeToLevelCompletedEvent()
    {
        GameManager.Instance.LevelManager.onLevelCompleted -= IncreaseGameLevel;
        GameManager.Instance.LevelManager.onLevelCompleted += IncreaseGameLevel;
    }

    private void OnDestroy()
    {
        ScenesManager.Instance.onAnySceneLoaded -= CheckIfCanSubscribeToEvent;
    }

    private class PlayerLocalData
    {
        public int currentGameLevel;
        public int maxGameLevelPlayed;
        public int playerCoins;
        public bool[] purchasedBackgrounds = new bool[100];
        public int selectedBackground = 0;

        public PlayerLocalData()
        {
            purchasedBackgrounds[0] = true;
            currentGameLevel = 1;
            maxGameLevelPlayed = 1;
        }
    }
}