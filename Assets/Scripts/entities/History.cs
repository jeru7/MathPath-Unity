using System;
using System.Collections.Generic;
using System.Linq;
using SQLite4Unity3d;
using Unity.Properties;
using Unity.VisualScripting;
using UnityEngine;

public class History : MonoBehaviour
{
    public static History Instance;

    [PrimaryKey, AutoIncrement]
    public int id { get; set; }
    public int level { get; set; }
    public int coins { get; set; }
    public float sfx { get; set; }
    public float music { get; set; }
    public string gameLevelIds { get; set; }
    public string bagItems { get; set; }
    public DateTime timestamp { get; set; }

    public History() { }

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

    public int GetLevel() => level;
    public void SetLevel(int level) => this.level = level;

    public int GetCoins() => coins;
    public void SetCoins(int coins) => this.coins = coins;

    public float GetSfx() => sfx;
    public void SetSfx(float sfx) => this.sfx = sfx;

    public float GetMusic() => music;
    public void SetMusic(float music) => this.music = music;

    public List<string> GetGameLevelIds()
    {
        return string.IsNullOrEmpty(gameLevelIds)
        ? new List<string>()
        : JsonUtility.FromJson<Wrapper<string>>(gameLevelIds).Items;
    }
    public void SetGameLevelIds(List<string> value)
    {
        gameLevelIds = JsonUtility.ToJson(new Wrapper<string> { Items = value });
    }

    public List<string> GetBagItems()
    {
        return string.IsNullOrEmpty(bagItems)
        ? new List<string>()
        : JsonUtility.FromJson<Wrapper<string>>(bagItems).Items;
    }
    public void SetBagItems(List<string> value)
    {
        bagItems = JsonUtility.ToJson(new Wrapper<string> { Items = value });
    }

    public DateTime GetTimestamp() => timestamp;
    public void SetTimeStamp() => timestamp = DateTime.Now;

    [Serializable]
    private class Wrapper<T>
    {
        public List<T> Items;
    }
}
