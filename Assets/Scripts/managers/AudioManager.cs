using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource musicSource;
    public AudioSource sfxSource;

    public AudioClip clickSoundClip;
    public AudioClip backgroundMusicClip;

    /// <summary>
    /// Initializes the audio settings from the player's stored settings.
    /// </summary>
    /// <param name="musicVolume"></param>
    /// <param name="sfxVolume"></param>
    public void InitializeVolume(float musicVolume, float sfxVolume)
    {
        musicSource.volume = musicVolume;
        sfxSource.volume = sfxVolume;
    }

    /// <summary>
    /// Plays the background music. 
    /// </summary>
    public void PlayMusic()
    {
        musicSource.clip = backgroundMusicClip;
        musicSource.loop = true;
        musicSource.Play();
    }

    /// <summary>
    /// Plays sfx.
    /// </summary>
    public void PlayClickSound()
    {
        sfxSource.PlayOneShot(clickSoundClip);
    }
}