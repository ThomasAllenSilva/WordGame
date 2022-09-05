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

    public PlayerData(int currentLevel)
    {
        currentGameLevel = currentLevel;
    }

    public PlayerData()
    {
        currentGameLevel = 1;
    }
}
