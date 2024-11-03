using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class LoginValidator : MonoBehaviour 
{
    public TMP_InputField username, password;
    public GameObject passwordError, usernameError, passwordEmpty, usernameEmpty; 
    public AudioManager audioManager;

    // api end point
    private string loginUrl = "http://localhost:3000/auth/login";

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

            Debug.Log(request.result);

            if(request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Login successful: " + request.downloadHandler.text);
                ResetError();
            } else {
                Debug.LogError("Login failed: " + request.downloadHandler.text);

                if(request.downloadHandler.text.Contains("Invalid username"))
                {
                    usernameError.SetActive(true);
                } else if(request.downloadHandler.text.Contains("Invalid password")){
                    passwordError.SetActive(true);
                }
            }
        }
    }

    /// <summary>
    /// check whether the input fields are empty
    /// </summary>
    /// <returns></returns>
    public bool IsValidField()
    {
        if (string.IsNullOrEmpty(username.text) && string.IsNullOrEmpty(password.text))
        {
            usernameEmpty.SetActive(true);
            passwordEmpty.SetActive(true);
            return false;
        } else if (string.IsNullOrEmpty(username.text))
        {
            usernameEmpty.SetActive(true);
            return false;
        } else if (string.IsNullOrEmpty(password.text))
        {
            passwordEmpty.SetActive(true);
            return false;
        }
        return true;
    }

    /// <summary>
    /// a function that simply resets the error ui indicator to false - defaults to inactive 
    /// </summary>
    public void ResetError() {
        passwordError.SetActive(false);
        usernameError.SetActive(false);
        passwordEmpty.SetActive(false);
        usernameEmpty.SetActive(false);
    }
}