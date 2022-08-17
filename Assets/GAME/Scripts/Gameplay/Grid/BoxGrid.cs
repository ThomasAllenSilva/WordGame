using TMPro;
public class BoxGrid : Grid
{
    private int currentLineIndex = 0;
    private int currentChildBoxIndex;

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
            boxText.text =  gameManager.LevelManager.CurrentLevel.columns[i].letterOnThisColum[currentLineIndex];

            transform.GetChild(currentChildBoxIndex).gameObject.SetActive(true);
            currentChildBoxIndex++;
        }

        if (currentLineIndex < gameManager.LevelManager.CurrentLevel.amountOfLines - 1)
        {
            currentLineIndex++;
            InitializeBoxes();
        }


    }

    protected void OnEnable()
    {
        InitializeGridValues();

        InitializeBoxes();
    }

    private void OnDisable()
    {
        currentChildBoxIndex = 0;
        currentLineIndex = 0;
    }
}
