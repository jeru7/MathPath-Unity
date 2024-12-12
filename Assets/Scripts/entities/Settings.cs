using SQLite4Unity3d;
using UnityEngine;

public class Settings : MonoBehaviour
{
    public static Settings Instance;
    private AudioManager audioManager;

    [PrimaryKey, AutoIncrement]
    public int id { get; set; }
    public float sfx { get; set; }
    public float music { get; set; }

    public Settings()
    {
        sfx = 1.0f;
        music = 1.0f;
    }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        audioManager = AudioManager.Instance;
    }

    public int GetId() => id;
    public void SetId(int id) => this.id = id;

    public float GetSfx() => sfx;
    public void SetSfx(float sfx)
    {
        this.sfx = sfx;
        audioManager.sfxSource.volume = sfx;
    }

    public float GetMusic() => music;
    public void SetMusic(float music)
    {
        this.music = music;
        audioManager.musicSource.volume = music;
    }
}
