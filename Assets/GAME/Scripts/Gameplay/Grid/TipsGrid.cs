using TMPro;

public class TipsGrid : Grid
{
    private void InitializeTipsUIFields()
    {
        for (int i = 0; i < gameManager.LevelManager.WordsToSearchInThisLevel.Count; i++)
        {
            transform.GetChild(i).name = gameManager.LevelManager.WordsToSearchInThisLevel[i];

            transform.GetChild(i).GetComponentInChildren<TextMeshProUGUI>().text = gameManager.LevelManager.TipsFromThisLevel[i];
            transform.GetChild(i).gameObject.SetActive(true);
        }
    }

    private void ResetTipsUIFields()
    {
        for (int i = 0; i < gameManager.LevelManager.WordsToSearchInThisLevel.Count; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        InitializeTipsUIFields();
    }

    private void OnDisable()
    {
        ResetTipsUIFields();
    }
}
