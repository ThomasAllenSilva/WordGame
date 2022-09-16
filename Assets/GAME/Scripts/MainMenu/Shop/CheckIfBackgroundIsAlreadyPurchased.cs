using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

[RequireComponent(typeof(BackgroundBuyHandler))]
[RequireComponent(typeof(BackgroundImageInfo))]

public class CheckIfBackgroundIsAlreadyPurchased : MonoBehaviour
{
    private static UnityAction selectBackgroundAction;

    private static UnityAction buyBackgroundAction;

    private static DataManager dataManager = DataManager.Instance;

    private BackgroundBuyHandler backgroundBuyHandler;

    private BackgroundImageInfo backgroundImageInfo;

    private Button thisBackgroundButton;

    private void Awake()
    {
        thisBackgroundButton = GetComponent<Button>();
        backgroundBuyHandler = GetComponent<BackgroundBuyHandler>();
        backgroundImageInfo = GetComponent<BackgroundImageInfo>();
    }

    private void Start()
    {
        if (dataManager.PlayerDataManager.PurchasedBackgrounds[transform.parent.GetSiblingIndex()])
        {
            ChangeButtonActionToSelectBackground();
       

            if (dataManager.PlayerDataManager.CurrentSelectedBackground == transform.parent.GetSiblingIndex())
            {
                thisBackgroundButton.interactable = false;
            }
        }

        else
        {
       
            ChangeButtonActionToBuyBackground();
        }
    }

    private void ChangeButtonActionToBuyBackground()
    {
        backgroundBuyHandler.onThisPurchasedBackground += ChangeButtonActionToSelectBackground;
        RemoveAllListenersFromButton();
        buyBackgroundAction = new UnityAction(BuyBackground);
        thisBackgroundButton.onClick.AddListener(buyBackgroundAction);
    }

    private void BuyBackground()
    {
        backgroundBuyHandler.BuyBackground();
    }

    private void ChangeButtonActionToSelectBackground()
    {
        RemoveAllListenersFromButton();
        selectBackgroundAction = new UnityAction(ChangeSelectedButton);
        selectBackgroundAction += ChangeBackgroundTheme;

        thisBackgroundButton.onClick.AddListener(selectBackgroundAction);
        ChangeThisButtonTextToSelectText();
    }

    private void ChangeSelectedButton()
    {
        transform.parent.parent.GetChild(dataManager.PlayerDataManager.CurrentSelectedBackground).GetComponentInChildren<Button>().interactable  = true;

        thisBackgroundButton.interactable = false;
    }

    private void ChangeBackgroundTheme()
    {
        Background.Instance.ChangeBackgroundTheme(backgroundImageInfo.GetThisBackgroundColor(), backgroundImageInfo.GetThisBackgroundTheme());
        SaveSelectedBackground();
    }

    private void SaveSelectedBackground()
    {
        dataManager.PlayerDataManager.SaveSelectedBackground(transform.parent.GetSiblingIndex());
    }

    private void RemoveAllListenersFromButton()
    {
        thisBackgroundButton.onClick.RemoveAllListeners();
    }

    private void OnDestroy()
    {
        backgroundBuyHandler.onThisPurchasedBackground -= ChangeButtonActionToSelectBackground;
    }

    private void ChangeThisButtonTextToSelectText()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(true);
    }
}
