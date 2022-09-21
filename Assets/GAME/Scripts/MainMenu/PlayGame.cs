using UnityEngine;


public class PlayGame : MonoBehaviour
{
    [SerializeField] private FadeManager fadeManager;
    [SerializeField] private GameObject selectGameLevel;

    private void Start()
    {
        if (CheckIfIsNewGame())
        {
            fadeManager.SetSceneToLoadIndex(2);
        }

        else
        {
            fadeManager.SetSceneToLoadIndex(3);
        }
    }
     
    private bool CheckIfIsNewGame()
    {
        if (DataManager.Instance.PlayerDataManager.CurrentGameLevel > 1)
        {
            selectGameLevel.SetActive(true);
            return false;
        }

        else
        {
            return true;
        }
    }
}
