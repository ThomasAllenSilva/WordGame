using UnityEngine;
using UnityEngine.UI;

public class FreeBackgroundHandler : MonoBehaviour
{
    private BackgroundImageInfo thisBackgroundImageInfo;
    private DataManager dataManager = DataManager.Instance;

    private void Start()
    {
        thisBackgroundImageInfo = GetComponentInChildren<BackgroundImageInfo>();

        if (dataManager.PlayerDataManager.CurrentSelectedBackground == transform.parent.GetSiblingIndex())
        {
            transform.GetComponentInChildren<Button>().interactable = false;
        }
    }

    public void ChangeBackgroundTheme()
    {
        Background.Instance.ChangeBackgroundTheme(thisBackgroundImageInfo.GetThisBackgroundColor(), thisBackgroundImageInfo.GetThisBackgroundTheme());
        ChangeSelectedButton();
        SaveSelectedBackground();
    }

    private void ChangeSelectedButton()
    {
        transform.parent.GetChild(dataManager.PlayerDataManager.CurrentSelectedBackground).GetComponentInChildren<Button>().interactable = true;
        transform.GetComponentInChildren<Button>().interactable = false;   
    }


    private void SaveSelectedBackground()
    {
        dataManager.PlayerDataManager.SaveSelectedBackground(transform.GetSiblingIndex());
    }
}
