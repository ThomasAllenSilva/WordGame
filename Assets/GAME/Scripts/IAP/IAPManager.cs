using UnityEngine;
using UnityEngine.Purchasing;

public class IAPManager : MonoBehaviour, IStoreListener
{
    private IStoreController storeController;
    private IExtensionProvider extensionProvider;

    private const string removeAds = "RemoveAds";

    private void InitializeIAP()
    {
        StandardPurchasingModule standardPurchasingModule = StandardPurchasingModule.Instance(AppStore.GooglePlay);
        ConfigurationBuilder configurationBuilder = ConfigurationBuilder.Instance(standardPurchasingModule);

        configurationBuilder.AddProduct(removeAds, ProductType.NonConsumable);

        UnityPurchasing.Initialize(this, configurationBuilder);
    }

    private void SellNoAds()
    {
        Product product = storeController.products.WithID(removeAds);

        if(product != null && product.availableToPurchase)
        {
            storeController.InitiatePurchase(product);
        }
    }
    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        extensions.GetExtension<IGooglePlayStoreExtensions>().RestoreTransactions(result => {
            if (result)
            {
                Debug.Log("restore purchase");
                // This does not mean anything was restored,
                // merely that the restoration process succeeded.
            }
            else
            {
                Debug.Log("nothing to restore");
                // Restoration failed.
            }
        });

    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {

    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {

    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
    {
        throw new System.NotImplementedException();
    }
}
