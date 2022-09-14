using UnityEngine;
using UnityEngine.EventSystems;

public class Background : MonoBehaviour
{
    private Camera mainCamera;

    [SerializeField] private Color currentBackgroundColor;

    private Color transparentColor = new Color(0f,0f,0f, 0f);

    private Color defaultBackgroundColor = new Color(255, 255, 255, 255);

    public static Background Instance;

    private SpriteRenderer backgroundSpriteRenderer;

    private GameObject shopContent;

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

    public void ChangeBackgroundTheme(Color backgroundColor, Sprite background)
    {
        backgroundSpriteRenderer.sprite = background;
        currentBackgroundColor = backgroundColor;
        UpdateMainCameraBackgroundColor();
    }

    private void UpdateMainCameraBackgroundColor()
    {
        mainCamera.backgroundColor = currentBackgroundColor;
    }

    private void OnLevelWasLoaded(int level)
    {
        if (level != 0)
        {

            backgroundSpriteRenderer.color = defaultBackgroundColor;
            mainCamera = Camera.main;
            UpdateMainCameraBackgroundColor();
            if (level == 1)
            {
                shopContent = GameObject.Find("Content");

                BackgroundImageInfo selectedBackgroundTheme = shopContent.transform.GetChild(DataManager.Instance.PlayerDataManager.PlayerData.selectedBackground).GetComponentInChildren<BackgroundImageInfo>();
                ChangeBackgroundTheme(selectedBackgroundTheme.GetThisBackgroundColor(), selectedBackgroundTheme.GetThisBackgroundTheme());
            }

        }

        else
        {
            backgroundSpriteRenderer.color = transparentColor;
        }
    }
}
