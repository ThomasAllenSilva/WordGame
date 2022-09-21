using UnityEngine;

public class ScaleTween : MonoBehaviour
{
    [SerializeField] private Vector2 scaleSizeOnEnable;

    public void ScaleToZeroAndDisableThisGameObject()
    {
        LeanTween.scale(gameObject, Vector2.zero, 0.2f).setOnComplete(DisableThisGameObject);
    }

    public void ScaleToZeroAndDisableThisGameObjectParent()
    {
        LeanTween.scale(gameObject, Vector2.zero, 0.2f).setOnComplete(DisableThisGameObjectParent);
    }

    public void ScaleToZeroAndDestroyThisGameObjectParent()
    {
        LeanTween.scale(gameObject, Vector2.zero, 0.2f).setOnComplete(DestroyThisGameObjectParent);
    }

    public void ScaleToZeroAndEnableOtherGameObject(GameObject gameObjectToEnable)
    {
        LeanTween.scale(gameObject, Vector2.zero, 0.2f).setOnComplete(() => EnableGameObject(gameObjectToEnable));
    }

    private void DisableThisGameObject()
    {
        this.gameObject.SetActive(false);
    }

    private void DisableThisGameObjectParent()
    {
        this.gameObject.transform.parent.gameObject.SetActive(false);
    }

    private void EnableGameObject(GameObject gameObjectToEnable)
    {
        gameObjectToEnable.SetActive(true);
    }

    private void DestroyThisGameObjectParent()
    {
        Destroy(transform.parent.gameObject);
    }

    private void OnEnable()
    {
        LeanTween.scale(gameObject, scaleSizeOnEnable, 0.2f);
    }
}
    