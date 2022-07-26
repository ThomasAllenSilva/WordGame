using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public HuntWordsSO CurrentLevel;
    
    public PlayerTouchController PlayerTouchController { get; private set; }

    public WordChecker WordChecker { get; private set; }

    public LoadNewLevel LoaderManager;

    private void Awake()
    {
        if (Instance == null) Instance = this;

        else
        {
            Destroy(gameObject);
            throw new Exception("There's already a GameManager in the scene, can't exist more than one");
        }

        Application.targetFrameRate = 60;

        PlayerTouchController = transform.GetComponentInChildren<PlayerTouchController>();
        WordChecker = transform.GetComponentInChildren<WordChecker>();
        LoaderManager = transform.GetComponentInChildren<LoadNewLevel>();    
    }

    private void Start()
    {
        LoaderManager.LoadNextLevel();
    }
}
