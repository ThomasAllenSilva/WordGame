using UnityEngine;
using UnityEngine.UI;
using System.Text;
using System.Collections.Generic;

public class WordChecker : MonoBehaviour
{
    [SerializeField] private WordToSearchFieldsController wordsToSearchUI;

    [SerializeField] private Text wordField;

    [SerializeField] private Color32[] completedColor;

    [SerializeField] private StringBuilder wordToFill = new StringBuilder(11);

    private int currentColorIndex = 0;

    private HuntWordsSO currentLevel;

    private GameManager gameManager;

    public List<string> teste = new List<string>(10);
    private void Awake()
    {
        gameManager = GameManager.Instance;
        gameManager.PlayerTouchController.TouchUpEvent += CheckIfTheWordToFillIsEqualsToAnyWordToSearchInThisLevel;
        currentLevel = gameManager.CurrentLevel;
        for (int i = 0; i < currentLevel.wordsToSearchInThisLevel.Count; i++)
        {
            teste.Add(currentLevel.wordsToSearchInThisLevel[i]);
        }
    }

    public void AddNewLetterToWordToFill(string letterToAdd) => wordField.text = wordToFill.Append(letterToAdd).ToString();

    public void RemoveTheLastLetterAddedToWordToFill() => wordField.text = wordToFill.Remove(wordToFill.Length - 1, 1).ToString();

    private void CheckIfTheWordToFillIsEqualsToAnyWordToSearchInThisLevel()
    {
        if (wordToFill.Length > 1)
        {
            string filledWord = wordToFill.ToString();

            string wordCompleted = BinarySearchString(currentLevel.wordsToSearchInThisLevel, filledWord, out int wordIndex);

            if (filledWord == wordCompleted)
            {
                SetAllCurrentBoxesCheckedAsComplete();
                wordsToSearchUI.SetWordUIFieldComplete(wordIndex);
                currentColorIndex += 1;
            }
        }

        SetToWordToFillAndWordFieldVariablesEmptyValues();
    }

    private string BinarySearchString(List<string> listToLoop, string stringToSearch, out int indexOfTheString)
    {
        int startIndex = 0;

        int lastIndex = listToLoop.Count - 1;

        int indexToFind = listToLoop.IndexOf(stringToSearch);

        indexOfTheString = indexToFind;

        while (startIndex <= lastIndex)
        {
            int middleIndex = (startIndex + lastIndex) / 2;

            if (middleIndex == indexToFind)
            {
                return listToLoop[middleIndex];
            }

            else if (middleIndex > indexToFind)
            {
                lastIndex = middleIndex - 1;
            }

            else
            {
                startIndex = middleIndex + 1;
            }
        }

        return null;
    }

    private void SetAllCurrentBoxesCheckedAsComplete()
    {
        foreach (Box box in Box.currentBoxesThatAreChecked)
        {
            box.SetThisBoxAsCompleted(completedColor[currentColorIndex]);
        }

        Box.indexOfAmountOfBoxesThatAreCurrentChecked = 0;
        Box.currentBoxesThatAreChecked.Clear();
    }

    private void SetToWordToFillAndWordFieldVariablesEmptyValues()
    {
        wordToFill.Clear();
        wordField.text = "";
    }

    public void OnChangedLevel()
    {
        currentLevel = gameManager.CurrentLevel;
        currentColorIndex = 0;
    }
}
