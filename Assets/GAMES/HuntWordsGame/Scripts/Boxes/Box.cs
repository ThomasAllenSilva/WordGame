using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems; 

public class Box : BoxController, IPointerDownHandler, IPointerEnterHandler
{

    protected override void Start()
    {
        base.Start();
        StartCoroutine(GetDirectionalBoxes());
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!isLetterCompleted)
        {
            PlayerTouch.Instance.isTouchingTheScreen = true;
            WordChecker.Instance.AddLetterToWordToFill(boxChildText.text);
            theFirstBoxChecked = this;
            SetThisBoxChecked();
            NewBoxChecked(this);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(IsNotCheckedAndCanSelectThisBox())
        {
            SetThisBoxChecked();
            NewBoxChecked(this);
            WordChecker.Instance.AddLetterToWordToFill(boxChildText.text);
        }

        else if (IsCheckedAndCanSelectThisBox())
        {
            WordChecker.Instance.RemoveTheLastLetterFromWordToFill();
            currentPrincipalBoxChecked.ResetBoxValues();
            SetThisBoxChecked();
            RemoveNewBoxChecked();
        }
    }

    private bool IsNotCheckedAndCanSelectThisBox()
    {
        return PlayerTouch.Instance.isTouchingTheScreen && canThisBoxBeSelected && !isLetterCompleted && !thisBoxIsChecked && amountOfBoxesThatAreChecked < 9;
    }

    private bool IsCheckedAndCanSelectThisBox()
    {
        return PlayerTouch.Instance.isTouchingTheScreen && canThisBoxBeSelected && !isLetterCompleted && thisBoxIsChecked && this == allCurrentBoxesThatAreChecked[amountOfBoxesThatAreChecked - 1];
    }

    private IEnumerator GetDirectionalBoxes()
    {

        yield return new WaitForEndOfFrame();

        yield return new WaitForEndOfFrame();

        yield return new WaitForEndOfFrame();

        RaycastHit2D[] hit = new RaycastHit2D[8];
        float rayXDistance = 75f;
        float rayYDistance = 75f;


        hit[4] = Physics2D.Raycast(transform.position + new Vector3(100f, 0f), new Vector2(rayXDistance, 0f));
        hit[5] = Physics2D.Raycast(transform.position + new Vector3(-100f, 0f), new Vector2(-rayXDistance, 0f));

        hit[6] = Physics2D.Raycast(transform.position + new Vector3(0f, 100f), new Vector2(0f, rayYDistance));
        hit[7] = Physics2D.Raycast(transform.position + new Vector3(0f, -100f), new Vector2(0f, -rayYDistance));


        for (int i = 0; i < hit.Length; i++)
        {
            if (hit[i].collider != null && this != BoxController.currentPrincipalBoxChecked)
            {
                boxesThatCanBeChecked[i] = hit[i].collider.GetComponent<Box>();
                hit[i].collider.GetComponent<Box>().canThisBoxBeSelected = false;
            }

            else if (hit[i].collider != null && this == BoxController.currentPrincipalBoxChecked)
            {
                boxesThatCanBeChecked[i] = hit[i].collider.GetComponent<Box>();
                hit[i].collider.GetComponent<Box>().canThisBoxBeSelected = true;
            }
        }

    }
}
