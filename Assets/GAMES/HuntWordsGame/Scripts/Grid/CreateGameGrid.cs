using UnityEngine;
using UnityEngine.UI;

public class CreateGameGrid : GameGrid
{
    int currentLineIndex = 0;
    int amountOfBox = 0;

    protected override void SetGridLaoutGroupValues()
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
            CreateGameObjects(currentLevel.numberOfColumns, currentLevel.numberOfLines);
        }
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        CreateGameObjects(currentLevel.numberOfColumns, currentLevel.numberOfLines);
    }

    private void OnDisable()
    {
        objectPooler.ResetLetterBoxListIndex();
        currentLineIndex = 0;
        amountOfBox = 0;
    }
}