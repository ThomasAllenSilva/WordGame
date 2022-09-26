using UnityEngine;
using UnityEngine.Localization.Settings;

public class ChangeGameLanguage : MonoBehaviour
{
    void OnEnable()
    {
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[DataManager.Instance.GameDataManager.CurrentGameLanguageLocalizationIndex];
    }
}
