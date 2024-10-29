using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour, IAudioController
{
    public AudioSource musicSource;
    public AudioSource sfxSource;

    /// <summary>
    /// Plays the background music. 
    /// </summary>
    /// <param name="clip">References to the audio clip (music/sound source)</param>
    public void PlayMusic(AudioClip clip)
    {
        musicSource.clip = clip;
        musicSource.loop= true;
        musicSource.Play();
    }

    /// <summary>
    /// Plays sfx.
    /// </summary>
    /// <param name="clip">References to the audio clip (music/sound source)</param>
    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }
}
