using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class LoginValidator : MonoBehaviour
{
    public TMP_InputField username, password;
    public GameObject passwordError, usernameError, passwordEmpty, usernameEmpty;
    public AudioManager audioManager;
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
        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("password", password);

        using (UnityWebRequest request = UnityWebRequest.Post(loginUrl, form))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {

                var response = JsonUtility.FromJson<LoginSuccessResponse>(request.downloadHandler.text);

                GameManager.Instance.InitializePlayerData(
                    response.id,
                    response.username,
                    int.Parse(response.level),
                    response.character,
                    response.coins,
                    JsonUtility.FromJson<Settings>(response.settings),
                    JsonUtility.FromJson<History>(response.history),
                    response.gameLevelIds,
                    response.bagItems
                );

                SyncWithDatabase();

                ResetError();

                if (response.character == "")
                {
                    SceneManager.LoadScene("CharacterSelection");
                }
                else
                {
                    // game hub
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

    private void SyncWithDatabase()
    {
        DatabaseController dbController = FindObjectOfType<DatabaseController>();
        if (dbController != null)
        {
            var player = GameManager.Instance.Player;
            var playerSettings = player.GetSettings();

            // syncing history 
            dbController.SaveSettings(playerSettings.GetSfx(), playerSettings.GetMusic());
            // sycn history
            dbController.SaveHistory(player.GetLevel(), player.GetCoins(), playerSettings.GetSfx(), playerSettings.GetMusic(), player.GetGameLevelIds(), player.GetBagItems());
        }
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
        public string level;
        public string character;
        public int coins;
        public string settings;
        public string history;
        public List<string> gameLevelIds;
        public List<string> bagItems;
    }
    [System.Serializable]
    public class LoginErrorResponse
    {
        public string error;
    }

}