using UnityEngine;
using UnityEngine.UI;


public class BoxController : MonoBehaviour
{
    protected Text letterTextFromThisBox;

    protected Image imageFromThisBox;

    protected Box[] boxesThatCanBeChecked;

    protected bool thisBoxIsChecked;
    
    protected bool canThisBoxBeSelected;

    protected bool isThisBoxCompleted;

    protected static Box theFirstBoxChecked;

    public static Box currentPrincipalBoxChecked;

    public static Box[] currentBoxesThatAreChecked = new Box[10];

    public static int indexOfAmountOfBoxesThatAreCurrentChecked = 0;

    protected virtual void Start()
    {
        letterTextFromThisBox = GetComponentInChildren<Text>();
        imageFromThisBox = GetComponent<Image>();

        GameManager.Instance.PlayerTouchControllerInfo().TouchUpEvent += ResetAllBoxValues;
    }

    protected void ChangeTheImageColorFromThisBox(Color32 newColor)
    {
        imageFromThisBox.color = newColor;
    }

    protected void SetThisBoxChecked()
    {
        OverrideCurrentPrincipalBoxChecked();

        if (!thisBoxIsChecked)
        {
            GameManager.Instance.WordCheckerInfo().AddNewLetterToWordToFill(letterTextFromThisBox.text);
            ChangeTheImageColorFromThisBox(new Color32(7, 204, 195, 255));
            thisBoxIsChecked = true;
            AddNewBoxToAllCurrentBoxThatAreCheckedList(this.GetComponent<Box>());
        }

        else
        {
            GameManager.Instance.WordCheckerInfo().RemoveTheLastLetterAddedToWordToFill();
            RemoveTheLastBoxAddedToAllCurrentBoxThatAreCheckedList();
        }
    }

    public void SetThisBoxAsCompleted(Color32 anyColorIndexFromCurrentWordGrid)
    {
        isThisBoxCompleted = true;
        ChangeTheImageColorFromThisBox(anyColorIndexFromCurrentWordGrid);
    }

    protected static void AddNewBoxToAllCurrentBoxThatAreCheckedList(Box newBox)
    {
        if (indexOfAmountOfBoxesThatAreCurrentChecked < 9) indexOfAmountOfBoxesThatAreCurrentChecked += 1;

        currentBoxesThatAreChecked[indexOfAmountOfBoxesThatAreCurrentChecked] = newBox;
        
    }

    protected static void RemoveTheLastBoxAddedToAllCurrentBoxThatAreCheckedList()
    {
        currentBoxesThatAreChecked[indexOfAmountOfBoxesThatAreCurrentChecked] = null;

        if (indexOfAmountOfBoxesThatAreCurrentChecked > 0) indexOfAmountOfBoxesThatAreCurrentChecked -= 1;
    }

    protected void OverrideCurrentPrincipalBoxChecked()
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

    protected void ResetThisBoxValues()
    {
        if (thisBoxIsChecked && !isThisBoxCompleted)
        {
            thisBoxIsChecked = false;
            imageFromThisBox.color = Color.gray;
            canThisBoxBeSelected = false;
        }
    }

    private void ResetAllBoxValues()
    {
        currentPrincipalBoxChecked = null;
        theFirstBoxChecked = null;
        ResetThisBoxValues();
        canThisBoxBeSelected = false;
        indexOfAmountOfBoxesThatAreCurrentChecked = 0;
        currentBoxesThatAreChecked = new Box[10];
    }

    private void OnDestroy()
    {
        GameManager.Instance.PlayerTouchControllerInfo().TouchUpEvent -= ResetAllBoxValues;
    }
}
