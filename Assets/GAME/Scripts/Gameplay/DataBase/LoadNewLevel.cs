using System.Text;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class LoadNewLevel : MonoBehaviour
{
    private StringBuilder fileName = new StringBuilder("level");

    public void LoadNextLevel()
    {
        int nextLevelID = DataManager.Instance.GameDataManager.GameDataValues.CurrentGameLevel + 1;
        fileName.Append(nextLevelID);
        fileName.Append(LocalizationSettings.SelectedLocale.Identifier.Code);

        string dataToLoad = DataManager.Instance.LoadDataManager.LoadFileData(fileName.ToString());

        JsonUtility.FromJsonOverwrite(dataToLoad, GameManager.Instance.CurrentLevel);
        GameManager.Instance.CurrentLevel.LoadNewLevelDataInfo();
    }
}
