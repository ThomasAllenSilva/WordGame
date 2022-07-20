using System;
using UnityEngine;

public class PlayerTouchController : MonoBehaviour
{
    private PlayerTouchActions playerTouchActions;

    public event Action TouchUpEvent;

    private void Awake()
    {
        playerTouchActions = new PlayerTouchActions();
        playerTouchActions.Enable();

        TouchUpEvent += Box.ResetAllBoxValues;
        playerTouchActions.Touch.TouchUp.canceled += _ => TouchUpEvent?.Invoke();
    }
}