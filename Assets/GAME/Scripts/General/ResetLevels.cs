using UnityEngine;

public class ResetLevels : MonoBehaviour
{
    public void ResetAllLevels()
    {
        DataManager.Instance.FirebaseDataManager.ResetFirebaseData();
    }
}
