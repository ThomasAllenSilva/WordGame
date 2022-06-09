using System;
using UnityEngine;

public class PlayerTouchController : MonoBehaviour
{
    private PlayerTouchActions touchActions;

    public event Action TouchUp;

    public bool isTouchingTheScreen;

    public static PlayerTouchController Instance;

    private void Awake()
    {
        Application.targetFrameRate = 60;

        Instance = this;

        touchActions = new PlayerTouchActions();
        touchActions.Enable();

        touchActions.Touch.TouchUp.canceled += _ => TouchUp?.Invoke();

        TouchUp += ResetPointerValues;
    }

    private void ResetPointerValues()
    {
        isTouchingTheScreen = false;

        WordChecker.Instance.CheckIfTheWordToFilIsEqualToAnyWordToSearch();
    }

    private void OnDestroy()
    {
        TouchUp -= ResetPointerValues;
    }
}
