using System.Text;

using UnityEngine;

public class FirebaseDataManager : MonoBehaviour
{
    private StringBuilder fireBaseLocalFileName = new StringBuilder("FireBaseData");

    private DataManager dataManager;

    private FirebaseData firebaseData;

    public int CurrentFirebaseBaseLevelIDNumber { get { return firebaseData.currentFireBaseLevelIDNumber; } private set { firebaseData.currentFireBaseLevelIDNumber = value; } }

    private void Awake()
    {
        dataManager = DataManager.Instance;
    }

    private void Start()
    {
        InitializeFirebaseData();

        ScenesManager.Instance.onFirebaseSceneLoaded += InitializeFirebaseData;
    }

    private void InitializeFirebaseData()
    {
        firebaseData = new FirebaseData();

        if (CheckIfFirebaseDataExists())
        {
            LoadFirebaseData();
        }

        else
        {
            CreateNewFirebaseData();

            LoadFirebaseData();
        }
    }

    public bool CheckIfFirebaseDataExists()
    {
        fireBaseLocalFileName.Append(dataManager.GetCurrentGameLanguageIdentifierCode());

        bool fileExist = dataManager.CheckIfFileExists(fireBaseLocalFileName.ToString());

        fireBaseLocalFileName.Clear();
        fireBaseLocalFileName.Append("FireBaseData");

        return fileExist;
    }

    private void CreateNewFirebaseData()
    {
        dataManager.SaveDataManager.SaveNewData(fireBaseLocalFileName, firebaseData);
    }

    private void LoadFirebaseData()
    {
        fireBaseLocalFileName.Append(dataManager.GetCurrentGameLanguageIdentifierCode());

        string fireBaseLocalDataInfo = dataManager.LoadDataManager.LoadFileData(fireBaseLocalFileName.ToString());

        fireBaseLocalFileName.Clear();
        fireBaseLocalFileName.Append("FireBaseData");

        JsonUtility.FromJsonOverwrite(fireBaseLocalDataInfo, firebaseData);
    }


    public void IncreaseCurrentFireBaseLevelNumber()
    {
        CurrentFirebaseBaseLevelIDNumber++;
        SaveFirebaseData();
    }

    private void SaveFirebaseData()
    {
        dataManager.SaveDataManager.SaveNewData(fireBaseLocalFileName, firebaseData);
    }

    public void ResetFirebaseData()
    {
        CurrentFirebaseBaseLevelIDNumber = 1;
        SaveFirebaseData();
    }

    private class FirebaseData
    {
        public int currentFireBaseLevelIDNumber = 1;
    }

    private void OnDestroy()
    {
        ScenesManager.Instance.onFirebaseSceneLoaded -= InitializeFirebaseData;
    }
}
