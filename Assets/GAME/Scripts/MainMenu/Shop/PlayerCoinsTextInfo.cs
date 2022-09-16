using UnityEngine;
using TMPro;

public class PlayerCoinsTextInfo : MonoBehaviour
{
    private TextMeshProUGUI coinsText;

    void Awake() => coinsText = GetComponent<TextMeshProUGUI>();

    private void Start()
    {
       coinsText.text = DataManager.Instance.PlayerDataManager.CurrentPlayerCoins.ToString();
    }

    public void UpdatePlayerCoinsTextInfo()
    {
        coinsText.text = DataManager.Instance.PlayerDataManager.CurrentPlayerCoins.ToString();
    }
}
