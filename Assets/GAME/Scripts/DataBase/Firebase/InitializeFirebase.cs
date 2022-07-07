using System;
using System.IO;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Extensions;

public class InitializeFirebase : MonoBehaviour
{
    private DatabaseReference databaseReference;

    void Awake()
    {
        Uri dataBaseUrl = new Uri("https://huntwordslevels-default-rtdb.firebaseio.com/");
        FirebaseApp.DefaultInstance.Options.DatabaseUrl = dataBaseUrl;

        InitializeDataBase();

        ReadDataBaseLevelValues(1);
    }

    private void InitializeDataBase()
    {
            FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
            var dependencyStatus = task.Result;

            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {
                databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
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
        FirebaseDatabase.DefaultInstance.GetReference("Levels").GetValueAsync().ContinueWithOnMainThread(task => {
          if (task.IsCompleted)
          {
              DataSnapshot snapshot = task.Result;

                string levelValues = snapshot.Child(levelID.ToString()).GetRawJsonValue();

                Level level = JsonUtility.FromJson<Level>(levelValues);
                print(level.amountOfColumns);

                print(level.amountOfLines);

                for (int i = 0; i < level.lettersFromLines.Length; i++)
                {
                    print(level.lettersFromLines[i]);
                    print("a");
                }

            }

            else if (task.IsFaulted)
          {
              // Handle the error...
          }

      });
    }
}

public class Level
{
    public int amountOfColumns;
    public int amountOfLines;
    public string[] lettersFromLines;

    public int amountOfWordsToSearch;
    public string[] wordsToSearch;

    public int amountOfTips;
    public string[] tips;

    //TODO: Solve this later xD
    public int[] otherGridValues;

    public Level(int columns, int lines, string letters, int wordsToSearchAmount, string[] wordsToSearch, int tipsAmount, string[] tips, int[] gridValues)
    {
        amountOfColumns = columns;
        amountOfLines = lines;
        lettersFromLines = new string[amountOfColumns * amountOfLines];

        string[] teste = letters.Split(" ");
        for (int i = 0; i < lettersFromLines.Length; i++)
        {
            lettersFromLines[i] = teste[i];
        }

        amountOfWordsToSearch = wordsToSearchAmount;
        this.wordsToSearch = wordsToSearch;

        amountOfTips = tipsAmount;
        this.tips = tips.ToString().Split(";");

        otherGridValues = gridValues;
    }
}

