using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    public PlayerTouchController PlayerTouchController { get; private set; }

    public WordChecker WordChecker { get; private set; }

    public DataManager DataManager { get; private set; }

    public LevelManager LevelManager { get; private set; }

    public AudioManager AudioManager { get; private set; }

    private void Awake()
    {
        if (Instance == null) Instance = this;

        else
        {
            Destroy(gameObject);
            throw new Exception("There's already a GameManager in the scene, can't exist more than one");
        }

        PlayerTouchController = transform.GetComponentInChildren<PlayerTouchController>();
        WordChecker = transform.GetComponentInChildren<WordChecker>();
        LevelManager = transform.GetComponentInChildren<LevelManager>();

        DataManager = DataManager.Instance;
        AudioManager = AudioManager.Instance;
    }
}
