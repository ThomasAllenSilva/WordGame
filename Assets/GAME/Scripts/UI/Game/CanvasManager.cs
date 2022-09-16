using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] private GameObject finishedLevelCanvas;
    [SerializeField] private GameObject noLevelInfoCanvas;
    [SerializeField] private GameObject mainGameCanvas;

    private void Start()
    {
        GameManager.Instance.LevelManager.onLevelStarted += OnLevelStarted;
        GameManager.Instance.LevelManager.onLevelCompleted += OnLevelFinished;
        GameManager.Instance.LevelManager.onNoLevelInfo += OnNoLevelInfo;
    }

    private void OnLevelStarted()
    {
        mainGameCanvas.SetActive(true);
        finishedLevelCanvas.SetActive(false);
    }

    private void OnLevelFinished()
    {
        finishedLevelCanvas.SetActive(true);
        mainGameCanvas.SetActive(false);
    }

    private void OnNoLevelInfo()
    {
        noLevelInfoCanvas.SetActive(true);
        mainGameCanvas.SetActive(false);
    }

    private void OnDestroy()
    {
        GameManager.Instance.LevelManager.onLevelCompleted -= OnLevelFinished;
        GameManager.Instance.LevelManager.onLevelStarted -= OnLevelStarted;
        GameManager.Instance.LevelManager.onNoLevelInfo -= OnNoLevelInfo;
    }
}
