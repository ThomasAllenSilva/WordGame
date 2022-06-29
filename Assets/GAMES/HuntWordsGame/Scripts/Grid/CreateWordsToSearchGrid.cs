using UnityEngine;
using UnityEngine.UI;

public class CreateWordsToSearchGrid : GameGrid
{
    protected override void SetGridLaoutGroupValues()
    {
        gridLayoutGroup.padding.left = currentLevel.wordsToSearchTipsGridConfiguration.paddingLeft;
        gridLayoutGroup.padding.top = currentLevel.wordsToSearchTipsGridConfiguration.paddingTop;
        gridLayoutGroup.cellSize = currentLevel.wordsToSearchTipsGridConfiguration.cellSize;
        gridLayoutGroup.spacing = currentLevel.wordsToSearchTipsGridConfiguration.spacing;
    }

    private void CreateWordsToSearchTipFields()
    {
        for (int i = 0; i < currentLevel.wordsToSearchInThisLevel.Count; i++)
        {
            GameObject wordToSearchTipImageField = objectPooler.GetTheNextWordToSearchObjectFromListQueue();
            wordToSearchTipImageField.name = currentLevel.wordsToSearchInThisLevel[i];

            Text wordToSearchTextField = wordToSearchTipImageField.GetComponentInChildren<Text>();
            wordToSearchTextField.text = currentLevel.wordsToSearchInThisLevel[i];

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