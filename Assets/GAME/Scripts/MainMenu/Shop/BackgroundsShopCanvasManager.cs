using UnityEngine;

public class BackgroundsShopCanvasManager : MonoBehaviour
{
    [SerializeField] private GameObject notEnoughCoinsCanvas;
    [SerializeField] private GameObject backgroundShopCanvas;

    [SerializeField] private PlayerCoinsTextInfo playerCoinsTextInfo;

    private void Awake()
    {
        BackgroundBuyHandler.onFailedToPurchaseBackground += EnableNotEnoughCoinsCanvas;
        BackgroundBuyHandler.onPurchasedAnyBackground += UpdatePlayerCoinsUIText;
    }

    public void EnableNotEnoughCoinsCanvas()
    {
        notEnoughCoinsCanvas.SetActive(true);
        DisableBackgroundsShopCanvas();
    }

    private void DisableBackgroundsShopCanvas()
    {
        backgroundShopCanvas.SetActive(false);
    }

    private void UpdatePlayerCoinsUIText()
    {
        playerCoinsTextInfo.UpdatePlayerCoinsTextInfo();
    }

    private void OnDestroy()
    {
        BackgroundBuyHandler.onFailedToPurchaseBackground -= EnableNotEnoughCoinsCanvas;
        BackgroundBuyHandler.onPurchasedAnyBackground -= UpdatePlayerCoinsUIText;
    }
}
