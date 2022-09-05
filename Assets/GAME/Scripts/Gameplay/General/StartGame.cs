using UnityEngine;

public class StartGame : MonoBehaviour
{

    private void Awake()
    {
        GameManager.Instance.LevelManager.StartLevel();
    }

    void Start()
    {
        Destroy(this.gameObject); 
    }
}
