using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject[] gameObjects;

    private LoadNewLevel loaderController;

    private void Awake()
    {
        loaderController = GetComponent<LoadNewLevel>();
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
}
