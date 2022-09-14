using UnityEngine;

public class BackgroundImageInfo : MonoBehaviour
{
    [SerializeField] private Color backgroundColor;
    [SerializeField] private Sprite backgroundSprite;

    public Sprite GetThisBackgroundTheme()
    {
        return backgroundSprite;
    }
    public Color GetThisBackgroundColor()
    {
        return backgroundColor;
    }
}
