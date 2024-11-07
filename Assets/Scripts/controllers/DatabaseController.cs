using SQLite4Unity3d;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using System.Linq;
using Unity.VisualScripting;

public class DatabaseController : MonoBehaviour
{
    private SQLiteConnection _connection;
    private string apiUrl = "backend endpoint";
    void Start()
    {
        string dbPath = Application.dataPath + "/GameData.db";
        Debug.Log("Database path" + dbPath);

        _connection = new SQLiteConnection(dbPath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);

        _connection.CreateTable<Settings>();
        _connection.CreateTable<History>();
    }

    #region Settings methods
    // TODO: user settings methods
    public void SaveSettings(float currentSfx, float currentMusic)
    {
        _connection.InsertOrReplace(new Settings {sfx = currentSfx, music = currentMusic });
    }

    public Settings LoadSettings()
    {
        return _connection.Table<Settings>().FirstOrDefault();
    }

    #endregion Settings methods

    #region History methods
    // TODO: user history methods
    public void SaveHistory(int level, float sfx, float music, List<string> gameLevelIds, List<string> bagItems)
    {
        var history = new History
        {
            level = level,
            settingsSfx = sfx,
            settingsMusic = music,
            gameLevelIds = string.Join(',', gameLevelIds),
            bag = string.Join(",", bagItems),
            createdAt = System.DateTime.Now,
        };
        _connection.Insert(history);
    }

    public List<History> LoadHistory()
    {
        return _connection.Table<History>().ToList();
    }

    #endregion History methods

    #region Sync database methods

    // TODO: methods for syncing the database
    public void SyncDatabase(string studentId)
    {
        StartCoroutine(SyncServerCoroutine(studentId));
    }

    private IEnumerator SyncServerCoroutine(string studentId)
    {
        UnityWebRequest req = UnityWebRequest.Get(apiUrl + "/" + studentId);
        yield return req.SendWebRequest();

        if(req.result == UnityWebRequest.Result.Success)
        {
            Student studentMongo = JsonUtility.FromJson<Student>(req.downloadHandler.text);

            List<History> localHistories = _connection.Table<History>().OrderBy(h => h.createdAt).ToList();

            foreach (var localHistory in localHistories)
            {
                bool hasChanges = false;
                var updatedSettings = new
                {
                    sfx = localHistory.settingsSfx,
                    music = localHistory.settingsMusic,
                };

                if(studentMongo.settings.sfx != updatedSettings.sfx || studentMongo.settings.music != updatedSettings.music)
                {
                    studentMongo.settings.sfx = updatedSettings.sfx;
                    studentMongo.settings.music = updatedSettings.music;
                    hasChanges = true;
                }

                List<string> localGameLevelIds = localHistory.gameLevelIds.Split(',').ToList();
                if(!localGameLevelIds.SequenceEqual(studentMongo.gameLevelIds))
                {
                    studentMongo.gameLevelIds = localGameLevelIds;
                    hasChanges = true;
                }

                List<string> localBagItems = localHistory.bag.Split(',').ToList();
                if(!localBagItems.SequenceEqual(studentMongo.bag)) 
                {
                    studentMongo.bag = localBagItems;
                    hasChanges = true;
                }

                if (hasChanges)
                {
                    var updatePayload = new 
                    {
                        settings = studentMongo.settings,
                        gameLevelIds = studentMongo.gameLevelIds,
                        bagItems = studentMongo.bag,
                    };

                    UnityWebRequest updateRequest = UnityWebRequest.Put(apiUrl + "/" + studentId, JsonUtility.ToJson(updatePayload));
                    updateRequest.SetRequestHeader("Content-Type", "application/json");
                    yield return updateRequest.SendWebRequest();

                    if(updateRequest.result == UnityWebRequest.Result.Success)
                    {
                        Debug.Log("Sync success");
                    }
                    else 
                    {
                        Debug.LogError("Sync failed");
                    }
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

    #region classes for representations
    public class Student 
    {
        public string id;
        public Settings settings;
        public List<string> gameLevelIds;
        public List<string> bag;

    }
    public class Settings 
    {
        [PrimaryKey, AutoIncrement]
        public int id {get; set;}
        public float sfx {get; set;}
        public float music {get; set;}
    }

    public class History
    {
        [PrimaryKey, AutoIncrement]
        public int id {get; set;}
        public int level {get; set;}
        public float settingsSfx {get; set;}
        public float settingsMusic {get; set;}
        public string gameLevelIds {get; set;}
        public string bag {get; set;}
        public System.DateTime createdAt {get; set;}
    }
    #endregion classes for representations
}
