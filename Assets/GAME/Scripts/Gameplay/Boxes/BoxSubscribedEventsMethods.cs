using UnityEngine;

public class BoxSubscribedEventsMethods : MonoBehaviour
{
    private void Awake()
    {
        GameManager.Instance.PlayerTouchController.TouchUpEvent += Box.SetRandomColorToCompletedBoxImageColor;
        GameManager.Instance.PlayerTouchController.TouchUpEvent += Box.ResetTouchUp;

        GameManager.Instance.LevelManager.onLevelCompleted += Box.ResetGame;
    }

    private void OnDestroy()
    {
        GameManager.Instance.PlayerTouchController.TouchUpEvent -= Box.SetRandomColorToCompletedBoxImageColor;

        GameManager.Instance.PlayerTouchController.TouchUpEvent -= Box.ResetTouchUp;

        GameManager.Instance.LevelManager.onLevelCompleted -= Box.ResetGame;
    }
}
