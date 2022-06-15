using UnityEngine;
using UnityEngine.UI;

public class CreateGameGrid : GameGrid
{
    private int currentLineIndex = 0;

    int amountOfBox = 0;

    protected override void Awake()
    {
        base.Awake();

        CreateGameObjects(currentHuntWordsLevel.numberOfColumns, currentHuntWordsLevel.numberOfLines);

        Destroy(this, 1f);
    }

    protected override void SetGridLaoutGroupValues()
    {
        gridLayoutGroup.padding.left = currentHuntWordsLevel.gameGridConfiguration.paddingLeft;
        gridLayoutGroup.padding.top = currentHuntWordsLevel.gameGridConfiguration.paddingTop;
        gridLayoutGroup.cellSize = currentHuntWordsLevel.gameGridConfiguration.cellSize;
        gridLayoutGroup.spacing = currentHuntWordsLevel.gameGridConfiguration.spacing;
    }

    private void CreateGameObjects(int amountOfColumns, int amountOfLines)
    {
        for (int i = 0; i < amountOfColumns; i++)
        {

            GameObject columHeaderImageBox = new GameObject();
            columHeaderImageBox.SetActive(false);
            columHeaderImageBox.isStatic = true;
            columHeaderImageBox.AddComponent<BoxCollider2D>().size = new Vector2(140f, 140f);
            columHeaderImageBox.GetComponent<BoxCollider2D>().isTrigger = true;
            columHeaderImageBox.AddComponent<Image>().color = Color.gray;
            columHeaderImageBox.GetComponent<Image>().sprite = currentHuntWordsLevel.boxConfiguration.boxSprite;
            columHeaderImageBox.AddComponent<Box>();
            columHeaderImageBox.name = "box" + amountOfBox;

            if (BoxController.currentPrincipalBoxChecked == null)
            {
                BoxController.currentPrincipalBoxChecked = columHeaderImageBox.GetComponent<Box>();
            }

            GameObject columHeaderTextBox = new GameObject();
            columHeaderTextBox.SetActive(false);
            columHeaderTextBox.AddComponent<Text>().alignment = TextAnchor.MiddleCenter;
            columHeaderTextBox.isStatic = true;

            Text cachedTextComponentFromcolumHeaderTextBox = columHeaderTextBox.GetComponent<Text>();
            cachedTextComponentFromcolumHeaderTextBox.color = new Color(100f, 100f, 100f, 255f);
            cachedTextComponentFromcolumHeaderTextBox.raycastTarget = false;
            cachedTextComponentFromcolumHeaderTextBox.text = currentHuntWordsLevel.columns[i].letterOnThisColum[currentLineIndex];
            cachedTextComponentFromcolumHeaderTextBox.font = wordsFont;
            cachedTextComponentFromcolumHeaderTextBox.fontSize = currentHuntWordsLevel.boxConfiguration.fontSize;

            RectTransform cachedRectTransformFromcolumHeaderTextBox = columHeaderTextBox.GetComponent<RectTransform>();
            cachedRectTransformFromcolumHeaderTextBox.anchorMin = Vector2.zero;
            cachedRectTransformFromcolumHeaderTextBox.anchorMax = Vector2.one;
            cachedRectTransformFromcolumHeaderTextBox.SetParent(columHeaderImageBox.transform);

            columHeaderImageBox.GetComponent<RectTransform>().SetParent(gameObjectParent);
            columHeaderImageBox.SetActive(true);
            columHeaderTextBox.SetActive(true);
            amountOfBox++;
        }


        currentLineIndex++;

        if (currentLineIndex < amountOfLines)
        {
            CreateGameObjects(currentHuntWordsLevel.numberOfColumns, currentHuntWordsLevel.numberOfLines);
        }
    }
}