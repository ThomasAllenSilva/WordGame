using UnityEngine;

public class StartGame : MonoBehaviour
{
    private void Start()
    {
        GameManager.Instance.LevelManager.LoadNextLevel();
        Destroy(this.gameObject);
    }
}
