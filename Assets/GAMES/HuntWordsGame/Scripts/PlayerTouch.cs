using UnityEngine;

public class PlayerTouch : MonoBehaviour
{
    public static PlayerTouch Instance;

    public bool isTouchingTheScreen;

    private void Awake()
    {
       Instance = this;
       PlayerTouchController.TouchUp += ResetPointerValues;
       Application.targetFrameRate = 60;
    }

    private void ResetPointerValues()
    {
        isTouchingTheScreen = false;

        WordChecker.Instance.CheckIfTheWordToFilIsEqualToAnyWordToSearch();
    }
}
