using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    public AudioManager audioManager;
    public AudioClip clickSound;
    public BarAnimation barAnimation;

    // 0.2f is 20%
    public const float volumeChangeAmount = 0.2f;


    /// <summary>
    /// Increases music volume.
    /// </summary>
    public void IncreaseMusicVolume()
    {
        audioManager.musicSource.volume = Mathf.Clamp(audioManager.musicSource.volume + volumeChangeAmount, 0f, 1f);
        barAnimation.UpdateMusicVolumeBars(audioManager.musicSource.volume, volumeChangeAmount);
        audioManager.PlaySFX(clickSound);
    }

    /// <summary>
    /// Decreases music volume.
    /// </summary>
    public void DecreaseMusicVolume()
    {
        audioManager.musicSource.volume = Mathf.Clamp(audioManager.musicSource.volume - volumeChangeAmount, 0f, 1f);
        barAnimation.UpdateMusicVolumeBars(audioManager.musicSource.volume, volumeChangeAmount);
        audioManager.PlaySFX(clickSound);
    }

    /// <summary>
    /// Increases sfx volume.
    /// </summary>
    public void IncreaseSFXVolume()
    {
        audioManager.sfxSource.volume = Mathf.Clamp(audioManager.sfxSource.volume + volumeChangeAmount, 0f, 1f);
        barAnimation.UpdateSFXVolumeBars(audioManager.sfxSource.volume, volumeChangeAmount);
        audioManager.PlaySFX(clickSound);
    }

    /// <summary>
    /// Decreases sfx volume.
    /// </summary>
    public void DecreaseSFXVolume()
    {
        audioManager.sfxSource.volume = Mathf.Clamp(audioManager.sfxSource.volume - volumeChangeAmount, 0f, 1f);
        barAnimation.UpdateSFXVolumeBars(audioManager.sfxSource.volume, volumeChangeAmount);
        audioManager.PlaySFX(clickSound);
    }

    /// <summary>
    /// Initializes the volume bar sprites based on the current sfx and music volume. 
    /// </summary>
    private void Start()
    {
        barAnimation.UpdateMusicVolumeBars(audioManager.musicSource.volume, volumeChangeAmount);
        barAnimation.UpdateSFXVolumeBars(audioManager.sfxSource.volume, volumeChangeAmount);
    }
}
