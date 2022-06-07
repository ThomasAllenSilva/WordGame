using UnityEngine;

[CreateAssetMenu(fileName = "HuntWordBoard", menuName = "Create NewHuntWordsList")]
public class HuntWordsSO : ScriptableObject
{
    private const string ALPHABET = "AÃBCDEFGHIJKLMNOPQRSTUVWXYZ";
    public string[] wordsToSearchInThisLevel;

    public int numberOfColumns;
    public int numberOfLines;

    public Colum[] columns;
    public GridConfiguration gameGridConfiguration = new GridConfiguration();
    public BoxConfiguration boxConfiguration = new BoxConfiguration();



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
    public class BoxConfiguration 
    {
        public Sprite boxSprite;
        public int fontSize;
        public Color32[] completedColor = new Color32[10];
    }

    public void CreateNewColum()
    {
        columns = new Colum[numberOfColumns];

        for (int i = 0; i < columns.Length; i++)
        {
            columns[i] = new Colum(numberOfLines);
        }
    }

    public void FillAllEmptyBoxesWithRandomLetters()
    {
        for (int i = 0; i < columns.Length; i++)
        {
            columns[i].FillAllEmptyBoxesInThisColumWithRandomLetters();
        }
    }

    public void DeleteAllLetters()
    {
        for (int i = 0; i < columns.Length; i++)
        {
            columns[i].DeleteAllLettersInThisColum();
        }
    }
}