using UnityEngine;

public class BoxScaleTween : MonoBehaviour
{
    [SerializeField] private Vector2 defaultBoxScaleSize;
    [SerializeField] private Vector2 selectedBoxScaleSize;

    public void SetThisBoxSizeToDefaultValue()
    {
        LeanTween.scale(gameObject, defaultBoxScaleSize, 0f);
    }

    public void SetThisBoxSizeAsSelectedSize()
    {
        LeanTween.scale(gameObject, selectedBoxScaleSize, 0f);
    }

    private void OnEnable()
    {
        SetThisBoxSizeToDefaultValue();
    }
}
