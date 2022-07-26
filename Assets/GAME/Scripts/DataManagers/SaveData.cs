using System.IO;
using System.Text;
using UnityEngine;

public class SaveData : MonoBehaviour
{
    private string fileDirectoryPath;
    private readonly string encryptionCodeWord = "mygame";

    private void Awake() => fileDirectoryPath = Application.persistentDataPath;


    public void SaveNewData(string fileName, object objectToCreateNewData)
    {
        string fullPath = Path.Combine(fileDirectoryPath, fileName.ToString());

        string newData = JsonUtility.ToJson(objectToCreateNewData);

        newData = EncryptData(newData);

        Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

        using (FileStream stream = new FileStream(fullPath, FileMode.Create))
        {
            using (StreamWriter writer = new StreamWriter(stream))
            {
                writer.Write(newData);
            }
        }
    }

    public void SaveNewFile(string fileName, string fileData)
    {
        string fullPath = Path.Combine(fileDirectoryPath, fileName.ToString());

       // string newFile = toJsonFile == true ? _ = JsonUtility.ToJson(fileData) : fileData;

        string newFile = EncryptData(fileData);

        Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

        using (FileStream stream = new FileStream(fullPath, FileMode.Create))
        {
            using (StreamWriter writer = new StreamWriter(stream))
            {
                writer.Write(newFile);
            }
        }
    }


    public bool CheckIfFileExists(string fileName)
    {
        string fullPath = Path.Combine(fileDirectoryPath, fileName.ToString());
        return File.Exists(fullPath);
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
