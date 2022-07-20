using UnityEngine;
using System.Text;
using System.IO;

public class SaveNewLevel : MonoBehaviour
{
    private string fileDirectoryPath;
    private StringBuilder fileName = new StringBuilder();
    private readonly string encryptionCodeWord = "mygame";

    private void Awake() => fileDirectoryPath = Application.persistentDataPath;

    public void SaveNewLevelData(string levelValues, int levelID)
    {
        fileName.Append("level");
        fileName.Append(levelID);
        fileName.Append(LoaderMenuManager.Instance.GetCurrentLanguageCode());

        levelValues = EncryptData(levelValues);
        string fullPath = Path.Combine(fileDirectoryPath, fileName.ToString());
        print(fileName.ToString());
        if (!File.Exists(fullPath))
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(levelValues);
                }
            }
        }
    }

    private string EncryptData(string dataToEncrypt)
    {
        StringBuilder encryptedData = new StringBuilder();

        for (int i = 0; i < dataToEncrypt.Length; i++)
        {
            encryptedData.Append((char) (dataToEncrypt[i] ^ encryptionCodeWord[i % encryptionCodeWord.Length]));
        }

        return encryptedData.ToString();
    }

}
