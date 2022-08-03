using System.IO;
using System.Text;
using UnityEngine;

public class LoadData : MonoBehaviour
{
    private readonly string decryptionCodeWord = "mygame";

    public string LoadFileData(string fileName)
    {
        string fullPath = Path.Combine(Application.persistentDataPath, fileName.ToString());

        using (FileStream stream = new FileStream(fullPath, FileMode.Open))
        {
            using (StreamReader reader = new StreamReader(stream))
            {
                string dataToLoad = reader.ReadToEnd();

                return DecryptData(dataToLoad);
            }
        }
    }

    private string DecryptData(string dataToDecrypt)
    {
        StringBuilder decryptedData = new StringBuilder();

        for (int i = 0; i < dataToDecrypt.Length; i++)
        {
            decryptedData.Append( (char) (dataToDecrypt[i] ^ decryptionCodeWord[i % decryptionCodeWord.Length]));
        }

        return decryptedData.ToString();
    }
}
