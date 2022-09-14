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
        thisBackgroundPrice = int.Parse(transform.GetComponentInChildren<TextMeshProUGUI>().text);
    }

    public void BuyBackground()
    {
        if (CheckIfCanBuyThisBackground())
        {
            SpendPlayerCoins();
            ChangeThisBackgroundButtonEvent();
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
        if (DataManager.Instance.PlayerDataManager.PlayerData.playerCoins >= thisBackgroundPrice)
        {
            return true;
        }

        else
        {
            return false;
        }
    }

    private void ChangeThisBackgroundButtonEvent()
    {
       // GetComponent<CheckIfBackgroundIsAlreadyPurchased>().ChangeButtonActionToSelectBackground();
    }

    private void SpendPlayerCoins()
    {
        DataManager.Instance.PlayerDataManager.SpendPlayerCoins(thisBackgroundPrice);
        DataManager.Instance.PlayerDataManager.SavePurchasedBackground(transform.parent.GetSiblingIndex());
    }
}
