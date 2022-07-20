using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Localization.Settings;

public class ChangeLanguage : MonoBehaviour
{
    private Dropdown languageDropdown;

    private void Awake() => languageDropdown = GetComponent<Dropdown>();

    public void ChangeGameLanguage()
    {
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[languageDropdown.value];
    }
}
