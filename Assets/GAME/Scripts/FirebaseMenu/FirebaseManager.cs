using System;
using System.Collections;
using System.Text;

using UnityEngine;

using Firebase;
using Firebase.Database;
using Firebase.Extensions;

public class FirebaseManager : MonoBehaviour
{
    private string levelInfo;

    private DatabaseReference databaseReference;

    private readonly Uri dataBaseUrl = new Uri("https://huntwordlevels-c623d-default-rtdb.firebaseio.com/");

    private StringBuilder fireBaseLocalFileName = new StringBuilder("FireBaseData");

    private StringBuilder downloadedContentFileName = new StringBuilder("level");

    private DataManager dataManager;

    FireBaseLocalData fireBaseLocalData = new FireBaseLocalData()
;

    [SerializeField] private Animator fadeAnimator;

    private void Awake() => dataManager = DataManager.Instance;

    private IEnumerator Start()
    {
        yield return null;
        InitializeFirebase();
    }

    public void InitializeFirebase()
    {
        if (CheckIfFireBaseLocalDataExists())
        {
            LoadFireBaseLocalData();
        }

        else
        {
            CreateNewFireBaseLocalData();

            LoadFireBaseLocalData();
        }
    }

    public void InitializeFirebaseRealTimeDataBase()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            if (task.Result == DependencyStatus.Available)
            {
                FirebaseApp.DefaultInstance.Options.DatabaseUrl = dataBaseUrl;

                databaseReference = FirebaseDatabase.DefaultInstance.RootReference;

                ReadDataBaseLevelValues(fireBaseLocalData.currentFireBaseLevelNumber);
            }

            else
            {
                Debug.LogError(string.Format("Could not resolve all Firebase dependencies: {0}", task.Result));
                InitializeFirebaseRealTimeDataBase();
                return;
            }
        });
    }

    private void ReadDataBaseLevelValues(int levelID)
    {

        if (!FirebaseMenuManager.Instance.DownloadManager.CanDownloadNewContent) return;

        databaseReference.Database.GetReference("Levels").GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                levelInfo = task.Result.Child(levelID.ToString()).Child(dataManager.GetCurrentGameLanguageIdentifierCode()).GetRawJsonValue();

                if (levelInfo != null && levelInfo != "")
                {
                    SaveDownloadedLevelContentInDisk();   
                    IncreaseCurrentFireBaseLevelNumber();
                    ReadDataBaseLevelValues(fireBaseLocalData.currentFireBaseLevelNumber);
                }

                else
                {
                    fadeAnimator.Play("FadeIn");
                }
            }

            else if (task.IsFaulted)
            {
                fadeAnimator.Play("FadeIn");
                return;
            }
        });
    }

    private void SaveDownloadedLevelContentInDisk()
    {
        downloadedContentFileName.Append(fireBaseLocalData.currentFireBaseLevelNumber);

        dataManager.SaveDataManager.SaveNewData(downloadedContentFileName, levelInfo);

        downloadedContentFileName.Replace(fireBaseLocalData.currentFireBaseLevelNumber.ToString(), null);
        downloadedContentFileName.Replace(DataManager.Instance.GetCurrentGameLanguageIdentifierCode(), null);
    }

    public bool CheckIfFireBaseLocalDataExists()
    {
        fireBaseLocalFileName.Append(dataManager.GetCurrentGameLanguageIdentifierCode());

        bool fileExist = dataManager.CheckIfFileExists(fireBaseLocalFileName.ToString());

        fireBaseLocalFileName.Clear();
        fireBaseLocalFileName.Append("FireBaseData");

        return fileExist;
    }

    private void CreateNewFireBaseLocalData()
    {
        dataManager.SaveDataManager.SaveNewData(fireBaseLocalFileName, fireBaseLocalData);
    }

    private void LoadFireBaseLocalData()
    {
        fireBaseLocalFileName.Append(dataManager.GetCurrentGameLanguageIdentifierCode());

        string fireBaseLocalDataInfo = dataManager.LoadDataManager.LoadFileData(fireBaseLocalFileName.ToString());

        fireBaseLocalFileName.Clear();
        fireBaseLocalFileName.Append("FireBaseData");

        JsonUtility.FromJsonOverwrite(fireBaseLocalDataInfo, fireBaseLocalData);

        InitializeFirebaseRealTimeDataBase();
    }

    private void IncreaseCurrentFireBaseLevelNumber()
    {
        fireBaseLocalData.currentFireBaseLevelNumber++;
        SaveFirebaseLocalData();
    }

    private void SaveFirebaseLocalData()
    {
        dataManager.SaveDataManager.SaveNewData(fireBaseLocalFileName, fireBaseLocalData);
    }

    public void ResetFirebaseLocalData()
    {
        fireBaseLocalData = new FireBaseLocalData();
        SaveFirebaseLocalData();
    }
}

public class FireBaseLocalData
{
    public int currentFireBaseLevelNumber = 1;
}

