using UnityEngine;
using UnityEngine.SceneManagement;

using System;

public class ScenesManager : MonoBehaviour
{
    public static ScenesManager Instance;

    public Action onFirebaseSceneLoaded;

    public Action onSceneLoaded;

    public int CurrentSceneIndex { get; private set; }

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

        SceneManager.sceneLoaded += OnFirebaseSceneLoaded;
        SceneManager.sceneLoaded += OnSceneLoaded;

        DontDestroyOnLoad(Instance.gameObject);
    }

    private void OnFirebaseSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene.buildIndex == 0)
        {
            onFirebaseSceneLoaded?.Invoke();
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        CurrentSceneIndex = scene.buildIndex;

        onSceneLoaded?.Invoke();
    }

    public void LoadFirebaseMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadMainMenuScene()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadTutorialScene()
    {
        SceneManager.LoadScene(2);
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene(3);
    }

    public void LoadSceneByIndex(int index)
    {
        SceneManager.LoadScene(index);
    }
}
