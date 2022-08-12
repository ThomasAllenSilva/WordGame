using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New HuntWord Level", menuName = "New HuntWords Level")]
public class HuntWordsSO : ScriptableObject
{
    private const string ALPHABET = "AÃBCDEFGHIJKLMNOPQRSTUVWXYZ";

    public int amountOfColumns;
    public int amountOfLines;

    public string cellSize, cellSpacing, paddingLeftBottom;

    public string[] lettersFromLines;
    public string letters;

    public List<string> wordsToSearchInThisLevel = new List<string>();
    public string words; 

    public string[] tipsFromThisLevel;
    public string tips;

    public List<Colum> columns;

    public GridConfiguration gameGridConfiguration = new GridConfiguration();

    public void LoadNewLevelDataInfo()
    {
        CreateNewColum();

        lettersFromLines = new string[amountOfColumns * amountOfLines];

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

        string[] wordsToSearch = words.Split(";");

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

    [System.Serializable]
    public class Colum
    {
        public string[] letterOnThisColum;

        public Colum(int amountOfLines)
        {
            letterOnThisColum = new string[amountOfLines];
        }

        public void DeleteAllLettersInThisColum()
        {
            for (int i = 0; i < letterOnThisColum.Length; i++)
            {
                letterOnThisColum[i] = "";
            }
        }
    }

    [System.Serializable]
    public class GridConfiguration
    {
        public int paddingLeft;
        public int paddingTop;
        public Vector2 cellSize;
        public Vector2 spacing;
    }

    public void CreateNewColum()
    {
        columns = new List<Colum>(amountOfColumns);

        for (int i = 0; i < columns.Capacity; i++)
        {
            columns.Add(new Colum(amountOfLines));
        }
    }

    public void DeleteAllLetters()
    {
        for (int i = 0; i < columns.Count; i++)
        {
            columns[i].DeleteAllLettersInThisColum();
        }
    }
}