using UnityEngine;

public class AudioController : MonoBehaviour
{
    private AudioManager audioManager;
    public BarAnimation barAnimation;
    private Player player;
    private Settings settings;

    // 0.2f is 20%
    public const float volumeChangeAmount = 0.2f;

    void Awake()
    {
        player = Player.Instance;
        settings = Settings.Instance;
        audioManager = AudioManager.Instance;

        barAnimation.UpdateMusicVolumeBars(audioManager.musicSource.volume, volumeChangeAmount);
        barAnimation.UpdateSFXVolumeBars(audioManager.sfxSource.volume, volumeChangeAmount);
    }

    public void SyncAudioSettings()
    {
        if (player != null && player.GetSettings() != null)
        {
            audioManager.SyncVolume(player.GetSettings().music, player.GetSettings().sfx);
        }

        barAnimation.UpdateMusicVolumeBars(audioManager.musicSource.volume, volumeChangeAmount);
        barAnimation.UpdateSFXVolumeBars(audioManager.sfxSource.volume, volumeChangeAmount);

        audioManager.PlayMusic();
    }


    /// <summary>
    /// Increases music volume.
    /// </summary>
    public void IncreaseMusicVolume()
    {
        audioManager.musicSource.volume = Mathf.Clamp(audioManager.musicSource.volume + volumeChangeAmount, 0f, 1f);
        UpdateMusicVolume(audioManager.musicSource.volume);
        barAnimation.UpdateMusicVolumeBars(audioManager.musicSource.volume, volumeChangeAmount);
        audioManager.PlayClickSound();
    }

    /// <summary>
    /// Decreases music volume.
    /// </summary>
    public void DecreaseMusicVolume()
    {
        audioManager.musicSource.volume = Mathf.Clamp(audioManager.musicSource.volume - volumeChangeAmount, 0f, 1f);
        UpdateMusicVolume(audioManager.musicSource.volume);
        barAnimation.UpdateMusicVolumeBars(audioManager.musicSource.volume, volumeChangeAmount);
        audioManager.PlayClickSound();
    }

    /// <summary>
    /// Increases sfx volume.
    /// </summary>
    public void IncreaseSFXVolume()
    {
        audioManager.sfxSource.volume = Mathf.Clamp(audioManager.sfxSource.volume + volumeChangeAmount, 0f, 1f);
        UpdateSfxVolume(audioManager.sfxSource.volume);
        barAnimation.UpdateSFXVolumeBars(audioManager.sfxSource.volume, volumeChangeAmount);
        audioManager.PlayClickSound();
    }

    /// <summary>
    /// Decreases sfx volume.
    /// </summary>
    public void DecreaseSFXVolume()
    {
        audioManager.sfxSource.volume = Mathf.Clamp(audioManager.sfxSource.volume - volumeChangeAmount, 0f, 1f);
        UpdateSfxVolume(audioManager.sfxSource.volume);
        barAnimation.UpdateSFXVolumeBars(audioManager.sfxSource.volume, volumeChangeAmount);
        audioManager.PlayClickSound();
    }

    private void UpdateMusicVolume(float newVolume)
    {
        if (player != null && player.GetSettings() != null)
        {
            player.GetSettings().music = newVolume;
        }

        if (settings != null)
        {
            settings.SetMusic(newVolume);
        }


    }

    private void UpdateSfxVolume(float newVolume)
    {
        if (player != null && player.GetSettings() != null)
        {
            player.GetSettings().sfx = newVolume;
        }

        if (settings != null)
        {
            settings.SetSfx(newVolume);
        }
    }
}
