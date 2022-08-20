using UnityEngine;
using UnityEngine.Localization.Settings;

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
    }

    public void ChangeGameLanguage()
    {
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[languageDropdown.value];

        DataManager.Instance.GameDataManager.UpdateGameData(languageDropdown.value, LocalizationSettings.SelectedLocale.Identifier.Code);
    }

    public void ReturnToDefaultValue()
    {
        languageDropdown.value = defaultLanguageDropdownValue;
    }
}
