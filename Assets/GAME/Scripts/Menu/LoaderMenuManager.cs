using UnityEngine;
using UnityEngine.Localization.Settings;
using System.Collections;

public class LoaderMenuManager : MonoBehaviour
{
    public static LoaderMenuManager Instance;

    private SaveNewLevel SaveNewLevel;

    [SerializeField] private GameObject dataBaseObject;

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

        SaveNewLevel = GetComponentInChildren<SaveNewLevel>();

        StartCoroutine(InitializeDataBase());
    }

    public void SaveDownloadedLevel(string levelValues, int levelID)
    {
        SaveNewLevel.SaveNewLevelData(levelValues, levelID);
    }

    public string GetCurrentLanguageCode()
    {
        return LocalizationSettings.SelectedLocale.Identifier.Code;
    }

    private IEnumerator InitializeDataBase()
    {
        yield return new WaitForSecondsRealtime(1.5f);
        dataBaseObject.SetActive(true);
    }
}
