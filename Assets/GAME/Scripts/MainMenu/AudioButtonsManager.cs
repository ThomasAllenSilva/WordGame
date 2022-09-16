using UnityEngine;

public class AudioButtonsManager : MonoBehaviour
{
    [SerializeField] private GameObject gameMusicOnButton;
    [SerializeField] private GameObject gameMusicOffButton;

    [SerializeField] private GameObject gameSoundsOnButton;
    [SerializeField] private GameObject gameSoundsOffButton;

    void Start()
    {
        if (DataManager.Instance.GameDataManager.IsGameMusicMuted)
        {
            gameMusicOnButton.SetActive(false);
            gameMusicOffButton.SetActive(true);
        }

        if (DataManager.Instance.GameDataManager.IsGameSoundsMuted)
        {
            gameSoundsOnButton.SetActive(false);
            gameSoundsOffButton.SetActive(true);
        }
    }
}
