using UnityEngine;
using UnityEngine.UI;

public class CreateWordsToSearchGrid : MonoBehaviour
{
    private GridLayoutGroup gridLayoutGroup;

    private Transform gameObjectParent;

    private WordToSearchFieldsController wordToSearchUIFields;

    [SerializeField] private HuntWordsSO currentWordsGrid;

    [SerializeField] private Font wordsFont;


    private void Awake()
    {
        wordToSearchUIFields = GetComponent<WordToSearchFieldsController>();

        gridLayoutGroup = GetComponent<GridLayoutGroup>();

        gameObjectParent = this.gameObject.transform;

        SetGridLaoutGroupValues();

        CreateWordsToSearchTipFields();

        Destroy(gridLayoutGroup, 1f);

        Destroy(this, 1f);
    }


    private void SetGridLaoutGroupValues()
    {
        gridLayoutGroup.padding.left = currentWordsGrid.wordsToSearchTipsGridConfiguration.paddingLeft;
        gridLayoutGroup.padding.top = currentWordsGrid.wordsToSearchTipsGridConfiguration.paddingTop;
        gridLayoutGroup.cellSize = currentWordsGrid.wordsToSearchTipsGridConfiguration.cellSize;
        gridLayoutGroup.spacing = currentWordsGrid.wordsToSearchTipsGridConfiguration.spacing;
    }

    private void CreateWordsToSearchTipFields()
    {
        for (int i = 0; i < currentWordsGrid.wordsToSearchInThisLevel.Length; i++)
        {
            GameObject wordToSearchTipImageField = new GameObject();
            wordToSearchTipImageField.SetActive(false);
            wordToSearchTipImageField.isStatic = true;
            wordToSearchTipImageField.AddComponent<Image>().raycastTarget = false;
            wordToSearchTipImageField.GetComponent<Image>().sprite = currentWordsGrid.boxConfiguration.boxSprite;
            wordToSearchTipImageField.name = currentWordsGrid.wordsToSearchInThisLevel[i];

            GameObject columHeaderTextBox = new GameObject();
            columHeaderTextBox.SetActive(false);
            columHeaderTextBox.AddComponent<Text>().alignment = TextAnchor.MiddleCenter;
            columHeaderTextBox.isStatic = true;

            Text wordToSearchTextField = columHeaderTextBox.GetComponent<Text>();
            wordToSearchTextField.color = Color.black;
            wordToSearchTextField.raycastTarget = false;
            wordToSearchTextField.text = currentWordsGrid.wordsToSearchInThisLevel[i];
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
