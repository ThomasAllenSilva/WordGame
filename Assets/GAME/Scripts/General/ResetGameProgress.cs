using UnityEngine;

public class ResetGameProgress : MonoBehaviour
{
    public void ResetAllGameProgress()
    {
        ResetPlayerData();
        ResetGameData();
    }

    private void ResetPlayerData()
    {
        DataManager.Instance.PlayerDataManager.ResetPlayerData();
    }

    private void ResetGameData()
    {
        DataManager.Instance.GameDataManager.ResetGameData();
    }
}
