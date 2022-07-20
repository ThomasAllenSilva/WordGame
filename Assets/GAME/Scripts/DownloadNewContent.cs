using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using UnityEditor;
using UnityEngine.SceneManagement;
public class DownloadNewContent : MonoBehaviour
{
    private IEnumerator Start()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(1);
    }

}
