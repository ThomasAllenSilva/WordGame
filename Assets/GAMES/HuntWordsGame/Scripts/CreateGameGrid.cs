using UnityEngine;
using UnityEngine.UI;

public class CreateGameGrid : MonoBehaviour
{

    [SerializeField] private HuntWordsSO currentWordsGrid;

    [SerializeField] private Font wordsFont;

    private int currentLineIndex = 0;

    private Transform gameObjectParent;

    private GridLayoutGroup gridLayoutGroup;

    int amountOfBox = 0;

    private void Awake()
    {
        gridLayoutGroup = GetComponent<GridLayoutGroup>();

        gameObjectParent = this.gameObject.transform;

        SetGridLaoutGroupValues();

        CreateGameObjects(currentWordsGrid.numberOfColumns, currentWordsGrid.numberOfLines);

        Destroy(gridLayoutGroup, 1f);

        Destroy(this, 1f);
    }

    private void SetGridLaoutGroupValues()
    {
        gridLayoutGroup.padding.left = currentWordsGrid.gameGridConfiguration.paddingLeft;
        gridLayoutGroup.padding.top = currentWordsGrid.gameGridConfiguration.paddingTop;
        gridLayoutGroup.cellSize = currentWordsGrid.gameGridConfiguration.cellSize;
        gridLayoutGroup.spacing = currentWordsGrid.gameGridConfiguration.spacing;
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
            columHeaderImageBox.GetComponent<Image>().sprite = currentWordsGrid.boxConfiguration.boxSprite;
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
            cachedTextComponentFromcolumHeaderTextBox.text = currentWordsGrid.columns[i].letterOnThisColum[currentLineIndex];
            cachedTextComponentFromcolumHeaderTextBox.font = wordsFont;
            cachedTextComponentFromcolumHeaderTextBox.fontSize = currentWordsGrid.boxConfiguration.fontSize;

            RectTransform cachedRectTransformFromcolumHeaderTextBox = columHeaderTextBox.GetComponent<RectTransform>();
            cachedRectTransformFromcolumHeaderTextBox.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, currentWordsGrid.gameGridConfiguration.cellSize.x);
            cachedRectTransformFromcolumHeaderTextBox.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, currentWordsGrid.gameGridConfiguration.cellSize.y);
            cachedRectTransformFromcolumHeaderTextBox.SetParent(columHeaderImageBox.transform);


            columHeaderImageBox.GetComponent<RectTransform>().SetParent(gameObjectParent);
            columHeaderImageBox.SetActive(true);
            columHeaderTextBox.SetActive(true);
            amountOfBox++;
        }


        currentLineIndex++;

        if (currentLineIndex < amountOfLines)
        {
            CreateGameObjects(currentWordsGrid.numberOfColumns, currentWordsGrid.numberOfLines);
        }
    }
}
