using System.Text;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class WordChecker : MonoBehaviour
{
    [SerializeField] private TipsController tipsUIManager;

    [SerializeField] private Text wordField;

    [SerializeField] private StringBuilder wordToFill = new StringBuilder(11);

    private GameManager gameManager;

    private void Awake()
    {
        gameManager = GameManager.Instance;
        gameManager.PlayerTouchController.TouchUpEvent += CheckIfTheWordToFillIsEqualsToAnyWordToSearchInThisLevel;
    }

    public void AddLetterToWordToFill(string letterToAdd) => wordField.text = wordToFill.Append(letterToAdd).ToString();

    public void RemoveTheLastLetterAddedToWordToFill() => wordField.text = wordToFill.Remove(wordToFill.Length - 1, 1).ToString();

    private void CheckIfTheWordToFillIsEqualsToAnyWordToSearchInThisLevel()
    {
        if (wordToFill.Length > 1)
        {
            string filledWord = wordToFill.ToString();

            string wordCompleted = BinarySearchString(gameManager.LevelManager.CurrentLevel.wordsToSearchInThisLevel, filledWord, out int wordIndex);

            if (filledWord == wordCompleted)
            {
                Box.SetAllCurrentBoxesCheckedAsComplete();
                tipsUIManager.SetTipUIFieldComplete(wordIndex);
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



    private void SetToWordToFillAndWordFieldVariablesEmptyValues()
    {
        wordToFill.Clear();
        wordField.text = "";
    }
}
