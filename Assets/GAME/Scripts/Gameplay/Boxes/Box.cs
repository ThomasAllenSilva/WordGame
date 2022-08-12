using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;


[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Image))]
public class Box : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler
{
    private Text letterFromThisBox;

    private Image imageFromThisBox;

    private Animator boxAnimator;

    private GameManager gameManager;

    private LinkedList<Box> boxesThatCanBeChecked = new LinkedList<Box>();

    private bool thisBoxIsChecked;

    private bool canThisBoxBeSelected;

    private bool isThisBoxCompleted;

    public static Box currentPrincipalBoxChecked;

    public static List<Box> currentBoxesThatAreChecked = new List<Box>(10);

    public static Box theFirstBoxChecked;

    public static int amountOfBoxesThatAreCurrentChecked = 0;

    private const int maxOfBoxesThatCanBeChecked = 9;

    private static Color32 completedColor;

    private void Awake()
    {
        EnhancedTouchSupport.Enable();

        letterFromThisBox = GetComponentInChildren<Text>();
        imageFromThisBox = GetComponent<Image>();
        boxAnimator = GetComponent<Animator>();
        gameManager = GameManager.Instance;
    }

    private void Start()
    {
        gameManager.PlayerTouchController.TouchUpEvent += ResetThisBoxValues;
        gameManager.PlayerTouchController.TouchUpEvent += SetRandomColorToCompletedBoxImageColor;
    }

    public void SetRandomColorToCompletedBoxImageColor()
    {
        completedColor = Random.ColorHSV();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!isThisBoxCompleted && Touch.activeTouches.Count <= 1)
        {
            theFirstBoxChecked = this;
            OverrideCurrentPrincipalBoxChecked();
            gameManager.WordChecker.AddLetterToWordToFill(letterFromThisBox.text);
            ChangeTheImageColorFromThisBox(new Color32(7, 204, 195, 255));
            SetThisBoxAsChecked();
            AddThisBoxToAllCurrentBoxThatAreCheckedList();
            gameObject.GetComponent<Animator>().Play("BoxSelected");
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (CanSelectThisBox())
        { 
            if (IsNotChecked())
            {
                gameManager.WordChecker.AddLetterToWordToFill(letterFromThisBox.text);
                ChangeTheImageColorFromThisBox(new Color32(7, 204, 195, 255));
                SetThisBoxAsChecked();
                AddThisBoxToAllCurrentBoxThatAreCheckedList();
                OverrideCurrentPrincipalBoxChecked();
                boxAnimator.Play("BoxSelected");
            }

            else if (IsChecked())
            {
                currentPrincipalBoxChecked.ResetThisBoxValues();
                gameManager.WordChecker.RemoveTheLastLetterAddedToWordToFill();
                RemoveTheLastBoxAddedToAllCurrentBoxThatAreCheckedList();
                OverrideCurrentPrincipalBoxChecked();
            }
        }
    }

    private bool IsNotChecked()
    {
        return !thisBoxIsChecked && amountOfBoxesThatAreCurrentChecked < maxOfBoxesThatCanBeChecked;
    }

    private bool IsChecked()
    {
        return thisBoxIsChecked && currentBoxesThatAreChecked[amountOfBoxesThatAreCurrentChecked - 2] == this;
    }

    private bool CanSelectThisBox()
    {
        return canThisBoxBeSelected && !isThisBoxCompleted && Touch.activeTouches.Count <= 1;
    }

    private void SetThisBoxAsChecked()
    {
        thisBoxIsChecked = true;
    }

    private void OverrideCurrentPrincipalBoxChecked()
    {
        if (currentPrincipalBoxChecked != null)
        {
            foreach (Box box in currentPrincipalBoxChecked.boxesThatCanBeChecked)
            {
                box.canThisBoxBeSelected = false;
            }
        }

        currentPrincipalBoxChecked = this;

        foreach (Box box in currentPrincipalBoxChecked.boxesThatCanBeChecked)
        {
            box.canThisBoxBeSelected = true;
        }
    }

    private void AddThisBoxToAllCurrentBoxThatAreCheckedList()
    {
        currentBoxesThatAreChecked.Add(this);

        if (amountOfBoxesThatAreCurrentChecked < maxOfBoxesThatCanBeChecked) amountOfBoxesThatAreCurrentChecked += 1;
    }

    private void RemoveTheLastBoxAddedToAllCurrentBoxThatAreCheckedList()
    {
        currentBoxesThatAreChecked.Remove(currentPrincipalBoxChecked);
        currentPrincipalBoxChecked.boxAnimator.Play("BoxDeselect");
        if (amountOfBoxesThatAreCurrentChecked > 0) amountOfBoxesThatAreCurrentChecked -= 1;
    }

    public void SetThisBoxAsCompleted()
    {
        isThisBoxCompleted = true;
        ChangeTheImageColorFromThisBox();
    }

    private void ChangeTheImageColorFromThisBox() => imageFromThisBox.color = completedColor;

    private void ChangeTheImageColorFromThisBox(Color32 newColor) => imageFromThisBox.color = newColor;

    public static void SetAllCurrentBoxesCheckedAsComplete()
    {
        foreach (Box box in currentBoxesThatAreChecked)
        {
            box.SetThisBoxAsCompleted();
        }

        amountOfBoxesThatAreCurrentChecked = 0;
        currentBoxesThatAreChecked.Clear();
    }

    private IEnumerator GetAllBoxesThatCanBeSelectedByThisBox()
    {
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();

        List<RaycastHit2D> hit = new List<RaycastHit2D>();
        float rayXDistance = 700f;
        float rayYDistance = 700f;


        hit.Add(Physics2D.Raycast(transform.position, new Vector2(rayXDistance, 0f)));
        hit.Add(Physics2D.Raycast(transform.position, new Vector2(-rayXDistance, 0f)));

        hit.Add(Physics2D.Raycast(transform.position, new Vector2(0f, rayYDistance)));
        hit.Add(Physics2D.Raycast(transform.position, new Vector2(0f, -rayYDistance)));

        for (int i = 0; i < hit.Count; i++)
        {
            if (hit[i].collider != null)
            {
                if (this != currentPrincipalBoxChecked)
                {
                    boxesThatCanBeChecked.AddLast(hit[i].collider.GetComponent<Box>());
                    hit[i].collider.GetComponent<Box>().canThisBoxBeSelected = false;
                }

                else if (this == currentPrincipalBoxChecked)
                {
                    boxesThatCanBeChecked.AddLast(hit[i].collider.GetComponent<Box>());
                    hit[i].collider.GetComponent<Box>().canThisBoxBeSelected = true;
                }

                else
                {
                    currentPrincipalBoxChecked = this;
                    boxesThatCanBeChecked.AddLast(hit[i].collider.GetComponent<Box>());
                    hit[i].collider.GetComponent<Box>().canThisBoxBeSelected = true;
                }
            }
        }
    }

    public static void ResetAllBoxValues()
    {
        currentPrincipalBoxChecked = null;
        theFirstBoxChecked = null;
        amountOfBoxesThatAreCurrentChecked = 0;
        currentBoxesThatAreChecked.Clear();
    }

  
    private void ResetThisBoxValues()
    {
        if (!isThisBoxCompleted)
        {
            thisBoxIsChecked = false;
            imageFromThisBox.color = Color.gray;
            canThisBoxBeSelected = false;
            boxAnimator.Play("BoxDeselect");
        }
    }

    private void ResetGame()
    {
        thisBoxIsChecked = false;
        isThisBoxCompleted = false;
        canThisBoxBeSelected = false;
        currentPrincipalBoxChecked = null;
        theFirstBoxChecked = null;
        amountOfBoxesThatAreCurrentChecked = 0;
        currentBoxesThatAreChecked.Clear();
        boxesThatCanBeChecked.Clear();
        imageFromThisBox.color = Color.gray;
        boxAnimator.Play("BoxDeselect");
    }

    private void OnEnable()
    {
        StartCoroutine(GetAllBoxesThatCanBeSelectedByThisBox());
        boxAnimator.Play("BoxFadeIn");
    }

    private void OnDisable() => ResetGame();

    private void OnDestroy() => gameManager.PlayerTouchController.TouchUpEvent -= ResetAllBoxValues;
}
