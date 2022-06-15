using UnityEngine;
using UnityEngine.UI;

public class WordChecker : MonoBehaviour
{
    [SerializeField] private HuntWordsSO currentHuntWordsLevel;

    [SerializeField] private Text wordField;

    [SerializeField] private string wordToFill;

    private int currentColorIndex = 0;

    private WordToSearchFieldsController wordsToSearchUI;

    private void Awake()
    {
        wordsToSearchUI = GameObject.FindGameObjectWithTag("WordsToSearchUI").GetComponent<WordToSearchFieldsController>();
    }

    public void AddNewLetterToWordToFill(string letterToAdd)
    {
       wordField.text = wordToFill += letterToAdd;
    }

    public void RemoveTheLastLetterAddedToWordToFill()
    {
       wordField.text = wordToFill = wordToFill.Remove(wordToFill.LastIndexOf(wordToFill.Substring(wordToFill.Length)), 1);
    }

    public void CheckIfTheWordToFillIsEqualsToAnyWordToSearchInThisLevel()
    {
        for (int i = 0; i < currentHuntWordsLevel.wordsToSearchInThisLevel.Length; i++)
        {
            if (wordToFill == currentHuntWordsLevel.wordsToSearchInThisLevel[i])
            {
                SetAllCurrentBoxesCheckedAsComplete();
                wordsToSearchUI.SetWordUIFieldComplete(i);
                currentColorIndex += 1;
            }
        }

        SetToWordToFillAndWordFieldVariablesEmptyValues();

    }

    private void SetToWordToFillAndWordFieldVariablesEmptyValues()
    {
        wordField.text = wordToFill = "";
    }

    private void SetAllCurrentBoxesCheckedAsComplete()
    {
        for (int i = 0; i < BoxController.currentBoxesThatAreChecked.Length; i++)
        {
            if (BoxController.currentBoxesThatAreChecked[i] != null)
            {
                BoxController.currentBoxesThatAreChecked[i].SetThisBoxAsCompleted(currentHuntWordsLevel.boxConfiguration.completedColor[currentColorIndex]);
            }
        }

        BoxController.indexOfAmountOfBoxesThatAreCurrentChecked = 0;
    }
}
