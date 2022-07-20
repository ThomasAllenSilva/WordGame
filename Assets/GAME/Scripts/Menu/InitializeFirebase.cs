using System;
using UnityEngine;
using UnityEngine.Localization.Settings;
using Firebase;
using Firebase.Database;
public class InitializeFirebase : MonoBehaviour
{
   private DatabaseReference databaseReference;

    private string levelInfo;
    private int levelID;

    private readonly Uri dataBaseUrl = new("https://huntwordslevels-default-rtdb.firebaseio.com/");
 
    private void Awake()
    {
        InitializeDataBase();
    }

    private void InitializeDataBase()
    {
            FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
            var dependencyStatus = task.Result;

            if (dependencyStatus == DependencyStatus.Available)
            {
                    FirebaseApp.DefaultInstance.Options.DatabaseUrl = dataBaseUrl;
                    databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
                ReadDataBaseLevelValues(1);
            }

            else
            {
                  Debug.LogError(string.Format("Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                // Firebase Unity SDK is not safe to use here.
            }
        });
    }

    private void ReadDataBaseLevelValues(int levelID)
    {
        databaseReference.Database.GetReference("Levels").GetValueAsync().ContinueWith(task => {

        if (task.IsCompleted)
        {
            DataSnapshot snapshot = task.Result;

            levelInfo = snapshot.Child(levelID.ToString()).Child(LoaderMenuManager.Instance.GetCurrentLanguageCode()).GetRawJsonValue();
                Teste();
            }

        else if (task.IsFaulted)
        {
           // Handle the error...
        }
            
        });
    }

    private void Teste()
    {
        LoaderMenuManager.Instance.SaveDownloadedLevel(levelInfo, levelID);
    }
}


