using System.Text;

using UnityEngine;

public class FirebaseLocalData : MonoBehaviour
{
    private StringBuilder fireBaseLocalFileName = new StringBuilder("FireBaseData");

    private DataManager dataManager;

    private FireBaseLocalData fireBaseLocalData = new FireBaseLocalData();

    private FirebaseRealTimeDataBaseReader firebaseReader;

    public int CurrentFirebaseBaseLevelIDNumber { get { return fireBaseLocalData.currentFireBaseLevelIDNumber; } private set { fireBaseLocalData.currentFireBaseLevelIDNumber = value; } }

    private void Awake()
    {
        firebaseReader = GetComponent<FirebaseRealTimeDataBaseReader>();
        dataManager = DataManager.Instance;
    }

    private void Start() => InitializeFirebase();

    private void InitializeFirebase()
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

        firebaseReader.InitializeFirebaseRealTimeDataBase();
    }


    public void IncreaseCurrentFireBaseLevelNumber()
    {
        CurrentFirebaseBaseLevelIDNumber++;
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

    private class FireBaseLocalData
    {
        public int currentFireBaseLevelIDNumber = 1;
    }
}
