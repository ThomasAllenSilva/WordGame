using UnityEngine;
using UnityEngine.UI;

public class CheckImageToTutorial : MonoBehaviour
{
    private Image tutorialImage;

    [SerializeField] private Sprite englishTutorialImage;
    [SerializeField] private Sprite portugueseTutorialImage;

    private void Awake() => tutorialImage = GetComponent<Image>();

    private void Start()
    {
        if(DataManager.Instance.GetCurrentGameLanguageIdentifierCode() == "en")
        {
            tutorialImage.sprite = englishTutorialImage;
        }

        else if(DataManager.Instance.GetCurrentGameLanguageIdentifierCode() == "pt")
        {
            tutorialImage.sprite = portugueseTutorialImage;
        }

        else
        {
            tutorialImage.sprite = englishTutorialImage;
        }
    }
}
