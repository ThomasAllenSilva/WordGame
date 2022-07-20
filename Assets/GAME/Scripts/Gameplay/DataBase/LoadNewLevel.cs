using System.Text;
using UnityEngine;
using System.IO;
using UnityEngine.Localization.Settings;

public class LoadNewLevel : MonoBehaviour
{
    private string fileDirectoryPath;
    private StringBuilder fileName = new StringBuilder();
    private readonly string encryptionCodeWord = "mygame";

    private void Awake()
    {
        fileDirectoryPath = Application.persistentDataPath;
    }

    public void LoadLevel(int levelID)
    {
        fileName.Append("level");
        fileName.Append(levelID);
        fileName.Append(LocalizationSettings.SelectedLocale.Identifier.Code);

        string fullPath = Path.Combine(fileDirectoryPath, fileName.ToString());

        if (File.Exists(fullPath))
        {
            string dataToLoad = "";
            using(FileStream stream = new FileStream(fullPath, FileMode.Open))
            {
                using(StreamReader reader = new StreamReader(stream))
                {
                    dataToLoad = reader.ReadToEnd();
                }
            }

            dataToLoad = DecryptData(dataToLoad);
            JsonUtility.FromJsonOverwrite(dataToLoad, GameManager.Instance.CurrentLevel);
            GameManager.Instance.CurrentLevel.LoadNewLevelDataInfo();
        }
    }

    private string DecryptData(string data)
    {
        StringBuilder encryptedData = new StringBuilder();

        for (int i = 0; i < data.Length; i++)
        {
            encryptedData.Append((char)(data[i] ^ encryptionCodeWord[i % encryptionCodeWord.Length]));
        }

        return encryptedData.ToString();
    }
}
