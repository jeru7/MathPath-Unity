using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioManager audioManager;
    public BarAnimation barAnimation;

    // 0.2f is 20%
    public const float volumeChangeAmount = 0.2f;

    public void Start()
    {
        Player player = GameManager.Instance?.Player;
        if (player != null && player.GetSettings() != null)
        {
            audioManager.InitializeVolume(player.GetSettings().GetMusic(), player.GetSettings().GetSfx());
        }

        barAnimation.UpdateMusicVolumeBars(audioManager.musicSource.volume, volumeChangeAmount);
        barAnimation.UpdateSFXVolumeBars(audioManager.sfxSource.volume, volumeChangeAmount);
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
        Player player = GameManager.Instance?.Player;
        if (player != null && player.GetSettings() != null)
        {
            player.GetSettings().SetMusic(newVolume);
        }
    }

    private void UpdateSfxVolume(float newVolume)
    {
        Player player = GameManager.Instance?.Player;
        if (player != null && player.GetSettings() != null)
        {
            player.GetSettings().SetSfx(newVolume);
        }
    }
}
