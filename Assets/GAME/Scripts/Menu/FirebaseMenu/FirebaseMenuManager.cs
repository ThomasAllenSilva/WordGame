using UnityEngine;

using System.Collections;
using UnityEngine.Localization.Settings;
public class FirebaseMenuManager : MonoBehaviour
{
    private CheckSystemLanguage systemLanguage;

    private InternetChecker internetChecker;

    public FirebaseManager DataBaseController { get; private set; }

    public DownloadManager DownloadManager { get; private set; }

    public bool HasInternetconnection { get { return internetChecker.InternetConnectBool; } private set { HasInternetconnection = value; } }

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

        StartCoroutine(InitializeDataBase());
    }

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(0.1f);
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[DataManager.Instance.GameDataManager.CurrentGameLanguageIndex];
    }

    private IEnumerator InitializeDataBase()
    {
        yield return new WaitForSeconds(0.5f);
        DataBaseController.gameObject.SetActive(true);
    }

    public string GetCurrentLocalSystemLanguageCode()
    {
        return LocalizationSettings.AvailableLocales.Locales[systemLanguage.LocalSystemLanguageIndexCode].Identifier.Code;
    }
}
