using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]

public class AlphaTween : MonoBehaviour
{
    private CanvasGroup canvasGroup;

    private void Awake() => canvasGroup = GetComponent<CanvasGroup>();

    public void FadeInThisGameObject()
    {
        canvasGroup.alpha = 0f;
        LeanTween.alphaCanvas(canvasGroup, 1, 0.5f).setOnComplete(EnableInteractableFromThisCanvasGroup);
    }

    public void FadeOutThisGameObjectAndEnableAnotherOne(GameObject gameObjectToEnable)
    {
        LeanTween.alphaCanvas(canvasGroup, 0, 0.4f).setOnComplete(() => EnableOtherGameObject(gameObjectToEnable));
    }

    private void EnableOtherGameObject(GameObject gameObjectToEnable)
    {
        gameObjectToEnable.SetActive(true);
        DisableThisGameObject();
    }

    private void DisableThisGameObject()
    {
        this.gameObject.SetActive(false);
    }

    private void EnableInteractableFromThisCanvasGroup()
    {
        canvasGroup.interactable = true;
    }

    private void OnEnable()
    {
        FadeInThisGameObject();
    }
}
