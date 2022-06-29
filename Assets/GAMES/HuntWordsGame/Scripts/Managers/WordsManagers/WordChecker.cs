using UnityEngine;
using UnityEngine.UI;
using System.Text;
using System.Collections.Generic;
using System.Collections;

public class WordChecker : MonoBehaviour
{
    [SerializeField] private WordToSearchFieldsController wordsToSearchUI;

    [SerializeField] private Text wordField;

    [SerializeField] private Color32[] completedColor;

    private StringBuilder wordToFill = new StringBuilder();

    private int currentColorIndex = 0;

    private HuntWordsSO currentLevel;

    private GameManager gameManager;

    private void Awake()
    {
        gameManager = GameManager.Instance;
        currentLevel = gameManager.currentLevel;
        gameManager.PlayerTouchControllerInfo().TouchUpEvent += CheckIfTheWordToFillIsEqualsToAnyWordToSearchInThisLevel;
    }

    public void AddNewLetterToWordToFill(string letterToAdd) => wordField.text = wordToFill.Append(letterToAdd).ToString();

    public void RemoveTheLastLetterAddedToWordToFill() => wordField.text = wordToFill.Remove(wordToFill.Length - 1, 1).ToString();

    public void CheckIfTheWordToFillIsEqualsToAnyWordToSearchInThisLevel()
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

    private void SetToWordToFillAndWordFieldVariablesEmptyValues()
    {
        wordToFill.Clear();
        wordField.text = "";
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


    private string BinarySearchString(List<string> listToLoop, string stringToSearch, out int index)
    {
        int startIndex = 0;

        int lastIndex = listToLoop.Count - 1;

        int indexToFind = listToLoop.IndexOf(stringToSearch);

        index = indexToFind;

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

    public void OnChangedLevel()
    {
        currentLevel = gameManager.currentLevel;
        currentColorIndex = 0;
    }
}
