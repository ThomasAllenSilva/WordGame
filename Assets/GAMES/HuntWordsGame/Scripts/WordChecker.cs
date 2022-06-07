using UnityEngine;
using UnityEngine.UI;

public class WordChecker : MonoBehaviour
{
    [SerializeField] private HuntWordsSO currentHuntWordsLevel;

    [SerializeField] private Text wordField;

    private string[] wordsToSearch;

    private string wordToFill;

    private int currentColorIndex = 0;

    public static WordChecker Instance;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        wordsToSearch = new string[currentHuntWordsLevel.wordsToSearchInThisLevel.Length];

        for (int i = 0; i < currentHuntWordsLevel.wordsToSearchInThisLevel.Length; i++)
        {
            wordsToSearch[i] = currentHuntWordsLevel.wordsToSearchInThisLevel[i];
        }
    }

    public void AddLetterToWordToFill(string letterToAdd)
    {
        wordToFill += letterToAdd;

        //TODO: Find a way to not repeat this line of code:
        wordField.text = wordToFill;
    }

    public void RemoveTheLastLetterFromWordToFill()
    {
        wordToFill = wordToFill.Remove(wordToFill.LastIndexOf(wordToFill.Substring(wordToFill.Length)), 1);
        wordField.text = wordToFill;
    }

    public void CheckIfTheWordToFilIsEqualToAnyWordToSearch()
    {

        for (int i = 0; i < wordsToSearch.Length; i++)
        {
            if(wordToFill == wordsToSearch[i])
            {
                SetAllCurrentBoxesCheckedAsComplete();
                currentColorIndex += 1;
            }
        }

        wordToFill = "";
        wordField.text = wordToFill;

    }

    private void SetAllCurrentBoxesCheckedAsComplete()
    {
        for (int j = 0; j < BoxController.allCurrentBoxesThatAreChecked.Length; j++)
        {
            if (BoxController.allCurrentBoxesThatAreChecked[j] != null)
            {
                BoxController.allCurrentBoxesThatAreChecked[j].SetThisLetterAsCompleted(currentHuntWordsLevel.boxConfiguration.completedColor[currentColorIndex]);
                BoxController.amountOfBoxesThatAreChecked = 0;
            }
        }
    }
}
