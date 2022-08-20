using UnityEngine;


public class PlayGame : MonoBehaviour
{
    [SerializeField] private FadeManager fadeManager;

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
        if (DataManager.Instance.PlayerDataManager.PlayerData.currentGameLevel > 1)
        {
            return false;
        }

        else
        {
            return true;
        }
    }
}
