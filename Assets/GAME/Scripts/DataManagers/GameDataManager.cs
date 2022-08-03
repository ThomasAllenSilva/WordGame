using System.Text;
using UnityEngine;

[RequireComponent(typeof(GameDataValues))]
public class GameDataManager : MonoBehaviour
{
    private StringBuilder gameDataFileName = new StringBuilder("GameData");

    public GameDataValues GameDataValues { get; private set; }

    private DataManager dataManager;

    private void Awake()
    {
        dataManager = DataManager.Instance;

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
        return dataManager.CheckIfFileExists(gameDataFileName.ToString());
    }

    private void LoadGameData()
    {
        string dataToLoad = dataManager.LoadDataManager.LoadFileData(gameDataFileName.ToString());

        GameData gameData = new GameData();

        JsonUtility.FromJsonOverwrite(dataToLoad, gameData);

        GameDataValues.LoadGameData(gameData);
    }

    private void CreateNewGameData()
    {
        GameData gameData = new GameData();

        dataManager.SaveDataManager.SaveNewData(gameDataFileName, gameData);
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
        currentGameLanguage = DataManager.Instance.GetCurrentGameLanguageIdentifierCode();
    }
}
