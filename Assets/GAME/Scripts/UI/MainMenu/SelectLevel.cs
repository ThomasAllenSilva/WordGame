using UnityEngine;
using TMPro;

public class SelectLevel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private FadeManager fadeManager;

    private int selectedLevel = 2;

    private void Start()
    {
        selectedLevel = DataManager.Instance.PlayerDataManager.PlayerData.currentGameLevel;
        ChangeUILevelNumberValue();
    }

    public void PlaySelectedLevel()
    {
        selectedLevel = int.Parse(levelText.text);

        PlayerData playerData = new PlayerData(selectedLevel);
        DataManager.Instance.PlayerDataManager.OverWriteCurrentPlayerData(playerData);
    }

    public void IncreaseLevelToPlay()
    {
        if (selectedLevel + 1 <= DataManager.Instance.PlayerDataManager.PlayerData.currentGameLevel)
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
        if (selectedLevel == 1) this.gameObject.SetActive(false);
    }
}
