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
            PlayerTouchController.Instance.isTouchingTheScreen = true;
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
        return PlayerTouchController.Instance.isTouchingTheScreen && canThisBoxBeSelected && !isLetterCompleted && !thisBoxIsChecked && amountOfBoxesThatAreChecked < 9;
    }

    private bool IsCheckedAndCanSelectThisBox()
    {
        return PlayerTouchController.Instance.isTouchingTheScreen && canThisBoxBeSelected && !isLetterCompleted && thisBoxIsChecked && this == allCurrentBoxesThatAreChecked[amountOfBoxesThatAreChecked - 1];
    }

    private IEnumerator GetDirectionalBoxes()
    {
        
        yield return new WaitForEndOfFrame();

        yield return new WaitForEndOfFrame();

        RaycastHit2D[] hit = new RaycastHit2D[4];
        float rayXDistance = 100f;
        float rayYDistance = 100f;


        hit[0] = Physics2D.Raycast(transform.position, new Vector2(rayXDistance, 0f));
        hit[1] = Physics2D.Raycast(transform.position, new Vector2(-rayXDistance, 0f));

        hit[2] = Physics2D.Raycast(transform.position, new Vector2(0f, rayYDistance));
        hit[3] = Physics2D.Raycast(transform.position, new Vector2(0f, -rayYDistance));

        boxesThatCanBeChecked = new Box[hit.Length];

        for (int i = 0; i < hit.Length; i++)
        {
         

            if (hit[i].collider != null)
            {
                boxesThatCanBeChecked[i] = hit[i].collider.GetComponent<Box>();

                if (this != BoxController.currentPrincipalBoxChecked)
                {
                    boxesThatCanBeChecked[i] = hit[i].collider.GetComponent<Box>();
                    hit[i].collider.GetComponent<Box>().canThisBoxBeSelected = false;
                }

                else if (this == BoxController.currentPrincipalBoxChecked)
                {
                    boxesThatCanBeChecked[i] = hit[i].collider.GetComponent<Box>();
                    hit[i].collider.GetComponent<Box>().canThisBoxBeSelected = true;
                }
            }


        }

    }

    private void OnDrawGizmos()
    {
        float rayXDistance = 100f;
        float rayYDistance = 100f;

        Gizmos.DrawRay(transform.position, new Vector2(rayXDistance, 0f));
        Gizmos.DrawRay(transform.position, new Vector2(-rayXDistance, 0f));


        Gizmos.DrawRay(transform.position, new Vector2(0f, rayYDistance));
        Gizmos.DrawRay(transform.position, new Vector2(0f, -rayYDistance));

    }
}
