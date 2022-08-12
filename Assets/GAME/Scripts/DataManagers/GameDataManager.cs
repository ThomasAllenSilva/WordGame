using System.Text;

using UnityEngine;


public class GameDataManager : MonoBehaviour
{
    private const string gameDataFileName = "GameData";
    public string CurrentGameLanguageCode { get; private set; }
    public int CurrentGameLanguageIndex { get; private set; }

    private GameData gameData;

    private DataManager dataManager;

    private void Start()
    {
        gameData = new GameData();
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
        return dataManager.CheckIfFileExists(gameDataFileName);
    }

    private void LoadGameData()
    {
        string dataToLoad = dataManager.LoadDataManager.LoadFileData(gameDataFileName);

        GameData gameData = new GameData();

        JsonUtility.FromJsonOverwrite(dataToLoad, gameData);

        this.CurrentGameLanguageCode = gameData.currentGameLanguageCode;
        this.CurrentGameLanguageIndex = gameData.CurrentGameLanguageIndex;
    }


    private void CreateNewGameData()
    {
        dataManager.SaveDataManager.SaveNewData(gameDataFileName, gameData);
    }

    public void UpdateGameData(int newLanguageIndex, string newLanguageCode)
    {
        CurrentGameLanguageCode = newLanguageCode;
        CurrentGameLanguageIndex = newLanguageIndex;

        gameData.currentGameLanguageCode = this.CurrentGameLanguageCode;
        gameData.CurrentGameLanguageIndex = this.CurrentGameLanguageIndex;

        dataManager.SaveDataManager.SaveNewData(gameDataFileName, gameData);
    }
}


public class GameData 
{
    public string currentGameLanguageCode;
    public int CurrentGameLanguageIndex;

    public GameData()
    {
        currentGameLanguageCode = FirebaseMenuManager.Instance.GetCurrentLocalSystemLanguageCode();
    }
}

