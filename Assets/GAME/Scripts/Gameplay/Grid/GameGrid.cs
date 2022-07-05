using UnityEngine;
using UnityEngine.UI;


public class GameGrid : MonoBehaviour
{
    protected HuntWordsSO currentLevel;

    protected ObjectPool objectPooler;

    private GameManager gameManager;

    protected virtual void Awake()
    {
        gameManager = GameManager.Instance;

        objectPooler = gameManager.GetComponent<ObjectPool>();

        currentLevel = gameManager.CurrentLevel;    
        
    }


    protected virtual void OnEnable()
    {
        currentLevel = gameManager.CurrentLevel;
    }
}
