using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using UnityEditor;

public class DownloadNewContent : MonoBehaviour
{
    [SerializeField] private string contentURL;
    int currentLineIndexFromLevelInfo = 0;
    private IEnumerator Start()
    {
        UnityWebRequest www = UnityWebRequestAssetBundle.GetAssetBundle(contentURL);

        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }

        else
        {
            AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(www);

            TextAsset levelAsset = bundle.LoadAsset("Level-1.txt") as TextAsset;

            CreateNewLevel(levelAsset);
        }
    }

    private void CreateNewLevel(TextAsset levelAsset)
    {
        HuntWordsSO newLevel = ScriptableObject.CreateInstance<HuntWordsSO>();

        string[] levelInfo = levelAsset.text.Split("\n");



        newLevel.numberOfColumns = int.Parse(levelInfo[currentLineIndexFromLevelInfo]);
        currentLineIndexFromLevelInfo++;

        newLevel.numberOfLines = int.Parse(levelInfo[currentLineIndexFromLevelInfo]);
        currentLineIndexFromLevelInfo++;

        newLevel.CreateNewColum();

        string[] wordsFromTheGrid = new string[newLevel.numberOfColumns * newLevel.numberOfLines];

        for (int i = currentLineIndexFromLevelInfo; i < wordsFromTheGrid.Length + 2; i++)
        {
            wordsFromTheGrid[i - 2] = levelInfo[i].ToString().Replace("\r", "");
            currentLineIndexFromLevelInfo++;
        }

        int currentLine = 0;

        for (int i = 0; i < newLevel.numberOfColumns; i++)
        {
            for (int j = 0; j < newLevel.numberOfLines; j++)
            {
                newLevel.columns[i].letterOnThisColum[j] = wordsFromTheGrid[currentLine];
                currentLine++;
            }
        }

        InitializeWordsToSearchInThisLevel(levelInfo, newLevel);


        int tempCurrentLineIndexFromLevelInfo = ++currentLineIndexFromLevelInfo;

        for (int i = currentLineIndexFromLevelInfo; i < int.Parse(levelInfo[tempCurrentLineIndexFromLevelInfo]) + tempCurrentLineIndexFromLevelInfo; i++)
        {
            currentLineIndexFromLevelInfo++;
            newLevel.tipsFromThisLevel.Add(levelInfo[currentLineIndexFromLevelInfo].ToString());
        }

        InitializeLevelGridValues(levelInfo, newLevel);

        GameManager.Instance.CurrentLevel = newLevel;
    }
    private void InitializeWordsToSearchInThisLevel(string[] infoToLoad, HuntWordsSO levelToInitializeWordsToSearch)
    {
        int tempCurrentLineIndexFromLevelInfo = currentLineIndexFromLevelInfo;

        for (int i = currentLineIndexFromLevelInfo; i < int.Parse(infoToLoad[tempCurrentLineIndexFromLevelInfo]) + tempCurrentLineIndexFromLevelInfo; i++)
        {
            currentLineIndexFromLevelInfo++;
            levelToInitializeWordsToSearch.wordsToSearchInThisLevel.Add(infoToLoad[currentLineIndexFromLevelInfo].ToString().Replace("\r", ""));
        }
    }

    private void InitializeLevelGridValues(string[] infoToLoad, HuntWordsSO levelToInitializeGridValues)
    {
        levelToInitializeGridValues.gameGridConfiguration.paddingLeft = int.Parse(infoToLoad[++currentLineIndexFromLevelInfo]);
        levelToInitializeGridValues.gameGridConfiguration.paddingTop = int.Parse(infoToLoad[++currentLineIndexFromLevelInfo]);

        levelToInitializeGridValues.gameGridConfiguration.cellSize.x = int.Parse(infoToLoad[++currentLineIndexFromLevelInfo]);
        levelToInitializeGridValues.gameGridConfiguration.cellSize.y = int.Parse(infoToLoad[++currentLineIndexFromLevelInfo]);

        levelToInitializeGridValues.gameGridConfiguration.spacing.x = int.Parse(infoToLoad[++currentLineIndexFromLevelInfo]);
        levelToInitializeGridValues.gameGridConfiguration.spacing.y = int.Parse(infoToLoad[++currentLineIndexFromLevelInfo]);
    }
}
