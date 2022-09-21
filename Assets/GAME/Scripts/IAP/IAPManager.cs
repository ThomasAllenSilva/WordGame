using UnityEngine;
using UnityEngine.Purchasing;

public class IAPManager : MonoBehaviour, IStoreListener
{

    private const string RemoveAdsID = "0.5removeads";

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        throw new System.NotImplementedException();
    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
        throw new System.NotImplementedException();
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        throw new System.NotImplementedException();
    }

    public void OnRemoveAdsPurchaseCompleted()
    {
        DataManager.Instance.GameDataManager.SetAdsAsBuyed();
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
    {
        if(purchaseEvent.purchasedProduct.definition.id == RemoveAdsID)
        {
            DataManager.Instance.GameDataManager.SetAdsAsBuyed();
        }

        else
        {
            
        }

        return PurchaseProcessingResult.Complete;
    }
}
