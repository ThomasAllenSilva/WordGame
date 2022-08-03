using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    public static ScenesManager Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }

        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(Instance.gameObject);
    }


    public void LoadMainMenuScene()
    {
        SceneManager.LoadScene(1);
    }

    //public void LoadTutorialScene()
    //{
    //    if (tutorialOperation.isDone) SceneManager.LoadScene(2);
    //}

    //public void LoadGameScene()
    //{
    //    if (gameOperation.isDone) SceneManager.LoadScene(3);
    //}
}
