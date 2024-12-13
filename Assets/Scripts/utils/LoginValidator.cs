using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class LoginValidator : MonoBehaviour
{
    public TMP_InputField username, password;
    public GameObject passwordError, usernameError, passwordEmpty, usernameEmpty;
    private Player player;
    private Settings settings;
    public AudioManager audioManager;
    private SceneController sceneController;
    private DatabaseController dbController;
    // api end point
    private string loginUrl = "http://localhost:3001/game/auth/login";

    /// <summary>
    /// runs immediately when the login button is clicked
    /// </summary>
    public void OnLogin()
    {
        audioManager.PlayClickSound();
        ResetError();

        if (!IsValidField()) return;

        StartCoroutine(LoginCoroutine(username.text, password.text));
    }

    /// <summary>
    /// required enumerator for the couroutine
    /// responsible for the async process to check the credentials of the username and password
    /// </summary>
    /// <param name="username">the username value on the username field</param>
    /// <param name="password">the password value on the password field</param>
    /// <returns></returns>
    private IEnumerator LoginCoroutine(string username, string password)
    {
        dbController = DatabaseController.Instance;
        player = Player.Instance;
        settings = Settings.Instance;
        sceneController = SceneController.Instance;

        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("password", password);

        using (UnityWebRequest request = UnityWebRequest.Post(loginUrl, form))
        {

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Server Response: " + request.downloadHandler.text);

                var response = JsonUtility.FromJson<LoginSuccessResponse>(request.downloadHandler.text);

                InitializePlayer(response);

                SyncWithDatabase();

                ResetError();

                if (response.character == "")
                {
                    sceneController.GoToTitleScreen();
                }
                else
                {
                    sceneController.GoToGameMainHub();
                }
            }
            else
            {
                var response = JsonUtility.FromJson<LoginErrorResponse>(request.downloadHandler.text);

                if (response.error == "Invalid username")
                {
                    usernameError.SetActive(true);
                }
                else if (response.error == "Invalid password")
                {
                    passwordError.SetActive(true);
                }
            }
        }
    }

    private void InitializePlayer(LoginSuccessResponse response)
    {
        if (player == null)
        {
            Debug.Log("Player instance is null");
            return;
        }

        player.SetId(response.id);
        player.SetUsername(response.username);
        player.SetCharacterName(response.characterName);
        player.SetLevel(response.level);
        player.SetCharacter(response.character);
        player.SetCoins(response.coins);
        player.SetSettings(response.settings);
        player.SetHistory(response.history);
        player.SetGameLevelIds(response.gameLevelIds);
        player.SetBagItems(response.bagItems);
    }

    private void SyncWithDatabase()
    {

        if (dbController == null)
        {
            Debug.Log("dbController instance is null");
            return;
        }

        // TODO: update this once the ui for dialog for new settings is done

        // syncing history 
        dbController.SaveSettings(player.GetSettings().sfx, player.GetSettings().music);
        // sycn history
        dbController.SaveHistory(player.GetLevel(), player.GetCoins(), player.GetSettings().sfx, player.GetSettings().music, player.GetGameLevelIds(), player.GetBagItems());
    }

    /// <summary>
    /// check whether the input fields are empty
    /// </summary>
    /// <returns></returns>
    private bool IsValidField()
    {
        if (string.IsNullOrEmpty(username.text) && string.IsNullOrEmpty(password.text))
        {
            usernameEmpty.SetActive(true);
            passwordEmpty.SetActive(true);
            return false;
        }
        else if (string.IsNullOrEmpty(username.text))
        {
            usernameEmpty.SetActive(true);
            return false;
        }
        else if (string.IsNullOrEmpty(password.text))
        {
            passwordEmpty.SetActive(true);
            return false;
        }
        return true;
    }

    /// <summary>
    /// a function that simply resets the error ui indicator to false - defaults to inactive 
    /// </summary>
    public void ResetError()
    {
        passwordError.SetActive(false);
        usernameError.SetActive(false);
        passwordEmpty.SetActive(false);
        usernameEmpty.SetActive(false);
    }

    [System.Serializable]
    public class LoginSuccessResponse
    {
        public string id;
        public string username;
        public string characterName;
        public int level;
        public string character;
        public int coins;
        public SettingsData settings;
        public HistoryData history;
        public List<string> gameLevelIds;
        public List<string> bagItems;
    }
    [System.Serializable]
    public class LoginErrorResponse
    {
        public string error;
    }

}