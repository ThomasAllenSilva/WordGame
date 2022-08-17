using UnityEngine;

using System.Collections;
using UnityEngine.Localization.Settings;
public class FirebaseMenuManager : MonoBehaviour
{
    private CheckSystemLanguage systemLanguage;

    private InternetChecker internetChecker;

    public FirebaseManager DataBaseController { get; private set; }

    public DownloadManager DownloadManager { get; private set; }

    public bool HasInternetconnection { get { return internetChecker.InternetConnectionBool; } private set { HasInternetconnection = value; } }

    public static FirebaseMenuManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        else
        {
            Destroy(gameObject);
        }

        Application.targetFrameRate = 60;

        systemLanguage = GetComponentInChildren<CheckSystemLanguage>();
        internetChecker = GetComponentInChildren<InternetChecker>();
        DataBaseController = GetComponentInChildren<FirebaseManager>(true);
        DownloadManager = GetComponentInChildren<DownloadManager>();
    }

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(1f);
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[DataManager.Instance.GameDataManager.CurrentGameLanguageIndex];

        InitializeDataBase();
    }

    private void InitializeDataBase()
    {
        DataBaseController.gameObject.SetActive(true);
    }

    public string GetCurrentLocalSystemLanguageCode()
    {
        return LocalizationSettings.AvailableLocales.Locales[systemLanguage.LocalSystemLanguageIndexCode].Identifier.Code;
    }
}
