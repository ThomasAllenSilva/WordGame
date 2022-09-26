using System;
using UnityEngine;

public class Background : MonoBehaviour
{
    private Camera mainCamera;

    private Color currentBackgroundColor;

    private Color transparentColor = new Color(0f,0f,0f, 0f);

    private Color defaultBackgroundColor = new Color(255, 255, 255, 255);

    private SpriteRenderer backgroundSpriteRenderer;

    public static Background Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(Instance.gameObject);
        backgroundSpriteRenderer = GetComponent<SpriteRenderer>();

    }

    private void Start()
    {
        ScenesManager.Instance.onMainMenuSceneLoaded += GetCurrentSelectedBackgroundTheme;
        ScenesManager.Instance.onAnySceneLoaded += UpdateMainCameraBackgroundColor;
    }

    public void ChangeBackgroundTheme(Color backgroundColor, Sprite background)
    {
        backgroundSpriteRenderer.sprite = background;
        currentBackgroundColor = backgroundColor;
        UpdateMainCameraBackgroundColor();
    }

    private void UpdateMainCameraBackgroundColor()
    {
        mainCamera = Camera.main;
        backgroundSpriteRenderer.color = defaultBackgroundColor;
        mainCamera.backgroundColor = currentBackgroundColor;
    }

    private void GetCurrentSelectedBackgroundTheme()
    {
        BackgroundImageInfo selectedBackgroundTheme = FindObjectOfType<BackgroundsShopCanvasManager>(true).gameObject.transform.GetChild(DataManager.Instance.PlayerDataManager.CurrentSelectedBackground).GetComponentInChildren<BackgroundImageInfo>();
        ChangeBackgroundTheme(selectedBackgroundTheme.GetThisBackgroundColor(), selectedBackgroundTheme.GetThisBackgroundTheme());
    }
}
