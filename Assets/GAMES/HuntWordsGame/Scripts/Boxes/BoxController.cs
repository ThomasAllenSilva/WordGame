using UnityEngine;
using UnityEngine.UI;


public class BoxController : MonoBehaviour
{
    protected Text boxChildText;

    protected Image boxImage;

    protected Box[] boxesThatCanBeChecked = new Box[8];

    protected bool thisBoxIsChecked;
    
    protected bool canThisBoxBeSelected;

    public bool isLetterCompleted;

    public static int amountOfBoxesThatAreChecked = 0;

    public static Box currentPrincipalBoxChecked;

    public static Box theFirstBoxChecked;

    public static Box[] allCurrentBoxesThatAreChecked = new Box[10];

    protected virtual void Start()
    {
        boxChildText = GetComponentInChildren<Text>();
        boxImage = GetComponent<Image>();

        PlayerTouchController.TouchUp += ResetAllBoxValues;
    }

    protected void ChangeBoxColor(Color newColor)
    {
        boxImage.color = newColor;
    }

    protected void SetThisBoxChecked()
    {
        OverrideCurrentPrincialBoxChecked();
        ChangeBoxColor(new Color32(7, 204, 195, 255));
        thisBoxIsChecked = true;
    }

    public void SetThisLetterAsCompleted(Color32 anyColorIndexFromCurrentWordGrid)
    {
        isLetterCompleted = true;
        ChangeBoxColor(anyColorIndexFromCurrentWordGrid);
    }

    protected static void NewBoxChecked(Box newBox)
    {
        amountOfBoxesThatAreChecked += 1;

        if (amountOfBoxesThatAreChecked >= 9) amountOfBoxesThatAreChecked = 9;


        if (amountOfBoxesThatAreChecked < allCurrentBoxesThatAreChecked.Length)
        {
            allCurrentBoxesThatAreChecked[amountOfBoxesThatAreChecked] = newBox;
        }
    }

    protected static void RemoveNewBoxChecked()
    {
        allCurrentBoxesThatAreChecked[amountOfBoxesThatAreChecked] = null;

        amountOfBoxesThatAreChecked -= 1;

        if (amountOfBoxesThatAreChecked <= 0) amountOfBoxesThatAreChecked = 0;
    }

    protected void OverrideCurrentPrincialBoxChecked()
    {
        if (currentPrincipalBoxChecked != null)
        {
            for (int i = 0; i < boxesThatCanBeChecked.Length; i++)
            {
                if (currentPrincipalBoxChecked.boxesThatCanBeChecked[i] != null)
                {
                    currentPrincipalBoxChecked.boxesThatCanBeChecked[i].canThisBoxBeSelected = false;
                }
            }
        }

        currentPrincipalBoxChecked = this.GetComponent<Box>();

        for (int i = 0; i < boxesThatCanBeChecked.Length; i++)
        {
            if (currentPrincipalBoxChecked.boxesThatCanBeChecked[i] != null)
            {
                 currentPrincipalBoxChecked.boxesThatCanBeChecked[i].canThisBoxBeSelected = true;
            }
        }

    }

    protected void ResetBoxValues()
    {
        if (thisBoxIsChecked && !isLetterCompleted)
        {
            thisBoxIsChecked = false;
            boxImage.color = Color.gray;
            canThisBoxBeSelected = false;
        }
    }

    private void ResetAllBoxValues()
    {
        currentPrincipalBoxChecked = null;
        theFirstBoxChecked = null;
        ResetBoxValues();
        canThisBoxBeSelected = false;
        amountOfBoxesThatAreChecked = 0;

        for (int i = 0; i < allCurrentBoxesThatAreChecked.Length; i++)
        {
            allCurrentBoxesThatAreChecked[i] = null;
        }
    }
}
