using System;
using UnityEngine;

public class PlayerTouchController : MonoBehaviour
{
    PlayerTouchActions touchActions;

    public static event Action TouchUp; 

    private void Awake()
    {
        touchActions = new PlayerTouchActions();
        touchActions.Enable();
        touchActions.Touch.TouchUp.canceled += _ => TouchUp?.Invoke();
    }
}
