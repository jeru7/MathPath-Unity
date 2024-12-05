using SQLite4Unity3d;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using System.Linq;

// TODO: test and finalize the code
public class DatabaseController : MonoBehaviour
{
    private SQLiteConnection _connection;
    private string url = "http://localhost:3001/student";
    void Start()
    {
        string dbPath = Application.dataPath + "/GameData.db";
        Debug.Log("Database path" + dbPath);

        _connection = new SQLiteConnection(dbPath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);

        // creates a sql table for settings and history
        if (!_connection.Table<Settings>().Any())
        {
            _connection.CreateTable<Settings>();
        }
        if (!_connection.Table<History>().Any())
        {
            _connection.CreateTable<History>();
        }

    }

    #region Settings methods

    public void SaveSettings(float currentSfx, float currentMusic)
    {
        var settings = new Settings
        {
            sfx = currentSfx,
            music = currentMusic,
        };

        _connection.InsertOrReplace(settings);
    }

    public Settings LoadSettings()
    {
        return _connection.Table<Settings>().FirstOrDefault();
    }

    #endregion Settings methods

    #region History methods

    public void SaveHistory(int level, int coins, float sfx, float music, List<string> gameLevelIds, List<string> bagItems)
    {
        var history = new History
        {
            level = level,
            coins = coins,
            sfx = sfx,
            music = music,
            gameLevelIds = string.Join(',', gameLevelIds),
            bag = string.Join(",", bagItems),
            timestamp = System.DateTime.Now,
        };

        _connection.InsertOrReplace(history);
    }

    public History LoadHistory()
    {
        return _connection.Table<History>().FirstOrDefault();
    }

    #endregion History methods

    #region Sync database methods

    public void SyncDatabase(string studentId)
    {
        StartCoroutine(SyncServerCoroutine(studentId));
    }

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

            List<string> localGameLevelIds = localHistory.gameLevelIds.Split(',').ToList();
            if (!localGameLevelIds.SequenceEqual(studentMongo.gameLevelIds))
            {
                studentMongo.gameLevelIds = localGameLevelIds;
                hasChanges = true;
            }

            List<string> localBagItems = localHistory.bag.Split(',').ToList();
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
    #endregion Sync database methods
}
