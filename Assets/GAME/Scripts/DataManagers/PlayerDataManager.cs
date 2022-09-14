using System.Text;
using System.Collections;
using UnityEngine;

public class PlayerDataManager : MonoBehaviour
{
    private StringBuilder playerDataFileName = new StringBuilder("PlayerData");

    private DataManager dataManager;

    public PlayerData PlayerData { get; private set; }

    private void Start()
    {
        dataManager = DataManager.Instance;


        InitializePlayerData();

        ScenesManager.Instance.onFirebaseSceneLoaded += InitializePlayerData;
    }

    private void InitializePlayerData()
    {
        PlayerData = new PlayerData();

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
        PlayerData.maxGameLevelPlayed = PlayerData.currentGameLevel == PlayerData.maxGameLevelPlayed ? ++PlayerData.maxGameLevelPlayed : PlayerData.maxGameLevelPlayed;

        PlayerData.currentGameLevel++;

        SavePlayerData();
    }

    private void SavePlayerData()
    {
        dataManager.SaveDataManager.SaveNewData(playerDataFileName, PlayerData);
    }

    private IEnumerator SubscribeToLevelCompletedEvent()
    {
        yield return new WaitForSecondsRealtime(1);
        GameManager.Instance.LevelManager.onLevelCompleted -= IncreaseGameLevel;
        GameManager.Instance.LevelManager.onLevelCompleted += IncreaseGameLevel;
    }

    public void OverWriteCurrentPlayerData(PlayerData newPlayerData)
    {
        PlayerData = newPlayerData;
        SavePlayerData();
    }

    public void ResetPlayerData()
    {
        PlayerData = new PlayerData();
        SavePlayerData();
    }


    public void SavePurchasedBackground(int backgroundIndex)
    {
        PlayerData.purchasedBackgrounds[backgroundIndex] = true;
        SavePlayerData();
    }

    public void SaveSelectedBackground(int selectedBackgroundIndex)
    {
        PlayerData.selectedBackground = selectedBackgroundIndex;
        SavePlayerData();
    }

    public void SpendPlayerCoins(int amountToSpend)
    {
        Debug.Log(PlayerData.playerCoins);
        PlayerData.playerCoins -= amountToSpend;
        Debug.Log(PlayerData.playerCoins);
        SavePlayerData();
    }

    public void IncreasePlayerCoins(int amountToIncrease)
    {
        PlayerData.playerCoins += amountToIncrease;
        SavePlayerData();
    }

    public void ChangeCurrentPlayerLevel(int levelToPlay)
    {
        PlayerData.currentGameLevel = levelToPlay;
        SavePlayerData();
    }

    private void OnLevelWasLoaded(int level)
    {
        if(level == 2 || level == 3)
        {
           StartCoroutine(SubscribeToLevelCompletedEvent());
        }
    }
}


public class PlayerData
{
    public int currentGameLevel;
    public int maxGameLevelPlayed;
    public int playerCoins;
    public bool[] purchasedBackgrounds = new bool[100];
    public int selectedBackground = 0;

    public PlayerData()
    {
        purchasedBackgrounds[0] = true;
        currentGameLevel = 1;
        maxGameLevelPlayed = 1;
    }
}
