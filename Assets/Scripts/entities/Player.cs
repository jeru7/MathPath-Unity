using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;

    public string id { get; set; }
    public string username { get; set; }
    public string characterName { get; set; }
    public int level { get; set; }
    public string character { get; set; }
    public int coins { get; set; }
    public SettingsData settings { get; set; }
    public HistoryData history { get; set; }
    public List<string> gameLevelIds { get; set; }
    public List<string> bagItems { get; set; }

    public Player() { }

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

    public string GetId() => id;
    public void SetId(string id) => this.id = id;

    public string GetUsername() => username;
    public void SetUsername(string username) => this.username = username;

    public string GetCharacterName() => characterName;
    public void SetCharacterName(string characterName) => this.characterName = characterName;

    public int GetLevel() => level;
    public void SetLevel(int level) => this.level = level;

    public string GetCharacter() => character;
    public void SetCharacter(string character) => this.character = character;

    public int GetCoins() => coins;
    public void SetCoins(int coins) => this.coins = coins;

    public SettingsData GetSettings() => settings;

    public void SetSettings(SettingsData settings) => this.settings = settings;

    public HistoryData GetHistory() => history;
    public void SetHistory(HistoryData history) => this.history = history;

    public List<string> GetGameLevelIds() => gameLevelIds;
    public List<string> SetGameLevelIds(List<string> gameLevelIds) => this.gameLevelIds = gameLevelIds;

    public List<string> GetBagItems() => bagItems;
    public void SetBagItems(List<string> bagItems) => this.bagItems = bagItems;
}
