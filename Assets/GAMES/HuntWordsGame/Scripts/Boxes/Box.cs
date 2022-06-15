using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class Box : BoxController, IPointerDownHandler, IPointerEnterHandler
{
    protected override void Start()
    {
        base.Start();
        StartCoroutine(GetAllBoxesThatCanBeSelectedByThisBox());
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!isThisBoxCompleted && Touch.activeTouches.Count <= 1)
        {
            GameManager.Instance.PlayerTouchControllerInfo().PlayerIsTouchingTheScreen();
            theFirstBoxChecked = this;
            SetThisBoxChecked();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(IsNotCheckedAndCanSelectThisBox())
        {
            SetThisBoxChecked();
        }

        else if (IsCheckedAndCanSelectThisBox())
        {
            currentPrincipalBoxChecked.ResetThisBoxValues();
            SetThisBoxChecked();
        }
    }

    private bool IsNotCheckedAndCanSelectThisBox()
    {
        return CanSelectThisBox() && !thisBoxIsChecked && indexOfAmountOfBoxesThatAreCurrentChecked < 9;
    }

    private bool IsCheckedAndCanSelectThisBox()
    {
        return CanSelectThisBox() && thisBoxIsChecked && this == currentBoxesThatAreChecked[indexOfAmountOfBoxesThatAreCurrentChecked - 1];
    }

    private bool CanSelectThisBox()
    {
        return GameManager.Instance.PlayerTouchControllerInfo().IsTouchingTheScreen && canThisBoxBeSelected && !isThisBoxCompleted && Touch.activeTouches.Count <= 1;
    }

    private IEnumerator GetAllBoxesThatCanBeSelectedByThisBox()
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

    private void OnEnable()
    {
        EnhancedTouchSupport.Enable();
    }

}
