using UnityEngine;
using TMPro;
using System;

public class BackgroundBuyHandler : MonoBehaviour
{
    private int thisBackgroundPrice;

    public Action onThisPurchasedBackground;

    public static Action onFailedToPurchaseBackground;

    public static Action onPurchasedAnyBackground;


    private void Awake()
    {
        thisBackgroundPrice = int.Parse(transform.GetChild(2).GetComponent<TextMeshProUGUI>().text);
    }

    public void BuyBackground()
    {
        if (CheckIfCanBuyThisBackground())
        {
            SpendPlayerCoins();
            onThisPurchasedBackground?.Invoke();
            onPurchasedAnyBackground?.Invoke();
        }

        else
        {
            onFailedToPurchaseBackground?.Invoke();
        }
    }

    private bool CheckIfCanBuyThisBackground()
    {
        if (DataManager.Instance.PlayerDataManager.CurrentPlayerCoins >= thisBackgroundPrice)
        {
            return true;
        }

        else
        {
            return false;
        }
    }

    private void SpendPlayerCoins()
    {
        DataManager.Instance.PlayerDataManager.SpendPlayerCoins(thisBackgroundPrice);
        DataManager.Instance.PlayerDataManager.SavePurchasedBackground(transform.parent.GetSiblingIndex());
    }
}
