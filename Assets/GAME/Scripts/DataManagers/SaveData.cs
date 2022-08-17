using System.IO;
using System.Text;
using UnityEngine;

public class SaveData : MonoBehaviour
{
    private readonly string encryptionCodeWord = "mygame";

    public void SaveNewData(StringBuilder fileName, object objectToCreateNewData)
    {
        StringBuilder fileToSaveName = new StringBuilder(fileName.ToString());

        fileToSaveName.Append(DataManager.Instance.GetCurrentGameLanguageIdentifierCode());

        string fullPath = Path.Combine(Application.persistentDataPath, fileToSaveName.ToString());

        string dataToSave = JsonUtility.ToJson(objectToCreateNewData);

        dataToSave = EncryptData(dataToSave);

        Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

        using (FileStream stream = new FileStream(fullPath, FileMode.Create))
        {
            using (StreamWriter writer = new StreamWriter(stream))
            {
                writer.Write(dataToSave);
            }
        }
    }

    public void SaveNewData(StringBuilder fileName, string fileData)
    {

        fileName.Append(DataManager.Instance.GetCurrentGameLanguageIdentifierCode());

        string fullPath = Path.Combine(Application.persistentDataPath, fileName.ToString());

        string fileToSave = EncryptData(fileData);

        Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

        using (FileStream stream = new FileStream(fullPath, FileMode.Create))
        {
            using (StreamWriter writer = new StreamWriter(stream))
            {
                writer.Write(fileToSave);
            }
        }
    }

    public void SaveNewData(string fileName, object objectToCreateNewData)
    {
        string fullPath = Path.Combine(Application.persistentDataPath, fileName.ToString());

        string dataToSave = JsonUtility.ToJson(objectToCreateNewData);

        dataToSave = EncryptData(dataToSave);

        Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

        using (FileStream stream = new FileStream(fullPath, FileMode.Create))
        {
            using (StreamWriter writer = new StreamWriter(stream))
            {
                writer.Write(dataToSave);
            }
        }
    }

    private string EncryptData(string dataToEncrypt)
    {
        StringBuilder encryptedData = new StringBuilder();

        for (int i = 0; i < dataToEncrypt.Length; i++)
        {
            encryptedData.Append((char)(dataToEncrypt[i] ^ encryptionCodeWord[i % encryptionCodeWord.Length]));
        }

        return encryptedData.ToString();
    }
}
