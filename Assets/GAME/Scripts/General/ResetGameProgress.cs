using UnityEngine;

public class ResetGameProgress : MonoBehaviour
{
    public void ResetAllGameProgress()
    {
        ResetPlayerData();
        ResetFirebaseData();
        ResetGameData();
    }

    private void ResetPlayerData()
    {
        DataManager.Instance.PlayerDataManager.ResetPlayerData();
    }

    private void ResetFirebaseData()
    {
        
    }

    private void ResetGameData()
    {
        DataManager.Instance.GameDataManager.ResetGameData();
    }
}
