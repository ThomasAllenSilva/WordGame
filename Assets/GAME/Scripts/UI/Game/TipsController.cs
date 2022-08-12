using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TipsController : MonoBehaviour
{
    private Image[] tipsUIFields;

    private Color32 completedColor = new Color32(69, 212, 93, 255);

    private void Start()
    {
        InitializeTips();
    }

    private void InitializeTips()
    {
        tipsUIFields = new Image[transform.childCount];

        for (int i = 0; i < tipsUIFields.Length; i++)
        {
            tipsUIFields[i] = transform.GetChild(i).GetComponent<Image>();
        }
    }

    public void SetTipUIFieldComplete(int indexToSetAsCompleted)
    {
        tipsUIFields[indexToSetAsCompleted].color = completedColor;
    }

    private void OnDisable()
    {
        for (int i = 0; i < tipsUIFields.Length; i++)
        {
            tipsUIFields[i].color = Color.white;
        }
    }
}
