using UnityEngine;
using UnityEngine.UI;

public class InitializeWordsToSearchFields : GameGrid
{
    private void CreateWordsToSearchTipFields()
    {
        for (int i = 0; i < currentLevel.wordsToSearchInThisLevel.Count; i++)
        {
            GameObject wordToSearchTipImageField = objectPooler.GetTheNextWordToSearchObjectFromListQueue();
            wordToSearchTipImageField.name = currentLevel.wordsToSearchInThisLevel[i];

            Text wordToSearchTextField = wordToSearchTipImageField.GetComponentInChildren<Text>();
            wordToSearchTextField.text = currentLevel.tipsFromThisLevel[i];
            wordToSearchTextField.resizeTextForBestFit = true;

            wordToSearchTipImageField.SetActive(true);
        }
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        CreateWordsToSearchTipFields();
    }

    private void OnDisable()
    {
        objectPooler.ResetWordToSearchListIndex();
    }
}
