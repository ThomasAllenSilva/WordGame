using TMPro;

public class BoxGrid : Grid
{
    private int currentLineIndex = 0;
    private int currentChildBoxIndex;

    private bool firstTimeEnablingGameObject = true;

    protected override void OnLevelCompleted()
    {
        ResetGridValues();
        DisableBoxes();
        ResetGridValues();
    }

    protected override void OnLevelStarted()
    {
        InitializeGridValues();
        InitializeBoxes();
    }

    private void InitializeGridValues()
    {
        gridLayoutGroup.padding.left = gameManager.LevelManager.BoxGridPaddingLeft;
        gridLayoutGroup.padding.top = gameManager.LevelManager.BoxGridPaddingTop;
        gridLayoutGroup.cellSize = gameManager.LevelManager.BoxGridCellSize;
        gridLayoutGroup.spacing = gameManager.LevelManager.BoxGridCellSpacing;
    }

    private void InitializeBoxes()
    {
        for (int i = 0; i < gameManager.LevelManager.AmountOfColumns; i++)
        {
            TextMeshProUGUI boxText = transform.GetChild(currentChildBoxIndex).GetComponentInChildren<TextMeshProUGUI>();
            boxText.fontSizeMax = 130f;
            boxText.text = gameManager.LevelManager.Columns[i].letterOnThisColum[currentLineIndex];

            transform.GetChild(currentChildBoxIndex).gameObject.SetActive(true);
            currentChildBoxIndex++;
        }

        if (currentLineIndex < gameManager.LevelManager.AmountOfLines - 1)
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
        for (int i = 0; i < gameManager.LevelManager.AmountOfColumns; i++)
        {
            transform.GetChild(currentChildBoxIndex).gameObject.SetActive(false);
            currentChildBoxIndex++;
        }

        if (currentLineIndex < gameManager.LevelManager.AmountOfLines - 1)
        {
            currentLineIndex++;
            DisableBoxes();
        }
    }

    private void OnEnable()
    {
        if (firstTimeEnablingGameObject)
        {
            InitializeGridValues();
            InitializeBoxes();
            firstTimeEnablingGameObject = false;
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