using UnityEngine;
using UnityEngine.UI;

public class WordToSearchFieldsController : MonoBehaviour
{
    private static Image[] wordsUIFields;

    private static Color32 completedImageColor = new Color32(69, 212, 93, 255);


    private void Start()
    {
        wordsUIFields = new Image[transform.childCount];

        for (int i = 0; i < wordsUIFields.Length; i++)
        {
            wordsUIFields[i] = transform.GetChild(i).GetComponent<Image>();
        }
    }

    public static void SetWordUIFieldComplete(int fieldIndex)
    {
        wordsUIFields[fieldIndex].color = completedImageColor;
    }
}
