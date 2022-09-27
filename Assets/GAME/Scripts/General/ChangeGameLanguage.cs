using UnityEngine;
using UnityEngine.Localization.Settings;

public class ChangeGameLanguage : MonoBehaviour
{
    private void Awake()
    {
        DataManager.Instance.GameDataManager.onFinishedLoadingGameData += ChangeLanguage;
    }

    private void ChangeLanguage()
    {
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[DataManager.Instance.GameDataManager.CurrentGameLanguageLocalizationIndex];
        Debug.Log("changed language");
    }

    private void OnDestroy()
    {
        DataManager.Instance.GameDataManager.onFinishedLoadingGameData -= ChangeLanguage;
    }
}
