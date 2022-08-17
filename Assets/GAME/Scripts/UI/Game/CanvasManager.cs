using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] private GameObject boxesCanvas;
    [SerializeField] private GameObject tipsScrollerCanvas;
    [SerializeField] private GameObject finishedLevelCanvas;

    private void Awake()
    {
        GameManager.Instance.LevelManager.onLevelStarted += EnableGameCanvas;
        GameManager.Instance.LevelManager.onLevelCompleted += EnableFinishedLevelCanvas;
    }

    private void EnableGameCanvas()
    {
        boxesCanvas.SetActive(true);
        tipsScrollerCanvas.SetActive(true);
    }

    private void EnableFinishedLevelCanvas()
    {
        finishedLevelCanvas.SetActive(true);
    }

    private void OnDestroy()
    {
        GameManager.Instance.LevelManager.onLevelCompleted += EnableFinishedLevelCanvas;
        GameManager.Instance.LevelManager.onLevelStarted += EnableGameCanvas;
    }
}
