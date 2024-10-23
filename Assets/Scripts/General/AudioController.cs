using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class AudioController : MonoBehaviour
{
    public AudioManager audioManager;
    public AudioClip clickSound;

    // canvas for musicbar and sfxbar references
    public GameObject musicVolumeBarContainer;
    public GameObject sfxVolumeBarContainer;

    // sprite image reference
    public Sprite emptyBar;
    public Sprite filledBar;

    // 0.2f is 20%
    private const float volumeChangeAmount = 0.2f;

    // updates both the bar
    private void UpdateVolumeBars()
    {
        UpdateBarSprites(audioManager.musicSource.volume, musicVolumeBarContainer);
        UpdateBarSprites(audioManager.sfxSource.volume, sfxVolumeBarContainer);
    }

    // updates the bar sprites
    private void UpdateBarSprites(float volume, GameObject volumeBarContainer)
    {
        // references the images on the container
        Image[] volumeBars = volumeBarContainer.GetComponentsInChildren<Image>();

        // checks how many filled bars to render
        int filledBarsCount = Mathf.FloorToInt(volume / volumeChangeAmount);

        for (int i = 0; i < volumeBars.Length; i++)
        {
            if (i < filledBarsCount)
            {
                volumeBars[i].sprite = filledBar;
            }
            else
            {
                volumeBars[i].sprite = emptyBar;
            }
        }
    }

    // method to increase music volume
    public void IncreaseMusicVolume()
    {
        audioManager.musicSource.volume = Mathf.Clamp(audioManager.musicSource.volume + volumeChangeAmount, 0f, 1f);
        UpdateVolumeBars();
        audioManager.PlaySFX(clickSound);
    }

    // method to decrease music volume
    public void DecreaseMusicVolume()
    {
        audioManager.musicSource.volume = Mathf.Clamp(audioManager.musicSource.volume - volumeChangeAmount, 0f, 1f);
        UpdateVolumeBars();
        audioManager.PlaySFX(clickSound);
    }

    // method to increase sfx volume
    public void IncreaseSFXVolume()
    {
        audioManager.sfxSource.volume = Mathf.Clamp(audioManager.sfxSource.volume + volumeChangeAmount, 0f, 1f);
        UpdateVolumeBars();
        audioManager.PlaySFX(clickSound);
    }

    // method to decrease sfx volume
    public void DecreaseSFXVolume()
    {
        audioManager.sfxSource.volume = Mathf.Clamp(audioManager.sfxSource.volume - volumeChangeAmount, 0f, 1f);
        UpdateVolumeBars();
        audioManager.PlaySFX(clickSound);
    }

    // initially updates the volume bars 
    private void Start()
    {
        UpdateVolumeBars();
    }
}
