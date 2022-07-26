using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

[RequireComponent(typeof(GameDataValues))]
public class GameDataManager : MonoBehaviour
{

    private StringBuilder fileName = new StringBuilder("GameData");

    private DataManager dataManager;

    public GameDataValues GameDataValues { get; private set; }

    private void Awake()
    {
        dataManager = DataManager.Instance;
        fileName.Append(dataManager.GetCurrentGameLanguageCode());
        GameDataValues = GetComponent<GameDataValues>();
    }

    private void Start()
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
        return dataManager.SaveDataManager.CheckIfFileExists(fileName.ToString());
    }

    private void LoadGameData()
    {
        string dataToLoad = dataManager.LoadDataManager.LoadFileData(fileName.ToString());

        GameData gameData = new GameData();
        JsonUtility.FromJsonOverwrite(dataToLoad, gameData);

        GameDataValues.LoadGameData(gameData);

    }

    private void CreateNewGameData()
    {
        GameData gameData = new GameData();
        dataManager.SaveDataManager.SaveNewData(fileName.ToString(), gameData);
    }
}

public class GameData
{
    public int coins;
    public int currentGameLevel;
    public string currentGameLanguage;

    public GameData()
    {
        coins = 0;
        currentGameLevel = 1;
        currentGameLanguage = DataManager.Instance.GetCurrentGameLanguageCode();
    }
}
