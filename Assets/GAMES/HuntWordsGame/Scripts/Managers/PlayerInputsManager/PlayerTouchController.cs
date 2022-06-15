using System;
using UnityEngine;

public class PlayerTouchController : MonoBehaviour
{
    public bool IsTouchingTheScreen { get; private set; }

    public event Action TouchUpEvent;

    private PlayerTouchActions touchActions;

    private void Awake()
    {
        touchActions = new PlayerTouchActions();
        touchActions.Enable();
        touchActions.Touch.TouchUp.canceled += _ => TouchUpEvent?.Invoke();

        TouchUpEvent += PlayerStoppedTouchingTheScreen;
    }

    private void PlayerStoppedTouchingTheScreen()
    {
        IsTouchingTheScreen = false;
        GameManager.Instance.WordCheckerInfo().CheckIfTheWordToFillIsEqualsToAnyWordToSearchInThisLevel();
    }

    public void PlayerIsTouchingTheScreen()
    {
        IsTouchingTheScreen = true;
    }

    private void OnDestroy()
    {
        TouchUpEvent -= PlayerStoppedTouchingTheScreen;
    }
}
