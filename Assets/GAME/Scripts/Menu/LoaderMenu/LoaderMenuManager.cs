using UnityEngine;
using UnityEngine.Localization.Settings;

using System.Collections;

public class LoaderMenuManager : MonoBehaviour
{
    private CheckSystemLanguage systemLanguage;

    private InternetChecker internetChecker;

    public FirebaseManager DataBaseController { get; private set; }

    public DownloadManager DownloadManager { get; private set; }

    public bool HasInternetconnection { get { return internetChecker.InternetConnectBool; } private set { HasInternetconnection = value; } }


    public static LoaderMenuManager Instance;

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
        DataBaseController = GetComponentInChildren<FirebaseManager>(true);
        DownloadManager = GetComponentInChildren<DownloadManager>();

        StartCoroutine(InitializeDataBase());
    }

    private void Start()
    {
        ChangeGameLanguageToLocalSystemLanguage();
    }

    private void ChangeGameLanguageToLocalSystemLanguage()
    {
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[GetCurrentLocalSystemLanguageIndex()];
    }

    private IEnumerator InitializeDataBase()
    {
        yield return new WaitForSeconds(0.5f);
        DataBaseController.gameObject.SetActive(true);
    }

    private int GetCurrentLocalSystemLanguageIndex()
    {
        return systemLanguage.LocalSystemLanguageIndex;
    }
}
