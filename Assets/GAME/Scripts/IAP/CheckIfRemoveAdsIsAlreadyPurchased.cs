using UnityEngine;
using UnityEngine.UI;

public class CheckIfRemoveAdsIsAlreadyPurchased : MonoBehaviour
{
    void Start()
    {
        if (DataManager.Instance.GameDataManager.HasBuyedNoAds)
        {
            this.gameObject.GetComponent<Button>().interactable = false;
        }
    }
}
