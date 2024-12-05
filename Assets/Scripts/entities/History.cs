using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using SQLite4Unity3d;

public class History
{
    [PrimaryKey, AutoIncrement]
    public int id { get; set; }
    public int level { get; set; }
    public int coins { get; set; }
    public float sfx { get; set; }
    public float music { get; set; }
    public string gameLevelIds { get; set; }
    public string bag { get; set; }
    public DateTime timestamp { get; set; }

    public History() { }

    public History(int level, int coins, float sfx, float music, List<string> gameLevelIds, List<string> bag)
    {
        this.level = level;
        this.coins = coins;
        this.sfx = sfx;
        this.music = music;
        this.gameLevelIds = string.Join(",", gameLevelIds);
        this.bag = string.Join(',', bag);
        timestamp = DateTime.Now;
    }

    public int GetLevel() => level;
    public void SetLevel(int level) => this.level = level;

    public int GetCoins() => coins;
    public void SetCoins(int coins) => this.coins = coins;

    public float GetSfx() => sfx;
    public void SetSfx(float sfx) => this.sfx = sfx;

    public float GetMusic() => music;
    public void SetMusic(float music) => this.music = music;

    public List<string> GetGameLevelIds() => gameLevelIds.Split(',').ToList();
    public void SetGameLevelIds(List<string> value) => gameLevelIds = string.Join(",", value);

    public List<string> GetBag() => bag.Split(',').ToList();
    public void SetBag(List<string> value) => bag = string.Join(",", value);

    public DateTime GetTimestamp() => timestamp;
    public void SetTimeStamp() => timestamp = DateTime.Now;
}
