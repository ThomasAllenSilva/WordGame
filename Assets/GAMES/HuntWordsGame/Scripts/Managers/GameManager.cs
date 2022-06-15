using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private PlayerTouchController playerTouchController;

    private WordChecker wordChecker;

    private void Awake()
    {
        Application.targetFrameRate = 60;

        Instance = this;

        playerTouchController = transform.GetComponentInChildren<PlayerTouchController>();
        wordChecker = transform.GetComponentInChildren<WordChecker>();
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
