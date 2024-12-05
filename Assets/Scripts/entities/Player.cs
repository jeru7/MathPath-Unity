using System;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public string id { get; set; }
    public string username { get; set; }
    public int level { get; set; }
    public string character { get; set; }
    public int coins { get; set; }
    public Settings settings { get; set; }
    public History history { get; set; }
    public List<string> gameLevelIds { get; set; }
    public List<string> bagItems { get; set; }

    public Player() { }
    public Player(string id, string username, int level, string character, int coins, Settings settings, History history, List<string> gameLevelIds, List<string> bagItems)
    {
        this.id = id;
        this.username = username;
        this.level = level;
        this.character = character;
        this.coins = coins;
        this.settings = settings;
        this.history = history;
        this.gameLevelIds = gameLevelIds;
        this.bagItems = bagItems;
    }

    public string GetId() => id;
    public void SetId(string id) => this.id = id;

    public string GetUsername() => username;
    public void SetUsername(string username) => this.username = username;

    public int GetLevel() => level;
    public void SetLevel(int level) => this.level = level;

    public string GetCharacter() => character;
    public void SetCharacter(string character) => this.character = character;

    public int GetCoins() => coins;
    public void SetCoins(int coins) => this.coins = coins;

    public Settings GetSettings() => settings;
    public void SetSettings(Settings settings) => this.settings = settings;

    public History GetHistory() => history;
    public void SetHistory(History history) => this.history = history;

    public List<string> GetGameLevelIds() => gameLevelIds;
    public List<string> SetGameLevelIds(List<string> gameLevelIds) => this.gameLevelIds = gameLevelIds;

    public List<string> GetBagItems() => bagItems;
    public void SetBagItems(List<string> bagItems) => this.bagItems = bagItems;
}
