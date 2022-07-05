using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using System.IO;
public class DownloadNewContent : MonoBehaviour
{
    [SerializeField] private string contentURL;

    private IEnumerator Start()
    {
        UnityWebRequest www = UnityWebRequestAssetBundle.GetAssetBundle(contentURL);

        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(www);

            TextAsset levelAsset = bundle.LoadAsset("Level2.txt") as TextAsset;


            CreateNewLevel(levelAsset);

        }
    }

    private void CreateNewLevel(TextAsset levelAsset)
    {
        HuntWordsSO newLevel = HuntWordsSO.CreateInstance<HuntWordsSO>();

        string contents = levelAsset.ToString();

        string[] levelInfo = contents.Split("\n");

        int currentLineIndex = 0;

        newLevel.numberOfColumns = int.Parse(levelInfo[currentLineIndex]);
        currentLineIndex++;

        newLevel.numberOfLines = int.Parse(levelInfo[currentLineIndex]);
        currentLineIndex++;

        string[] words = new string[newLevel.numberOfColumns * newLevel.numberOfLines];
        newLevel.CreateNewColum();

        for (int i = 3; i < 10 + 3; i++)
        {
            words[i - 3] = levelInfo[i];
            currentLineIndex++;
        }




        currentLineIndex++;
        int currentLine = 0;
   
        for (int i = 0; i < newLevel.numberOfColumns; i++)
        {
            for (int j = 0; j < newLevel.numberOfLines; j++)
            {
                newLevel.columns[i].letterOnThisColum[j] = words[currentLine];
                currentLine++;
            }
        }








        newLevel.wordsToSearchInThisLevel = new List<string>();

        currentLineIndex++;

        for (int i = currentLineIndex; i < int.Parse(levelInfo[currentLineIndex - 1]) + currentLineIndex; i++)
        {
            newLevel.wordsToSearchInThisLevel.Add(levelInfo[i].ToString());

        }

        for (int i = 0; i < newLevel.wordsToSearchInThisLevel.Count; i++)
        {
            currentLineIndex++;
            print(newLevel.wordsToSearchInThisLevel[i]);
        }

        newLevel.tipsFromThisLevel = new List<string>(int.Parse(levelInfo[currentLineIndex]));
        currentLineIndex++;
        for (int i = currentLineIndex; i < int.Parse(levelInfo[currentLineIndex - 1]) + currentLineIndex; i++)
        {
            newLevel.tipsFromThisLevel.Add(levelInfo[i].ToString());
        }

        for (int i = 0; i < newLevel.tipsFromThisLevel.Count; i++)
        {
            currentLineIndex++;
        }

        newLevel.gameGridConfiguration.paddingLeft = int.Parse(levelInfo[currentLineIndex++]);

        newLevel.gameGridConfiguration.paddingTop = int.Parse(levelInfo[currentLineIndex++]);


        newLevel.gameGridConfiguration.cellSize.x = int.Parse(levelInfo[currentLineIndex++]);
        newLevel.gameGridConfiguration.cellSize.y = int.Parse(levelInfo[currentLineIndex++]);

        newLevel.gameGridConfiguration.spacing.x = int.Parse(levelInfo[currentLineIndex++]);
        newLevel.gameGridConfiguration.spacing.y = int.Parse(levelInfo[currentLineIndex++]);
        AssetDatabase.CreateAsset(newLevel, "Assets/GAME/Levels/NewLevel.asset");
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
}
