using System;
using System.Text;

using UnityEngine;

using Firebase;
using Firebase.Database;
using Firebase.Extensions;

public class FirebaseManager : MonoBehaviour
{
    private string levelInfo;

    private int currentFireBaseLevelNumber;

    private DatabaseReference databaseReference;

    private readonly Uri dataBaseUrl = new Uri("https://huntwordslevels-default-rtdb.firebaseio.com/");

    private StringBuilder fireBaseLocalFileName = new StringBuilder("FireBaseData");

    private StringBuilder downloadedContentFileName = new StringBuilder("level");

    private DataManager dataManager;

    [SerializeField] private Animator fadeAnimator;

    [SerializeField] private FadeManager fadeManager;
    private void Awake() => dataManager = DataManager.Instance;

    private void Start()
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

                ReadDataBaseLevelValues(currentFireBaseLevelNumber);
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
                    ReadDataBaseLevelValues(currentFireBaseLevelNumber);
                }

                else
                {
                    fadeManager.SetSceneToLoadIndex(1);
                    fadeAnimator.Play("FadeIn");
                }
            }

            else if (task.IsFaulted)
            {
                ReadDataBaseLevelValues(levelID);
                return;
            }
        });
    }

    private void SaveDownloadedLevelContentInDisk()
    {
        downloadedContentFileName.Append(currentFireBaseLevelNumber);

        dataManager.SaveDataManager.SaveNewData(downloadedContentFileName, levelInfo);

        downloadedContentFileName.Replace(currentFireBaseLevelNumber.ToString(), null);
        downloadedContentFileName.Replace(DataManager.Instance.GetCurrentGameLanguageIdentifierCode(), null);

        currentFireBaseLevelNumber++;
    }

    public bool CheckIfFireBaseLocalDataExists()
    {
        return dataManager.CheckIfFileExists(fireBaseLocalFileName.ToString());
    }

    private void CreateNewFireBaseLocalData()
    {
        FireBaseLocalData fireBaseData = new FireBaseLocalData();

        dataManager.SaveDataManager.SaveNewData(fireBaseLocalFileName, fireBaseData);
    }

    private void LoadFireBaseLocalData()
    {
        fireBaseLocalFileName.Append(dataManager.GetCurrentGameLanguageIdentifierCode());

        string fireBaseLocalDataInfo = dataManager.LoadDataManager.LoadFileData(fireBaseLocalFileName.ToString());

        fireBaseLocalFileName.Clear();
        fireBaseLocalFileName.Append("FireBaseData");

        FireBaseLocalData fireBase = new FireBaseLocalData();
        JsonUtility.FromJsonOverwrite(fireBaseLocalDataInfo, fireBase);

        this.currentFireBaseLevelNumber = fireBase.currentFireBaseLevelNumber;

        InitializeFirebaseRealTimeDataBase();
    }
}

public class FireBaseLocalData
{
    public int currentFireBaseLevelNumber = 1;
}

