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
    public AudioClip clickSound;

    private string loginUrl = "http://localhost:3000/auth/login";

    public void OnLogin()
    {
        audioManager.PlaySFX(clickSound);
        ResetError();
        if (!IsValidField()) return;
        StartCoroutine(LoginCoroutine(username.text, password.text));
    }

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
                Debug.Log($"Username: {username} and Password: {password}");
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

    public void ResetError() {
        passwordError.SetActive(false);
        usernameError.SetActive(false);
        passwordEmpty.SetActive(false);
        usernameEmpty.SetActive(false);
    }
}