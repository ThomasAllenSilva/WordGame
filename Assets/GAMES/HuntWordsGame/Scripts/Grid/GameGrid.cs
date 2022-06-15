using UnityEngine;
using UnityEngine.UI;

public class GameGrid : MonoBehaviour
{
    protected GridLayoutGroup gridLayoutGroup;

    protected Transform gameObjectParent;

    [SerializeField] protected HuntWordsSO currentHuntWordsLevel;

    [SerializeField] protected Font wordsFont;

    protected virtual void Awake()
    {
        gridLayoutGroup = GetComponent<GridLayoutGroup>();

        gameObjectParent = this.gameObject.transform;

        SetGridLaoutGroupValues();

        Destroy(gridLayoutGroup, 1f);
    }


    protected virtual void SetGridLaoutGroupValues()
    {

    }
}
