using UnityEngine;
using TMPro;

public class PlayerCoinsTextInfo : MonoBehaviour
{
    private TextMeshProUGUI coinsText;

    void Awake() => coinsText = GetComponent<TextMeshProUGUI>();

    public void UpdatePlayerCoinsTextInfo()
    {
        coinsText.text = DataManager.Instance.PlayerDataManager.CurrentPlayerCoins.ToString();
    }

    private void OnEnable()
    {
        coinsText.text = DataManager.Instance.PlayerDataManager.CurrentPlayerCoins.ToString();
    }
}
