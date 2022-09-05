using TMPro;

public class BoxGrid : Grid
{
    private int currentLineIndex = 0;
    private int currentChildBoxIndex;

    private void Start()
    {
        gameManager.LevelManager.onLevelCompleted += ResetGridValues;
        gameManager.LevelManager.onLevelCompleted += DisableBoxes;

        gameManager.LevelManager.onLevelStarted += ResetGridValues;

        gameManager.LevelManager.onLevelStarted += InitializeGridValues;
        gameManager.LevelManager.onLevelStarted += InitializeBoxes;

    }
    private void InitializeGridValues()
    {
        gridLayoutGroup.padding.left = gameManager.LevelManager.CurrentLevel.gameGridConfiguration.paddingLeft;
        gridLayoutGroup.padding.top = gameManager.LevelManager.CurrentLevel.gameGridConfiguration.paddingTop;
        gridLayoutGroup.cellSize = gameManager.LevelManager.CurrentLevel.gameGridConfiguration.cellSize;
        gridLayoutGroup.spacing = gameManager.LevelManager.CurrentLevel.gameGridConfiguration.spacing;
    }

    private void InitializeBoxes()
    {
        for (int i = 0; i < gameManager.LevelManager.CurrentLevel.amountOfColumns; i++)
        {
            TextMeshProUGUI boxText = transform.GetChild(currentChildBoxIndex).GetComponentInChildren<TextMeshProUGUI>();
            boxText.fontSizeMax = 130f;
            boxText.text = gameManager.LevelManager.CurrentLevel.columns[i].letterOnThisColum[currentLineIndex];

            transform.GetChild(currentChildBoxIndex).gameObject.SetActive(true);
            currentChildBoxIndex++;
        }

        if (currentLineIndex < gameManager.LevelManager.CurrentLevel.amountOfLines - 1)
        {
            currentLineIndex++;
            InitializeBoxes();
        }
    }

    private void ResetGridValues()
    {
        currentChildBoxIndex = 0;
        currentLineIndex = 0;
    }

    private void DisableBoxes()
    {
        for (int i = 0; i < gameManager.LevelManager.CurrentLevel.amountOfColumns; i++)
        {
            transform.GetChild(currentChildBoxIndex).gameObject.SetActive(false);
            currentChildBoxIndex++;
        }

        if (currentLineIndex < gameManager.LevelManager.CurrentLevel.amountOfLines - 1)
        {
            currentLineIndex++;
            DisableBoxes();
        }
    }

    private void OnDestroy()
    {
        gameManager.LevelManager.onLevelCompleted -= ResetGridValues;
        gameManager.LevelManager.onLevelCompleted -= DisableBoxes;

        gameManager.LevelManager.onLevelStarted -= InitializeGridValues;
        gameManager.LevelManager.onLevelStarted -= InitializeBoxes;
    }
}