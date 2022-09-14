using UnityEngine;
using UnityEngine.UI;

public class FreeBackgroundHandler : MonoBehaviour
{
    [SerializeField] private Color backgroundColor;
    [SerializeField] private Sprite backgroundSprite;

    private void Start()
    {
        if (DataManager.Instance.PlayerDataManager.PlayerData.selectedBackground == transform.parent.GetSiblingIndex())
        {
            transform.GetComponentInChildren<Button>().interactable = false;
        }
    }

    public void ChangeBackgroundTheme()
    {
        Background.Instance.ChangeBackgroundTheme(backgroundColor, backgroundSprite);
        ChangeSelectedButton();
        SaveSelectedBackground();
    }

    private void ChangeSelectedButton()
    {
        transform.parent.GetChild(DataManager.Instance.PlayerDataManager.PlayerData.selectedBackground).GetComponentInChildren<Button>().interactable = true;
        transform.GetComponentInChildren<Button>().interactable = false;   
    }


    private void SaveSelectedBackground()
    {
        DataManager.Instance.PlayerDataManager.SaveSelectedBackground(transform.GetSiblingIndex());
    }

    public Sprite GetThisBackgroundTheme()
    {
        return backgroundSprite;
    }
    public Color GetThisBackgroundColor()
    {
        return backgroundColor;
    }
}
