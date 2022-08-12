using UnityEngine.UI;

public class BoxGrid : Grid
{
    private void InitializeGridValues()
    {
        gridLayoutGroup.padding.left = gameManager.LevelManager.CurrentLevel.gameGridConfiguration.paddingLeft;
        gridLayoutGroup.padding.top = gameManager.LevelManager.CurrentLevel.gameGridConfiguration.paddingTop;
        gridLayoutGroup.cellSize = gameManager.LevelManager.CurrentLevel.gameGridConfiguration.cellSize;
        gridLayoutGroup.spacing = gameManager.LevelManager.CurrentLevel.gameGridConfiguration.spacing;
    }

    private void InitializeBoxes()
    {
        int amountOfBoxes = gameManager.LevelManager.CurrentLevel.amountOfColumns * gameManager.LevelManager.CurrentLevel.amountOfLines;

        for (int i = 0; i < amountOfBoxes; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }
    }

    protected void OnEnable()
    {
        InitializeGridValues();

        InitializeBoxes();
    }
}
