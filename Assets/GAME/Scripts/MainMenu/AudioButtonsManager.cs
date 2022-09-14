using UnityEngine;

public class AudioButtonsManager : MonoBehaviour
{
    [SerializeField] private GameObject gameMusicOnButton;
    [SerializeField] private GameObject gameMusicOffButton;

    [SerializeField] private GameObject gameSoundsOnButton;
    [SerializeField] private GameObject gameSoundsOffButton;

    void Start()
    {
        if (DataManager.Instance.GameDataManager.GameData.isGameMusicMuted)
        {
            gameMusicOnButton.SetActive(false);
            gameMusicOffButton.SetActive(true);
        }

        if (DataManager.Instance.GameDataManager.GameData.isGameAudioMuted)
        {
            gameSoundsOnButton.SetActive(false);
            gameSoundsOffButton.SetActive(true);
        }
    }
}
