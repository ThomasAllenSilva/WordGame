using System;
using System.Text;

using UnityEngine;

using Firebase;
using Firebase.Database;
using Firebase.Extensions;


public class FirebaseRealTimeDataBaseReader : MonoBehaviour
{
    private DatabaseReference databaseReference;

    private readonly Uri dataBaseUrl = new Uri("https://huntwordlevels-c623d-default-rtdb.firebaseio.com/");

    private FirebaseLocalData firebaseLocalData;

    private DataManager dataManager;

    private StringBuilder downloadedContentFileName = new StringBuilder("level");

    private string levelInfo;

    [SerializeField] private Animator fadeSceneLoaderAnimator;

    public static Action onFailedToReadDataBaseValues;

    private void Awake() => firebaseLocalData = GetComponent<FirebaseLocalData>();

    private void Start()
    {
        dataManager = DataManager.Instance;
    }
    public void InitializeFirebaseRealTimeDataBase()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            if (task.Result == DependencyStatus.Available)
            {
                FirebaseApp.DefaultInstance.Options.DatabaseUrl = dataBaseUrl;

                databaseReference = FirebaseDatabase.DefaultInstance.RootReference;

                ReadDataBaseLevelValues(firebaseLocalData.CurrentFirebaseBaseLevelIDNumber);
            }

            else
            {
                Debug.LogError(string.Format("Could not resolve all Firebase dependencies: {0}", task.Result));
                onFailedToReadDataBaseValues?.Invoke();
                return;
            }
        });
    }

    private void ReadDataBaseLevelValues(int levelIDToRead)
    {
        if (!FirebaseMenuManager.Instance.DownloadManager.CanDownloadNewContent) return;

        databaseReference.Database.GetReference("Levels").GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                levelInfo = task.Result.Child(levelIDToRead.ToString()).Child(dataManager.GetCurrentGameLanguageIdentifierCode()).GetRawJsonValue();

                if (levelInfo != null && levelInfo != "")
                {
                    SaveDownloadedLevelContentInDisk();
                    firebaseLocalData.IncreaseCurrentFireBaseLevelNumber();
                    ReadDataBaseLevelValues(firebaseLocalData.CurrentFirebaseBaseLevelIDNumber);
                }

                else
                {
                    PlayFadeInAnimationToLoadMainMenuScene();
                }
            }

            else if (task.IsFaulted)
            {
                onFailedToReadDataBaseValues?.Invoke();
                return;
            }
        });
    }

    private void SaveDownloadedLevelContentInDisk()
    {
        downloadedContentFileName.Append(firebaseLocalData.CurrentFirebaseBaseLevelIDNumber);

        dataManager.SaveDataManager.SaveNewData(downloadedContentFileName, levelInfo);

        downloadedContentFileName.Replace(firebaseLocalData.CurrentFirebaseBaseLevelIDNumber.ToString(), null);
        downloadedContentFileName.Replace(DataManager.Instance.GetCurrentGameLanguageIdentifierCode(), null);
    }

    private void PlayFadeInAnimationToLoadMainMenuScene()
    {
        fadeSceneLoaderAnimator.Play("FadeIn");
    }
}
