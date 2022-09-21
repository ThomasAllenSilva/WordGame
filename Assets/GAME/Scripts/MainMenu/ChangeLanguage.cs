using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;
using TMPro;

public class ChangeLanguage : MonoBehaviour
{
    private TMP_Dropdown languageDropdown;

    private int defaultLanguageDropdownValue;

    [SerializeField] private GameObject warningChangeLanguagePanel;

    [SerializeField] private Button exitConfigPanelButton;

    [SerializeField] private Button resetProgressPanelButton;

    private void Awake()
    {
        languageDropdown = GetComponent<TMP_Dropdown>();   
    }

    private void Start()
    {
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[DataManager.Instance.GameDataManager.CurrentGameLanguageLocalizationIndex];

        languageDropdown.value = LocalizationSettings.AvailableLocales.Locales.IndexOf(LocalizationSettings.SelectedLocale);
        defaultLanguageDropdownValue = languageDropdown.value;
        
        warningChangeLanguagePanel.SetActive(false);
        exitConfigPanelButton.interactable = true;
        resetProgressPanelButton.interactable = true;

        transform.parent.GetComponent<ScaleTween>().ScaleToZeroAndDisableThisGameObject();
    }

    public void ChangeGameLanguage()
    {
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[languageDropdown.value];

        DataManager.Instance.GameDataManager.OverwriteGameDataLanguageInfo(LocalizationSettings.SelectedLocale.Identifier.Code, languageDropdown.value);
    }

    public void ReturnToDefaultValue()
    {
        languageDropdown.value = defaultLanguageDropdownValue;
    }
}
