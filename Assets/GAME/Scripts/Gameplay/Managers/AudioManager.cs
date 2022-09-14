using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource gameSoundsAudioSource;
    private AudioSource gameMusicAudioSource;

    [SerializeField] private AudioClip UnCheckButtonAudioClip;

    public static AudioManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null) Instance = this;

        else Destroy(gameObject);

        DontDestroyOnLoad(Instance.gameObject);

        gameSoundsAudioSource = transform.GetChild(0).GetComponent<AudioSource>();
        gameMusicAudioSource = transform.GetChild(1).GetComponent<AudioSource>();
    }

    private void Start()
    {
        if (DataManager.Instance.GameDataManager.GameData.isGameMusicMuted)
        {
            StopPlayingBackgroundGameMusic();
        }
    }

    public void PlaySelectedButtonSfx()
    {
        gameSoundsAudioSource.Play();
    }

    public void PlayUnCheckButtonSfx()
    {
        gameSoundsAudioSource.PlayOneShot(UnCheckButtonAudioClip);
    }

    public void StartPlayingBackgroundGameMusic()
    {
        gameMusicAudioSource.Play();
    }

    public void StopPlayingBackgroundGameMusic()
    {
        gameMusicAudioSource.Stop();
    }
}
