using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance; // singleton approach
    public AudioSource musicSource;
    public AudioSource sfxSource;

    public AudioClip clickSoundClip;
    public AudioClip backgroundMusicClip;
    public AudioController audioController;
    private Settings settings;

    void Awake()
    {
        // the 'gameObject' automatically initialized when you attach this AudioManager
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        InitializeVolume();
    }

    /// <summary>
    /// initializes volume on start
    /// </summary>
    public void InitializeVolume()
    {
        settings = Settings.Instance;

        if (settings == null)
        {
            Debug.Log("Settings is null");
        }

        Debug.Log("Settings SFX: " + settings.GetSfx() + "Settings Music: " + settings.GetMusic());

        musicSource.volume = settings.GetMusic();
        sfxSource.volume = settings.GetSfx();
    }

    /// <summary>
    /// syncs player's volume settings into system's volume settings
    /// </summary>
    /// <param name="sfxVolume"></param>
    /// <param name="musicVolume"></param>
    public void SyncVolume(float sfxVolume, float musicVolume)
    {
        musicSource.volume = musicVolume;
        sfxSource.volume = sfxVolume;
    }

    /// <summary>
    /// Plays the background music. 
    /// </summary>
    public void PlayMusic()
    {
        if (!musicSource.isPlaying)
        {
            musicSource.clip = backgroundMusicClip;
            musicSource.loop = true;
            musicSource.Play();
        }
    }

    /// <summary>
    /// Plays sfx.
    /// </summary>
    public void PlayClickSound()
    {
        sfxSource.PlayOneShot(clickSoundClip);
    }
}