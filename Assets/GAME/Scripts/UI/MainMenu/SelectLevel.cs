using UnityEngine;
using TMPro;

public class SelectLevel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private FadeManager fadeManager;

    private int selectedLevel;

    private void Start()
    {
        ChangeUILevelNumberValue();
    }

    public void PlaySelectedLevel()
    {
        selectedLevel = int.Parse(levelText.text);

        DataManager.Instance.PlayerDataManager.ChangeCurrentPlayerLevel(selectedLevel);
    }

    public void IncreaseLevelToPlay()
    {
        if (selectedLevel + 1 <= DataManager.Instance.PlayerDataManager.PlayerData.maxGameLevelPlayed)
        {
            selectedLevel += 1;
            ChangeUILevelNumberValue();
        }
    }

    public void DecreaseLevelToPlay()
    {
        if (selectedLevel - 1 > 1)
        {
            selectedLevel -= 1;
            ChangeUILevelNumberValue();
        }
    }

    private void ChangeUILevelNumberValue()
    {
        levelText.text = selectedLevel.ToString();
    }

    private void OnEnable()
    {
        selectedLevel = DataManager.Instance.PlayerDataManager.PlayerData.maxGameLevelPlayed;
        if (selectedLevel < 3) this.gameObject.SetActive(false);
    }
}
