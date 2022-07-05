using UnityEngine;
using UnityEngine.UI;

public class WordToSearchFieldsController : MonoBehaviour
{
    private Image[] wordsToSearchFields;

    private Color32 completedImageColor = new Color32(69, 212, 93, 255);

    private void Start()
    {
        InitializeWordsToSearchArray();
    }

    private void InitializeWordsToSearchArray()
    {
        wordsToSearchFields = new Image[transform.childCount];

        for (int i = 0; i < wordsToSearchFields.Length; i++)
        {
            wordsToSearchFields[i] = transform.GetChild(i).GetComponent<Image>();
        }
    }

    public void SetWordUIFieldComplete(int fieldIndexToSetAsCompleted)
    {
        wordsToSearchFields[fieldIndexToSetAsCompleted].color = completedImageColor;
    }

    private void OnDisable()
    {
        for (int i = 0; i < wordsToSearchFields.Length; i++)
        {
            wordsToSearchFields[i].color = Color.white;
        }
    }
}
