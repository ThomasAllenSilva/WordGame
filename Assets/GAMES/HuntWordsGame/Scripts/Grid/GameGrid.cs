using UnityEngine;
using UnityEngine.UI;

public class GameGrid : MonoBehaviour
{
    protected GridLayoutGroup gridLayoutGroup;

    protected HuntWordsSO currentLevel;

    protected Font wordsFont;

    protected ObjectPool objectPooler;

    private GameManager gameManager;
    protected virtual void Awake()
    {
        gameManager = GameManager.Instance;

        objectPooler = gameManager.GetComponent<ObjectPool>();

        gridLayoutGroup = GetComponent<GridLayoutGroup>();

        currentLevel = gameManager.currentLevel;

        wordsFont = gameManager.fontStyle;

        SetGridLaoutGroupValues();
    }


    protected virtual void OnEnable()
    {
        currentLevel = gameManager.currentLevel;

        SetGridLaoutGroupValues();
    }


    protected virtual void SetGridLaoutGroupValues()
    {

    }

}
