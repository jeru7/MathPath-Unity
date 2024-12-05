using UnityEngine;
using SQLite4Unity3d;
using Unity.VisualScripting;

public class Settings
{
    [PrimaryKey, AutoIncrement]
    public string id { get; set; }
    public float sfx = 1.0f;
    public float music = 1.0f;

    public Settings() { }

    public Settings(float sfx, float music)
    {
        this.sfx = sfx;
        this.music = music;
    }

    public string GetId() => id;
    public void SetId(string id) => this.id = id;

    public float GetSfx() => sfx;
    public void SetSfx(float sfx) => this.sfx = sfx;

    public float GetMusic() => music;
    public void SetMusic(float music) => this.music = music;
}
