using UnityEngine;

public class FadeManager : MonoBehaviour
{
    private int sceneIndex;

    private void Start()
    {
        Debug.Log(sceneIndex);
    }
    public void LoadScene()
    {
        ScenesManager.Instance.LoadSceneByIndex(sceneIndex);
    }

    public void SetSceneToLoadIndex(int index)
    {
        sceneIndex = index;
    }
}
