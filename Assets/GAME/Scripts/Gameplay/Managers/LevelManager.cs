using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private StringBuilder levelFileName = new StringBuilder("level");

    private LevelInfo CurrentLevel;

    private GameManager gameManager;

    private int levelID;

    private int amountOfWordsFinded;

    public event Action onLevelCompleted;

    public event Action onLevelStarted;

    public event Action onNoLevelInfo;

    public int BoxGridPaddingLeft { get { return CurrentLevel.boxGridConfiguration.paddingLeft; } }

    public int BoxGridPaddingTop { get { return CurrentLevel.boxGridConfiguration.paddingTop; } }

    public Vector2 BoxGridCellSize { get { return CurrentLevel.boxGridConfiguration.cellSize; } }

    public Vector2 BoxGridCellSpacing { get { return CurrentLevel.boxGridConfiguration.cellSpacing; } }

    public int AmountOfColumns { get { return CurrentLevel.amountOfColumns; } }

    public int AmountOfLines { get { return CurrentLevel.amountOfLines; } }

    public List<string> WordsToSearchInThisLevel { get { return CurrentLevel.wordsToSearchInThisLevel; } }

    public string[] TipsFromThisLevel { get { return CurrentLevel.tipsFromThisLevel; } }

    public int CoinsRewardFromCurrentLevel { get { return CurrentLevel.coinsRewardFromThisLevel; } }

    public List<Colum> Columns { get { return CurrentLevel.columns; } }


    private void Awake()
    {
        onLevelCompleted += ResetLevelValues;
        gameManager = GameManager.Instance;

        CurrentLevel = new LevelInfo();
    }

    public void LoadNextLevel()
    {
        levelID = gameManager.DataManager.PlayerDataManager.CurrentGameLevel;

        AppendFileName();

        string dataToLoad = DataManager.Instance.LoadDataManager.LoadFileData(levelFileName.ToString());
   
        if (dataToLoad != null && dataToLoad != "")
        {
             JsonUtility.FromJsonOverwrite(dataToLoad, CurrentLevel);

             CurrentLevel.LoadLevelDataInfo();

             Invoke(nameof(StartLevelAction), 1f);
        }

        else
        {
            onNoLevelInfo?.Invoke();
        }

        ClearFileName();
    }

    private void AppendFileName()
    {
        levelFileName.Append(levelID);
        levelFileName.Append(gameManager.DataManager.GameDataManager.CurrentGameLanguageCode);
    }

    private void ClearFileName()
    {
        levelFileName.Clear();
        levelFileName.Append("level");
    }

    private void ResetLevelValues()
    {
        amountOfWordsFinded = 0;
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
            FinishLevel();
        }
    }

    private void FinishLevel()
    {
        StartCoroutine(InvokeCompletedLevelEvent());
    }

    private IEnumerator InvokeCompletedLevelEvent()
    {
        yield return new WaitForSeconds(0.4f);
        onLevelCompleted?.Invoke();
    }

    private void StartLevelAction()
    {
        onLevelStarted?.Invoke();
    }


    private class LevelInfo
    {
        public List<Colum> columns;

        public BoxGridConfiguration boxGridConfiguration = new BoxGridConfiguration();

        public int amountOfColumns;
        public int amountOfLines;

        public string cellSize, cellSpacing, paddingLeftBottom;

        public string[] lettersFromLines;
        public string letters;

        public List<string> wordsToSearchInThisLevel = new List<string>();
        public string wordsToSearch;

        public string[] tipsFromThisLevel;
        public string tips;

        public int coinsRewardFromThisLevel;

        private void CreateNewColum()
        {
            columns = new List<Colum>(amountOfColumns);

            for (int i = 0; i < columns.Capacity; i++)
            {
                columns.Add(new Colum(amountOfLines));
            }
        }

        public void LoadLevelDataInfo()
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
            boxGridConfiguration.cellSize.x = int.Parse(cellSizeValues[0]);
            boxGridConfiguration.cellSize.y = int.Parse(cellSizeValues[1]);

            string[] cellSpacingValues = cellSpacing.Split(";");
            boxGridConfiguration.cellSpacing.x = int.Parse(cellSpacingValues[0]);
            boxGridConfiguration.cellSpacing.y = int.Parse(cellSpacingValues[1]);


            string[] paddingValues = paddingLeftBottom.Split(";");
            boxGridConfiguration.paddingLeft = int.Parse(paddingValues[0]);
            boxGridConfiguration.paddingTop = int.Parse(paddingValues[1]);
        }

 

        public struct BoxGridConfiguration
        {
            public int paddingLeft;
            public int paddingTop;
            public Vector2 cellSize;
            public Vector2 cellSpacing;
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
}