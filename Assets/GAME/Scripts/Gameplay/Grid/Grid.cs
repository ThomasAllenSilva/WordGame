using UnityEngine;
using UnityEngine.UI;

public class Grid : MonoBehaviour
{
    protected GridLayoutGroup gridLayoutGroup;

    protected static GameManager gameManager;

    protected virtual void Awake()
    {
        gameManager = GameManager.Instance;

        gridLayoutGroup = GetComponent<GridLayoutGroup>();
    }
}
