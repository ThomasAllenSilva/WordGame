using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;
using TMPro;

public class ChangeLanguage : MonoBehaviour
{
    private TMP_Dropdown languageDropdown;

    private int defaultLanguageDropdownValue;

    private void Awake()
    {
        languageDropdown = GetComponent<TMP_Dropdown>();   
    }

    private void Start()
    {
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[DataManager.Instance.GameDataManager.GameData.currentGameLanguageIndex];

        languageDropdown.value = LocalizationSettings.AvailableLocales.Locales.IndexOf(LocalizationSettings.SelectedLocale);
        defaultLanguageDropdownValue = languageDropdown.value;
        
        GameObject.Find("WarningLanguagePanel").SetActive(false);
        GameObject.Find("Config-Button").GetComponent<Button>().interactable = true;
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
