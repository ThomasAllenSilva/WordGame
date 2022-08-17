using System.Collections.Generic;
using System.Text;
using System;
using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour
{
    private StringBuilder levelFileName = new StringBuilder("level");

    public Level CurrentLevel { get; private set; }

    private GameManager gameManager;

    private int nextLevelID;

    public Action onLevelCompleted;

    public Action onLevelStarted;

    private int amountOfWordsFinded;

    private void Awake()
    {
        gameManager = GameManager.Instance;
        CurrentLevel = new Level();
        LoadNextLevel();
    }

    private void LoadNextLevel()
    {
        nextLevelID = gameManager.DataManager.PlayerDataManager.CurrentGameLevel;

        AppendFileName();

        string dataToLoad = DataManager.Instance.LoadDataManager.LoadFileData(levelFileName.ToString());

        if (dataToLoad != null && dataToLoad != "")
        {
            JsonUtility.FromJsonOverwrite(dataToLoad, CurrentLevel);

            CurrentLevel.LoadNewLevelDataInfo();
        }

        else
        {
            //No Level Canvas
            Debug.Log("no level info");
        }

        ClearFileName();
    }

    private void AppendFileName()
    {
        levelFileName.Append(nextLevelID);
        levelFileName.Append(gameManager.DataManager.GameDataManager.CurrentGameLanguageCode);
    }

    private void ClearFileName()
    {
        levelFileName.Clear();
        levelFileName.Append("level");
    }

    public void StartLevel()
    {
        StartCoroutine(InvokeStartLevelEvent());
    }

    private IEnumerator InvokeStartLevelEvent()
    {
        yield return new WaitForSeconds(1f);
        onLevelStarted?.Invoke();
    }

    public void FindedNewWord()
    {
        amountOfWordsFinded++;

        CheckIfLevelIsCompleted();
    }

    private void CheckIfLevelIsCompleted()
    {
        if (amountOfWordsFinded == CurrentLevel.wordsToSearchInThisLevel.Count)
        {
            FinishedLevel();
        }
    }

    private void FinishedLevel()
    {
        StartCoroutine(InvokeCompletedLevelEvent());
    }

    private IEnumerator InvokeCompletedLevelEvent()
    {
        yield return new WaitForSeconds(0.4f);
        onLevelCompleted?.Invoke();
    }
}

public class Level
{
    public List<Colum> columns;

    public GridConfiguration gameGridConfiguration = new GridConfiguration();

    public int amountOfColumns;
    public int amountOfLines;

    public string cellSize, cellSpacing, paddingLeftBottom;

    public string[] lettersFromLines;
    public string letters;

    public List<string> wordsToSearchInThisLevel = new List<string>();
    public string wordsToSearch;

    public string[] tipsFromThisLevel;
    public string tips;

    private void CreateNewColum()
    {
        columns = new List<Colum>(amountOfColumns);

        for (int i = 0; i < columns.Capacity; i++)
        {
            columns.Add(new Colum(amountOfLines));
        }
    }

    public void LoadNewLevelDataInfo()
    {
        CreateNewColum();

        lettersFromLines = letters.Split(";");

        int currentLine = 0;

        for (int i = 0; i < this.amountOfColumns; i++)
        {
            for (int j = 0; j < this.amountOfLines; j++)
            {
                columns[i].letterOnThisColum[j] = lettersFromLines[currentLine];
                currentLine++;
            }
        }

        string[] wordsToSearch = this.wordsToSearch.Split(";");

        wordsToSearchInThisLevel.Clear();
        for (int i = 0; i < wordsToSearch.Length; i++)
        {
            wordsToSearchInThisLevel.Add(wordsToSearch[i]);
        }

        tipsFromThisLevel = tips.Split(";");

        string[] cellSizeValues = cellSize.Split(";");
        gameGridConfiguration.cellSize.x = int.Parse(cellSizeValues[0]);
        gameGridConfiguration.cellSize.y = int.Parse(cellSizeValues[1]);

        string[] cellSpacingValues = cellSpacing.Split(";");
        gameGridConfiguration.spacing.x = int.Parse(cellSpacingValues[0]);
        gameGridConfiguration.spacing.y = int.Parse(cellSpacingValues[1]);


        string[] paddingValues = paddingLeftBottom.Split(";");
        gameGridConfiguration.paddingLeft = int.Parse(paddingValues[0]);
        gameGridConfiguration.paddingTop = int.Parse(paddingValues[1]);
    }
}

public struct Colum
{
    public string[] letterOnThisColum;

    public Colum(int amountOfLines)
    {
        letterOnThisColum = new string[amountOfLines];
    }
}

public struct GridConfiguration
{
    public int paddingLeft;
    public int paddingTop;
    public Vector2 cellSize;
    public Vector2 spacing;
}
