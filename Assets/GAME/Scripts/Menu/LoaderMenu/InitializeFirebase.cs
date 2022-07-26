using System;
using System.Text;
using Firebase;
using Firebase.Database;

using UnityEngine;
using System.Collections;
public class InitializeFirebase : MonoBehaviour
{
    private DatabaseReference databaseReference;
    private readonly Uri dataBaseUrl = new Uri("https://huntwordslevels-default-rtdb.firebaseio.com/");

    private string levelInfo;
    private int currentFireBaseLevelNumber;

    private DataManager dataManager;

    private StringBuilder fireBaseFileName = new StringBuilder("FireBaseData");
    private StringBuilder downloadedContentFileName = new StringBuilder("level");

    private void Awake()
    {
        dataManager = DataManager.Instance;

        fireBaseFileName.Append(dataManager.GetCurrentGameLanguageCode());


        if (CheckIfFireBaseDataExists())
        {
            LoadFireBaseData();
        }

        else
        {
            CreateFireBaseData();

            LoadFireBaseData();
        }


     
    }

    private IEnumerator Start()
    {
        yield return new WaitForSecondsRealtime(4f);
        //ScenesManager.Instance.LoadMainMenuScene();
    }


    public void InitializeDataBase()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => 
        {
            DependencyStatus dependencyStatus = task.Result;

            if (dependencyStatus == DependencyStatus.Available)
            {
                FirebaseApp.DefaultInstance.Options.DatabaseUrl = dataBaseUrl;
                databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
                ReadDataBaseLevelValues(currentFireBaseLevelNumber);
            }

            else
            {
                Debug.LogError(string.Format("Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                return;
            }
        });
    }

    private void ReadDataBaseLevelValues(int levelID)
    {
        if(!LoaderMenuManager.Instance.DownloadManager.CanDownloadNewContent) return;

        databaseReference.Database.GetReference("Levels").GetValueAsync().ContinueWith(task => 
        {
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                levelInfo = snapshot.Child(levelID.ToString()).Child(dataManager.GetCurrentGameLanguageCode()).GetRawJsonValue();

                if (levelInfo != null)
                {
                    SaveDownloadedContentInDisk();
                    ReadDataBaseLevelValues(levelID + 1);
                }

                else
                {

                    return;
                }



            }



            else if (task.IsFaulted)
            {
                return;
            }
        });


        ScenesManager.Instance.LoadMainMenuScene();
    }

    private void SaveDownloadedContentInDisk()
    {
        downloadedContentFileName.Append(currentFireBaseLevelNumber);
        downloadedContentFileName.Append(DataManager.Instance.GetCurrentGameLanguageCode());
        DataManager.Instance.SaveDataManager.SaveNewFile(downloadedContentFileName.ToString(), levelInfo);
    }

    public bool CheckIfFireBaseDataExists()
    {
        return dataManager.SaveDataManager.CheckIfFileExists(fireBaseFileName.ToString());
    }

    private void CreateFireBaseData()
    {
        FireBaseData fireBaseData = new FireBaseData();
        dataManager.SaveDataManager.SaveNewData(fireBaseFileName.ToString(), fireBaseData);

    }

    private void LoadFireBaseData()
    {
        string fireBaseData = dataManager.LoadDataManager.LoadFileData(fireBaseFileName.ToString());

        FireBaseData fireBase = new FireBaseData();
        JsonUtility.FromJsonOverwrite(fireBaseData, fireBaseData);

        ReadFireBaseDataValues(fireBase);
        InitializeDataBase();
    }

    private void ReadFireBaseDataValues(FireBaseData fireBaseData)
    {
        this.currentFireBaseLevelNumber = fireBaseData.currentFireBaseLevelNumber;
    }

}

public class FireBaseData
{
    public int currentFireBaseLevelNumber = 1;
}


