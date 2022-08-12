using System.Text;

using UnityEngine;

public class PlayerDataManager : MonoBehaviour
{
    private StringBuilder playerDataFileName = new StringBuilder("PlayerData");

    private DataManager dataManager;

    private PlayerData playerData = new PlayerData();

    public int Coins { get; private set; }

    public int CurrentGameLevel { get; private set; }


    private void Start()
    {
        dataManager = DataManager.Instance;

        InitializePlayerData();

        ScenesManager.Instance.onFirebaseSceneLoaded += InitializePlayerData;
    }

    private void InitializePlayerData()
    {
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


        JsonUtility.FromJsonOverwrite(dataToLoad, playerData);

        this.Coins = playerData.coins;
        this.CurrentGameLevel = playerData.currentGameLevel;
    }

    private void CreateNewPlayerData()
    {
        dataManager.SaveDataManager.SaveNewData(playerDataFileName, playerData);
    }

    public void IncreaseGameLevel()
    {
        playerData.currentGameLevel++;
        SavePlayerData();
    }

    private void SavePlayerData()
    {
        dataManager.SaveDataManager.SaveNewData(playerDataFileName, playerData);
    }
}


public class PlayerData
{
    public int coins;
    public int currentGameLevel;

    public PlayerData()
    {
        coins = 0;
        currentGameLevel = 1;
    }
}
