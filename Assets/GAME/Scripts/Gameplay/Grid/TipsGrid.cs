using TMPro;

public class TipsGrid : Grid
{
    private void InitializeTipsUIFields()
    {
        for (int i = 0; i < gameManager.LevelManager.CurrentLevel.wordsToSearchInThisLevel.Count; i++)
        {
            transform.GetChild(i).name = gameManager.LevelManager.CurrentLevel.wordsToSearchInThisLevel[i];

            transform.GetChild(i).GetComponentInChildren<TextMeshProUGUI>().text = gameManager.LevelManager.CurrentLevel.tipsFromThisLevel[i];
            transform.GetChild(i).gameObject.SetActive(true);

            //    wordToSearchTextField.resizeTextForBestFit = true;

        }
    }

    private void OnEnable()
    {
        InitializeTipsUIFields();
    }
}
