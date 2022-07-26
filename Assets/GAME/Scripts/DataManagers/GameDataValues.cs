using UnityEngine;

public class GameDataValues : MonoBehaviour
{
    public int Coins { get; private set; }

    public int CurrentGameLevel { get; private set; }

    public string CurrentGameLanguageCode { get; private set; }

    public void LoadGameData(GameData gameData)
    {
        this.Coins = gameData.coins;
        this.CurrentGameLevel = gameData.currentGameLevel;
        this.CurrentGameLanguageCode = gameData.currentGameLanguage;
    }
}
