using UnityEngine;

public class CheckSystemLanguage : MonoBehaviour
{
    public int LocalSystemLanguageIndexCode { get; private set; }

    private void Awake()
    {
        if (Application.systemLanguage == SystemLanguage.English)
        {
            LocalSystemLanguageIndexCode = 0;
        }

        else if (Application.systemLanguage == SystemLanguage.Portuguese)
        {
            LocalSystemLanguageIndexCode = 1;
        }

        else
        {
            LocalSystemLanguageIndexCode = 0;
        }
    }
}
