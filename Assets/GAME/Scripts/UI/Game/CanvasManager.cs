using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] private GameObject tipsScrollerCanvas;
    [SerializeField] private GameObject finishedLevelCanvas;
    [SerializeField] private GameObject noLevelInfoCanvas;

    private void Start()
    {
        GameManager.Instance.LevelManager.onLevelStarted += OnLevelStarted;
        GameManager.Instance.LevelManager.onLevelCompleted += OnLevelFinished;
        GameManager.Instance.LevelManager.onNoLevelInfo += OnNoLevelInfo;
    }

    private void OnLevelStarted()
    {
        tipsScrollerCanvas.SetActive(true);
        finishedLevelCanvas.SetActive(false);
    }

    private void OnLevelFinished()
    {
        finishedLevelCanvas.SetActive(true);
        tipsScrollerCanvas.SetActive(false);
    }

    private void OnNoLevelInfo()
    {
        Debug.Log("nocanvasinfo");
        noLevelInfoCanvas.SetActive(true);
        tipsScrollerCanvas.SetActive(false);
    }

    private void OnDestroy()
    {
        GameManager.Instance.LevelManager.onLevelCompleted -= OnLevelFinished;
        GameManager.Instance.LevelManager.onLevelStarted -= OnLevelStarted;
        GameManager.Instance.LevelManager.onNoLevelInfo -= OnNoLevelInfo;
    }
}
