using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] private GameObject boxesCanvas;
    [SerializeField] private GameObject tipsScrollerCanvas;

    private void Awake()
    {
        GameManager.Instance.LevelManager.onLevelCompleted += DisableCanvas;
        GameManager.Instance.LevelManager.onLevelStarted += EnableCanvas;
    }

    private void EnableCanvas()
    {
        boxesCanvas.SetActive(true);
        tipsScrollerCanvas.SetActive(true);
    }

    private void DisableCanvas()
    {
        boxesCanvas.SetActive(false);
        tipsScrollerCanvas.SetActive(false);
    }

    private void OnDestroy()
    {
        GameManager.Instance.LevelManager.onLevelCompleted += DisableCanvas;
        GameManager.Instance.LevelManager.onLevelStarted += EnableCanvas;
    }
}
