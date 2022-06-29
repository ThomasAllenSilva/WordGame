using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public event Action EnterNewLevel;

    public event Action LeaveCurrentLevel;

    public HuntWordsSO currentLevel;

    public Font fontStyle;
    
    private PlayerTouchController playerTouchController;

    private WordChecker wordChecker;

    private void Awake()
    {
        Application.targetFrameRate = 60;

        Instance = this;

        playerTouchController = transform.GetComponentInChildren<PlayerTouchController>();
        wordChecker = transform.GetComponentInChildren<WordChecker>();
    }

    public void ChangeLevel(HuntWordsSO nextLevel)
    {
        currentLevel = nextLevel;
        EnterNewLevel?.Invoke();
    }

    public void LeaveLevel()
    {
        LeaveCurrentLevel?.Invoke();
    }

    public PlayerTouchController PlayerTouchControllerInfo()
    {
        return playerTouchController;
    }

    public WordChecker WordCheckerInfo()
    {
        return wordChecker;
    }
}
