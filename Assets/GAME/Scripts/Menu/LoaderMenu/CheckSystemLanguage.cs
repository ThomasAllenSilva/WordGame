using UnityEngine;

public class CheckSystemLanguage : MonoBehaviour
{
    public int LocalSystemLanguageIndex { get; private set; }

    private void Awake()
    {
        if (Application.systemLanguage == SystemLanguage.English)
        {
            LocalSystemLanguageIndex = 0;
        }

        else if (Application.systemLanguage == SystemLanguage.Portuguese)
        {
            LocalSystemLanguageIndex = 1;
        }

        else
        {
            LocalSystemLanguageIndex = 0;
        }
    }
}
