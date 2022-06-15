using UnityEngine;
using UnityEngine.UI;

public class CreateWordsToSearchGrid : GameGrid
{
    protected override void Awake()
    {
        base.Awake();

        CreateWordsToSearchTipFields();

        Destroy(this, 1f);
    }

    protected override void SetGridLaoutGroupValues()
    {
        gridLayoutGroup.padding.left = currentHuntWordsLevel.wordsToSearchTipsGridConfiguration.paddingLeft;
        gridLayoutGroup.padding.top = currentHuntWordsLevel.wordsToSearchTipsGridConfiguration.paddingTop;
        gridLayoutGroup.cellSize = currentHuntWordsLevel.wordsToSearchTipsGridConfiguration.cellSize;
        gridLayoutGroup.spacing = currentHuntWordsLevel.wordsToSearchTipsGridConfiguration.spacing;
    }

    private void CreateWordsToSearchTipFields()
    {
        for (int i = 0; i < currentHuntWordsLevel.wordsToSearchInThisLevel.Length; i++)
        {
            GameObject wordToSearchTipImageField = new GameObject();
            wordToSearchTipImageField.SetActive(false);
            wordToSearchTipImageField.isStatic = true;
            wordToSearchTipImageField.AddComponent<Image>().raycastTarget = false;
            wordToSearchTipImageField.GetComponent<Image>().sprite = currentHuntWordsLevel.boxConfiguration.boxSprite;
            wordToSearchTipImageField.name = currentHuntWordsLevel.wordsToSearchInThisLevel[i];

            GameObject columHeaderTextBox = new GameObject();
            columHeaderTextBox.SetActive(false);
            columHeaderTextBox.AddComponent<Text>().alignment = TextAnchor.MiddleCenter;
            columHeaderTextBox.isStatic = true;

            Text wordToSearchTextField = columHeaderTextBox.GetComponent<Text>();
            wordToSearchTextField.color = Color.black;
            wordToSearchTextField.raycastTarget = false;
            wordToSearchTextField.text = currentHuntWordsLevel.wordsToSearchInThisLevel[i];
            wordToSearchTextField.font = wordsFont;
            wordToSearchTextField.fontSize = 36;


            RectTransform cachedRectTransformFromcolumHeaderTextBox = columHeaderTextBox.GetComponent<RectTransform>();
            cachedRectTransformFromcolumHeaderTextBox.anchorMin = Vector2.zero;
            cachedRectTransformFromcolumHeaderTextBox.anchorMax = Vector2.one;
            cachedRectTransformFromcolumHeaderTextBox.SetParent(wordToSearchTipImageField.transform);


            wordToSearchTipImageField.GetComponent<RectTransform>().SetParent(gameObjectParent);
            wordToSearchTipImageField.SetActive(true);
            columHeaderTextBox.SetActive(true);
        }
    }
}