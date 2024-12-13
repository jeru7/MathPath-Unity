using SQLite4Unity3d;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using System.Linq;

// TODO: test and finalize the code
public class DatabaseController : MonoBehaviour
{
    public static DatabaseController Instance;
    private Settings settings;
    private History history;
    private SQLiteConnection _connection;
    private string url = "http://localhost:3001/game/student";

    /// <summary>
    /// initializes the the database controller for persistent data
    /// </summary>
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

    /// <summary>
    /// runs after all awake
    /// creates or rewrite the db file for offline storage
    /// </summary>
    void Start()
    {
        // TODO: change to Application.persistentPath before build
        string dbPath = Application.dataPath + "/GameData.db";
        Debug.Log("Database path" + dbPath);

        _connection = new SQLiteConnection(dbPath, SQLiteOpenFlags.Create | SQLiteOpenFlags.ReadWrite);

        // creates a sql table for settings and history
        _connection.CreateTable<Settings>();
        Debug.Log("Settings table created");

        _connection.CreateTable<History>();
        Debug.Log("History table created");

    }

    public void SetCharacter(string studentId, string character, string characterName)
    {
        StartCoroutine(SetCharacterCoroutine(studentId, character, characterName));
    }

    /// <summary>
    /// saves the player's settings to the system's settings
    /// </summary>
    /// <param name="currentSfx"></param>
    /// <param name="currentMusic"></param>
    public void SaveSettings(float currentSfx, float currentMusic)
    {
        settings = Settings.Instance;

        settings.SetSfx(currentSfx);
        settings.SetMusic(currentMusic);

        _connection.InsertOrReplace(settings);
    }

    /// <summary>
    /// loads the system's settings
    /// </summary>
    /// <returns></returns>
    public Settings LoadSettings()
    {
        return _connection.Table<Settings>().FirstOrDefault();
    }

    /// <summary>
    /// tracks the player history during offline
    /// </summary>
    /// <param name="level"></param>
    /// <param name="coins"></param>
    /// <param name="sfx"></param>
    /// <param name="music"></param>
    /// <param name="gameLevelIds"></param>
    /// <param name="bagItems"></param>
    public void SaveHistory(int level, int coins, float sfx, float music, List<string> gameLevelIds, List<string> bagItems)
    {
        history = History.Instance;

        history.SetLevel(level);
        history.SetCoins(coins);
        history.SetSfx(sfx);
        history.SetMusic(music);
        history.SetBagItems(bagItems);
        history.SetGameLevelIds(gameLevelIds);

        _connection.InsertOrReplace(history);
    }

    /// <summary>
    /// returns the latest history, used for syncing when the internet connection is back
    /// </summary>
    /// <returns></returns>
    public History LoadHistory()
    {
        return _connection.Table<History>().FirstOrDefault();
    }

    /// <summary>
    /// syncs the sqlite db to mongodb
    /// </summary>
    /// <param name="studentId"></param>
    public void SyncDatabase(string studentId)
    {
        StartCoroutine(SyncServerCoroutine(studentId));
    }

    #region coroutines

    private IEnumerator SetCharacterCoroutine(string studentId, string character, string characterName)
    {
        UnityWebRequest req = new UnityWebRequest(url + "/character/" + studentId, "PUT");
        CharacterData characterData = new CharacterData { character = character, characterName = characterName };
        string jsonData = JsonUtility.ToJson(characterData);

        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
        req.uploadHandler = new UploadHandlerRaw(bodyRaw);
        req.downloadHandler = new DownloadHandlerBuffer();
        req.SetRequestHeader("Content-Type", "application/json");

        yield return req.SendWebRequest();

        if (req.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Character updated successfully");
        }
        else
        {
            Debug.LogError("Error in updating character: " + req.error);
        }

    }

    /// <summary>
    /// async method for syncing the databases
    /// </summary>
    /// <param name="studentId"></param>
    /// <returns></returns>
    private IEnumerator SyncServerCoroutine(string studentId)
    {
        UnityWebRequest req = UnityWebRequest.Get(url + "/" + studentId);
        yield return req.SendWebRequest();

        if (req.result == UnityWebRequest.Result.Success)
        {
            Player studentMongo = JsonUtility.FromJson<Player>(req.downloadHandler.text);
            History localHistory = LoadHistory();

            bool hasChanges = false;

            var updatedSettings = new
            {
                sfx = localHistory.sfx,
                music = localHistory.music,
            };

            if (studentMongo.settings.sfx != updatedSettings.sfx || studentMongo.settings.music != updatedSettings.music)
            {
                studentMongo.settings.sfx = updatedSettings.sfx;
                studentMongo.settings.music = updatedSettings.music;
                hasChanges = true;
            }

            if (studentMongo.level < localHistory.level)
            {
                studentMongo.level = localHistory.level;
                hasChanges = true;
            }

            List<string> localGameLevelIds = localHistory.GetGameLevelIds();
            if (!localGameLevelIds.SequenceEqual(studentMongo.gameLevelIds))
            {
                studentMongo.gameLevelIds = localGameLevelIds;
                hasChanges = true;
            }

            List<string> localBagItems = localHistory.GetBagItems();
            if (!localBagItems.SequenceEqual(studentMongo.bagItems))
            {
                studentMongo.bagItems = localBagItems;
                hasChanges = true;
            }

            if (hasChanges)
            {
                var updatePayload = new
                {
                    level = studentMongo.level,
                    coins = studentMongo.coins,
                    settings = studentMongo.settings,
                    gameLevelIds = studentMongo.gameLevelIds,
                    bagItems = studentMongo.bagItems,
                };

                UnityWebRequest updateRequest = UnityWebRequest.Put(url + "/" + studentId, JsonUtility.ToJson(updatePayload));
                updateRequest.SetRequestHeader("Content-Type", "application/json");
                yield return updateRequest.SendWebRequest();

                if (updateRequest.result == UnityWebRequest.Result.Success)
                {
                    Debug.Log("Sync success");
                }
                else
                {
                    Debug.LogError("Sync failed");
                }
            }

            _connection.DeleteAll<History>();
            Debug.Log("Local history deleted");
        }
        else
        {
            Debug.LogError("Failed to fetch student data");
        }
    }

    #endregion coroutines
}

// helper class
[System.Serializable]
public class CharacterData
{
    public string character;
    public string characterName;
}