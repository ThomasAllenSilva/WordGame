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
        if (DataManager.Instance.PlayerDataManager.PlayerData.purchasedBackgrounds[transform.parent.GetSiblingIndex()])
        {
            ChangeButtonActionToSelectBackground();
       

            if (DataManager.Instance.PlayerDataManager.PlayerData.selectedBackground == transform.parent.GetSiblingIndex())
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

    public void ChangeButtonActionToSelectBackground()
    {
        RemoveAllListenersFromButton();
        selectBackgroundAction = new UnityAction(ChangeSelectedButton);
        selectBackgroundAction += ChangeBackgroundTheme;

        thisBackgroundButton.onClick.AddListener(selectBackgroundAction);
        transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "Selecionar";
    }

    private void ChangeSelectedButton()
    {
        transform.parent.parent.GetChild(DataManager.Instance.PlayerDataManager.PlayerData.selectedBackground).GetComponentInChildren<Button>().interactable  = true;

        thisBackgroundButton.interactable = false;
    }

    private void ChangeBackgroundTheme()
    {
        Background.Instance.ChangeBackgroundTheme(backgroundImageInfo.GetThisBackgroundColor(), backgroundImageInfo.GetThisBackgroundTheme());
        SaveSelectedBackground();
    }

    private void SaveSelectedBackground()
    {
        DataManager.Instance.PlayerDataManager.SaveSelectedBackground(transform.parent.GetSiblingIndex());
    }

    private void RemoveAllListenersFromButton()
    {
        thisBackgroundButton.onClick.RemoveAllListeners();
    }

    private void OnDestroy()
    {
        backgroundBuyHandler.onThisPurchasedBackground -= ChangeButtonActionToSelectBackground;
    }
}
