using UnityEngine;

public class FadeManager : MonoBehaviour
{
    private int sceneIndex;

    public void LoadScene()
    {
        ScenesManager.Instance.LoadSceneByIndex(sceneIndex);
    }

    public void SetSceneToLoadIndex(int index)
    {
        sceneIndex = index;
    }
}
