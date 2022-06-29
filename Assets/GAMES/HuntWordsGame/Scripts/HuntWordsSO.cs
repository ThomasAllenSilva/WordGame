using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New HuntWord Level", menuName = "New HuntWords Level")]
public class HuntWordsSO : ScriptableObject
{
    private const string ALPHABET = "AÃBCDEFGHIJKLMNOPQRSTUVWXYZ";

    public List<string> wordsToSearchInThisLevel;

    public int numberOfColumns;
    public int numberOfLines;

    public List<Colum> columns;
    public GridConfiguration gameGridConfiguration = new GridConfiguration();
    public WordsToSearchTipsGridConfiguration wordsToSearchTipsGridConfiguration = new WordsToSearchTipsGridConfiguration();

    public bool completedThisLevel;

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

        public void FillAllEmptyBoxesInThisColumWithRandomLetters()
        {
            for (int i = 0; i < letterOnThisColum.Length; i++)
            {
                if (letterOnThisColum[i] == "")
                {
                    char randomChar = ALPHABET[Random.Range(0, ALPHABET.Length)];
                    letterOnThisColum[i] = randomChar.ToString();
                }
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

    [System.Serializable]
    public class WordsToSearchTipsGridConfiguration
    {
        public int paddingLeft;
        public int paddingTop;
        public Vector2 cellSize;
        public Vector2 spacing;
    }


    public void CreateNewColum()
    {
        columns = new List<Colum>(numberOfColumns);

        for (int i = 0; i < columns.Capacity; i++)
        {
            columns.Add(new Colum(numberOfLines));
        }
    }

    public void FillAllEmptyBoxesWithRandomLetters()
    {
        for (int i = 0; i < columns.Count; i++)
        {
            columns[i].FillAllEmptyBoxesInThisColumWithRandomLetters();
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