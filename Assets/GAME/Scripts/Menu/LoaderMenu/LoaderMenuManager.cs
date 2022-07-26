using UnityEngine;
using UnityEngine.Localization.Settings;
using System.Collections;

public class LoaderMenuManager : MonoBehaviour
{
    private CheckSystemLanguage systemLanguage;

    private InternetChecker internetChecker;

    public InitializeFirebase DataBaseController { get; private set; }

    public DownloadManager DownloadManager { get; private set; }

    public static LoaderMenuManager Instance;

    public bool HasInternetconnection { get { return internetChecker.InternetConnectBool; } private set { HasInternetconnection = value; } }


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

        systemLanguage = GetComponentInChildren<CheckSystemLanguage>();
        internetChecker = GetComponentInChildren<InternetChecker>();
        DataBaseController = GetComponentInChildren<InitializeFirebase>(true);
        DownloadManager = GetComponentInChildren<DownloadManager>();

        StartCoroutine(InitializeDataBase());
    }

    private void Start()
    {
        StartCoroutine(ChangeGameLanguageToLocalSystemLanguage());
    }

    private IEnumerator ChangeGameLanguageToLocalSystemLanguage()
    {
        yield return new WaitForSecondsRealtime(2f);
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[GetCurrentLocalSystemLanguageIndex()];
    }

    private IEnumerator InitializeDataBase()
    {
        yield return new WaitForSecondsRealtime(1.5f);
        DataBaseController.gameObject.SetActive(true);
    }

    private int GetCurrentLocalSystemLanguageIndex()
    {
        return systemLanguage.LocalSystemLanguageIndex;
    }
}
