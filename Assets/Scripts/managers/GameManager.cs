using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Player Player { get; private set; }

    private void Awake()
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

    public void InitializePlayerData(
        string id,
        string username,
        int level,
        string character,
        int coins,
        Settings settings,
        History history,
        List<string> gameLevelIds,
        List<string> bagItems
        )
    {
        Player = new Player(id, username, level, character, coins, settings, history, gameLevelIds, bagItems);
    }

    public void UpdatePlayerSettings(Settings newSettings)
    {
        if (Player != null)
        {
            Player.SetSettings(newSettings);
        }
    }

    public void UpdatePlayerHistory(History newHistory)
    {
        if (Player != null)
        {
            Player.SetHistory(newHistory);
        }
    }
}
