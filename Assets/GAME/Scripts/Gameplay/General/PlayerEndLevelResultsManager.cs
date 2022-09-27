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

        GameManager.Instance.LevelManager.onLevelCompleted += StopLevelTimerCount;
        GameManager.Instance.LevelManager.onLevelCompleted += GivePlayerCoins;

    }

    private void StartIncreasingLevelTimerCount()
    {
        currentLevelTimerCount = 0f;

        PlayerRewardedCoinsFromCurrentLevel = GameManager.Instance.LevelManager.CoinsRewardFromCurrentLevel;

        canIncreaseLevelTimerCount = true;

        StartCoroutine(IncreaseLevelTimerCount());
    }

    private IEnumerator IncreaseLevelTimerCount()
    {
        while (canIncreaseLevelTimerCount)
        {
            currentLevelTimerCount += 1;
            yield return new WaitForSecondsRealtime(1);
        }
    }

    private void StopLevelTimerCount()
    {
        canIncreaseLevelTimerCount = false;

        TotalLevelConclusionTime = GetTotalConclusionTime();
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
