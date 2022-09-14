using UnityEngine;

using System;
using System.Collections;

public class PlayerEndLevelResultsManager : MonoBehaviour
{
    private bool canIncreaseLevelTimerCount = true;

    private float currentLevelTimerCount;
    
    public int PlayerRewardedCoinsFromCurrentLevel { get; private set; }

    public string TotalLevelConclusionTime {get; private set;}

    private void Awake()
    {
        GameManager.Instance.LevelManager.onLevelStarted += StartIncreasingLevelTimerCount;

        GameManager.Instance.LevelManager.onLevelCompleted += GivePlayerCoins;
        GameManager.Instance.LevelManager.onLevelCompleted += StopLevelTimerCount;
    }

    private void StartIncreasingLevelTimerCount()
    {
        currentLevelTimerCount = 0f;

        PlayerRewardedCoinsFromCurrentLevel = GameManager.Instance.LevelManager.CurrentLevel.coinsRewardFromThisLevel;

        StartCoroutine(IncreaseLevelTimerCount());
    }

    private IEnumerator IncreaseLevelTimerCount()
    {
        while (canIncreaseLevelTimerCount)
        {
            currentLevelTimerCount += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }

    private void StopLevelTimerCount()
    {
        TotalLevelConclusionTime = GetTotalConclusionTime();

        canIncreaseLevelTimerCount = false;
    }

    private string GetTotalConclusionTime()
    {
        TimeSpan totalConclusionTime = TimeSpan.FromSeconds(currentLevelTimerCount);

        return totalConclusionTime.ToString("hh':'mm':'ss");
    }

    private void GivePlayerCoins()
    {
        DataManager.Instance.PlayerDataManager.IncreasePlayerCoins(PlayerRewardedCoinsFromCurrentLevel);
    }
}
