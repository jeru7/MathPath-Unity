using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource musicSource;
    public AudioSource sfxSource;

    public AudioClip clickSoundClip;
    public AudioClip backgroundMusicClip;

    /// <summary>
    /// Plays the background music. 
    /// </summary>
    /// <param name="clip">References to the audio clip (music/sound source)</param>
    public void PlayMusic()
    {
        musicSource.clip = backgroundMusicClip;
        musicSource.loop= true;
        musicSource.Play();
    }

    /// <summary>
    /// Plays sfx.
    /// </summary>
    /// <param name="clip">References to the audio clip (music/sound source)</param>
    public void PlayClickSound()
    {
        sfxSource.PlayOneShot(clickSoundClip);
    }
}
