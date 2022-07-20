using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(GridLayoutGroup))]
public class CreateGameGrid : GameGrid
{
    private int currentLineIndex = 0;
    private int amountOfBox = 0;
    private GridLayoutGroup gridLayoutGroup;

    protected override void Awake()
    {
        base.Awake();
        gridLayoutGroup = GetComponent<GridLayoutGroup>();

        SetGridLaoutGroupValues();
    }

    private void SetGridLaoutGroupValues()
    {
        gridLayoutGroup.padding.left = currentLevel.gameGridConfiguration.paddingLeft;
        gridLayoutGroup.padding.top = currentLevel.gameGridConfiguration.paddingTop;
        gridLayoutGroup.cellSize = currentLevel.gameGridConfiguration.cellSize;
        gridLayoutGroup.spacing = currentLevel.gameGridConfiguration.spacing;
    }

    private void CreateGameObjects(int amountOfColumns, int amountOfLines)
    {
        for (int i = 0; i < amountOfColumns; i++)
        {
            GameObject columHeaderImageBox = objectPooler.GetTheNextLetterBoxObjectFromListQueue();

            columHeaderImageBox.name = "box" + amountOfBox;

            if (Box.currentPrincipalBoxChecked == null)
            {
                Box.currentPrincipalBoxChecked = columHeaderImageBox.GetComponent<Box>();
            }

            Text cachedTextComponentFromcolumHeaderTextBox = columHeaderImageBox.GetComponentInChildren<Text>();
            cachedTextComponentFromcolumHeaderTextBox.text = currentLevel.columns[i].letterOnThisColum[currentLineIndex];

            columHeaderImageBox.SetActive(true);
            amountOfBox++;
        }


        currentLineIndex++;

        if (currentLineIndex < amountOfLines)
        {
            CreateGameObjects(currentLevel.amountOfColumns, currentLevel.amountOfLines);
        }
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        CreateGameObjects(currentLevel.amountOfColumns, currentLevel.amountOfLines);
    }

    private void OnDisable()
    {
        objectPooler.ResetLetterBoxListIndex();
        currentLineIndex = 0;
        amountOfBox = 0;
    }
}