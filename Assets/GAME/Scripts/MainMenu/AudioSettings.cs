using UnityEngine;
using UnityEngine.Audio;

public class AudioSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer masterGameAudioMixer;

    private const string GameMusicMixerName = "GameMusicVolume";
    private const string GameSoundsMixerName = "GameSoundsVolume";

    private bool playerHasMutedGameMusic;
    private bool playerHasMutedGameSounds;

    private void Start()
    {
        if (DataManager.Instance.GameDataManager.GameData.isGameMusicMuted)
        {
            MuteGameMusic();
        }

        if (DataManager.Instance.GameDataManager.GameData.isGameAudioMuted)
        {
            MuteGameSounds();
        }
    }

    public void MuteGameSounds()
    {
        SetVolumeToAudioMixer(GameSoundsMixerName, -80);
        playerHasMutedGameSounds = true;
    }

    public void UnMuteGameSounds()
    {
        SetVolumeToAudioMixer(GameSoundsMixerName, -15);
        playerHasMutedGameSounds = false;
    }

    public void MuteGameMusic()
    {
        SetVolumeToAudioMixer(GameMusicMixerName, -80);
        AudioManager.Instance.StopPlayingBackgroundGameMusic();
        playerHasMutedGameMusic = true;
    }

    public void UnMuteGameMusic()
    {
        SetVolumeToAudioMixer(GameMusicMixerName, 7);
        AudioManager.Instance.StartPlayingBackgroundGameMusic();
        playerHasMutedGameMusic = false;
    }

    private void SetVolumeToAudioMixer(string mixerName, float volume)
    {
        masterGameAudioMixer.SetFloat(mixerName, volume);
    }

    public void SaveAudioChanges()
    {
        DataManager.Instance.GameDataManager.OverWriteGameDataAudioInfo(playerHasMutedGameMusic, playerHasMutedGameSounds);
    }
}
