using UnityEngine;
using UnityEngine.UI;

public class WordToSearchFieldsController : MonoBehaviour
{
    private Image[] wordsUIFields;

    private Color32 completedImageColor = new Color32(69, 212, 93, 255);

    private void Awake()
    {
        wordsUIFields = new Image[transform.childCount];

        for (int i = 0; i < wordsUIFields.Length; i++)
        {
            wordsUIFields[i] = transform.GetChild(i).GetComponent<Image>();
        }
    }

    public void SetWordUIFieldComplete(int fieldToSetAsCompleted)
    {
        wordsUIFields[fieldToSetAsCompleted].color = completedImageColor;
    }

    private void OnDisable()
    {
        for (int i = 0; i < wordsUIFields.Length; i++)
        {
            wordsUIFields[i].color = Color.white;
        }
    }
}
