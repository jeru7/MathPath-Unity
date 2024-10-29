using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarAnimation : MonoBehaviour
{
    public GameObject musicVolumeBarContainer;
    public GameObject sfxVolumeBarContainer;

    public Sprite emptyBar;
    public Sprite filledBar;

    /// <summary>
    /// Updates the music volume bar.
    /// </summary>
    /// <param name="musicVol">The new music volume.</param>
    /// <param name="changeAmount">The amount to change the volume (default: 0.2f or 20%)</param>
    public void UpdateMusicVolumeBars(float musicVol, float changeAmount)
    {
        UpdateBarSprites(musicVol, musicVolumeBarContainer, changeAmount);
    }

    /// <summary>
    /// Updates the sfx volume bar.
    /// </summary>
    /// <param name="sfxVol">The new sfx volume.</param>
    /// <param name="changeAmount">The amount to change the volume (default: 0.2f or 20%)</param>
    public void UpdateSFXVolumeBars(float sfxVol, float changeAmount)
    {
        UpdateBarSprites(sfxVol, sfxVolumeBarContainer, changeAmount);
    }

    /// <summary>
    /// The method that updates the bar sprites
    /// </summary>
    /// <param name="volume">The current volume (sfx or music)</param>
    /// <param name="volumeBarContainer">References to the image components (each per bar: contains 5)</param>
    /// <param name="changeAmount">The amount to change the volume (default: 0.2f or 20%)</param>
    public void UpdateBarSprites(float volume, GameObject volumeBarContainer, float changeAmount)
    {
        Image[] volumeBars = volumeBarContainer.GetComponentsInChildren<Image>();

        int filledBarsCount = Mathf.FloorToInt(volume / changeAmount);

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
}
