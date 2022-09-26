using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

using TMPro;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Image))]
public class Box : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler
{
    private TextMeshProUGUI letterFromThisBox;

    private Image imageFromThisBox;
    
    private BoxScaleTween boxScaleTween;

    private LinkedList<Box> boxesThatCanBeChecked = new LinkedList<Box>();

    private bool thisBoxIsChecked;

    private bool canThisBoxBeSelected;

    private bool isThisBoxCompleted;

    private static Box currentPrincipalBoxChecked;

    private static List<Box> currentBoxesThatAreChecked = new List<Box>(10);

    private static Box theFirstBoxChecked;

    private static GameManager gameManager;

    private static int amountOfBoxesThatAreCurrentChecked = 0;

    private static readonly int maxOfBoxesThatCanBeChecked = 20;

    private static Color32 completedColor = new Color32(178, 178, 178, 255);

    private static Action onResetedGame;

    private void Awake()
    {
        EnhancedTouchSupport.Enable();

        gameManager = GameManager.Instance;
    }

    private void Start()
    {
        letterFromThisBox = GetComponentInChildren<TextMeshProUGUI>();
        imageFromThisBox = GetComponent<Image>();
        boxScaleTween = GetComponent<BoxScaleTween>();
    }


    public static void SetRandomColorToCompletedBoxImageColor()
    {
        Color32 newRandomColor = UnityEngine.Random.ColorHSV();

        if (!completedColor.Compare(newRandomColor))
        {
            completedColor = newRandomColor;
        }
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
            ChangeThisBoxScaleToSelectedSize();
            PlaySelectedBoxSound();
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
                ChangeThisBoxScaleToSelectedSize();
                PlaySelectedBoxSound();
            }

            else if (IsChecked())
            {
                currentPrincipalBoxChecked.SetThisBoxValuesToDefault();
                gameManager.WordChecker.RemoveTheLastLetterAddedToWordToFill();
                RemoveTheLastBoxAddedToAllCurrentBoxThatAreCheckedList();
                OverrideCurrentPrincipalBoxChecked();
                gameManager.AudioManager.PlayUnCheckButtonSfx();
            }
        }
    }

    private void PlaySelectedBoxSound()
    {
        gameManager.AudioManager.PlaySelectedButtonSfx();
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
        currentPrincipalBoxChecked.ChangeThisBoxScaleSizeToDefaultValue();
        if (amountOfBoxesThatAreCurrentChecked > 0) amountOfBoxesThatAreCurrentChecked -= 1;
    }

    public void SetThisBoxAsCompleted()
    {
        isThisBoxCompleted = true;
        SetThisBoxColorToCompletedColor();
    }

    private void ChangeThisBoxScaleToSelectedSize()
    {
        boxScaleTween.SetThisBoxSizeAsSelectedSize();
    }

    private void ChangeThisBoxScaleSizeToDefaultValue()
    {
        boxScaleTween.SetThisBoxSizeToDefaultValue();
    }

    private void SetThisBoxColorToCompletedColor() => imageFromThisBox.color = completedColor;

    private void ChangeTheImageColorFromThisBox(Color newColor) => imageFromThisBox.color = newColor;

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
        yield return new WaitForSeconds(0.5f);

        List<RaycastHit2D> hit = new List<RaycastHit2D>();
        float rayXDistance = 1000f;
        float rayYDistance = 1000f;


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

    private void SetThisBoxValuesToDefault()
    {
        if (!isThisBoxCompleted)
        {
            thisBoxIsChecked = false;
            imageFromThisBox.color = Color.gray;
            canThisBoxBeSelected = false;
            ChangeThisBoxScaleSizeToDefaultValue();
        }
    }

    private void ResetCompletelyThisBoxValues()
    {
        thisBoxIsChecked = false;
        isThisBoxCompleted = false;
        canThisBoxBeSelected = false;
        boxesThatCanBeChecked.Clear();
        imageFromThisBox.color = Color.gray;
    }

    public static void ResetGame()
    {
        onResetedGame?.Invoke();
    }

    public static void ResetTouchUp()
    {
        currentPrincipalBoxChecked = null;
        theFirstBoxChecked = null;
        amountOfBoxesThatAreCurrentChecked = 0;
        currentBoxesThatAreChecked.Clear();
    }

    private void OnEnable()
    {
        gameManager.PlayerTouchController.TouchUpEvent += SetThisBoxValuesToDefault;
        onResetedGame += ResetCompletelyThisBoxValues;
        StartCoroutine(GetAllBoxesThatCanBeSelectedByThisBox());
    }

    private void OnDisable()
    {
        gameManager.PlayerTouchController.TouchUpEvent -= SetThisBoxValuesToDefault;
        onResetedGame -= ResetCompletelyThisBoxValues;
    }

    private void OnDestroy()
    {
        gameManager.PlayerTouchController.TouchUpEvent -= SetThisBoxValuesToDefault;
        onResetedGame -= ResetCompletelyThisBoxValues;
        Destroy(this );
    }
}