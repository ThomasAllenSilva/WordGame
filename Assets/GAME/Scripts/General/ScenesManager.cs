using UnityEngine;
using UnityEngine.SceneManagement;

using System;
using System.Threading.Tasks;

public class ScenesManager : MonoBehaviour
{
    public static ScenesManager Instance { get; private set; } 

    public event Action onGameSceneLoaded;
    public event Action onFirebaseSceneLoaded;
    public event Action onMainMenuSceneLoaded;
    public event Action onTutorialSceneLoaded;
    public event Action onAnySceneLoaded;

    public int LoadedSceneIndex { get; private set; }

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

        SceneManager.sceneLoaded += OnAnySceneLoaded;

        DontDestroyOnLoad(Instance.gameObject);
    }

    private void OnAnySceneLoaded(Scene scene, LoadSceneMode mode)
    {
        LoadedSceneIndex = scene.buildIndex;

        onAnySceneLoaded?.Invoke();

        switch (LoadedSceneIndex)
        {
            case 0:
                onFirebaseSceneLoaded?.Invoke();
                break;

            case 1:
                onMainMenuSceneLoaded?.Invoke();
                break;

            case 2:
                onTutorialSceneLoaded?.Invoke();
                break;

            case 3:
                onGameSceneLoaded?.Invoke();
                break;
        }
    }

    public void LoadSceneByIndex(int index)
    {
        SceneManager.LoadScene(index);
    }
}
