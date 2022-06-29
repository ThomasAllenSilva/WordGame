using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject[] gameObjects;
    
    private void Start()
    {
        GameManager.Instance.EnterNewLevel += EnableGameObjects;
        GameManager.Instance.LeaveCurrentLevel += DisableGameObjects;
    }

    private void EnableGameObjects()
    {
        for (int i = 0; i < gameObjects.Length; i++)
        {
            gameObjects[i].SetActive(true);
        }
    }

    private void DisableGameObjects()
    {
        for (int i = 0; i < gameObjects.Length; i++)
        {
            gameObjects[i].SetActive(false);
        }
    }

    private void OnDestroy()
    {
        GameManager.Instance.EnterNewLevel -= EnableGameObjects;
        GameManager.Instance.LeaveCurrentLevel -= DisableGameObjects;
    }
}
