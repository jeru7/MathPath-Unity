using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class LoginValidator : MonoBehaviour 
{
    public TMP_InputField username, password;
    public GameObject passwordError, usernameError, passwordEmpty, usernameEmpty; 

    private string loginUrl = "url sa mongodb end point";

    public void OnLogin()
    {
        ResetError();
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

    public void CheckField()
    {
        if (string.IsNullOrEmpty(username.text) && string.IsNullOrEmpty(password.text))
        {
            usernameEmpty.SetActive(true);
            passwordEmpty.SetActive(true);
        } else if (string.IsNullOrEmpty(username.text))
        {
            usernameEmpty.SetActive(true);
            return;
        } else if (string.IsNullOrEmpty(password.text))
        {
            passwordEmpty.SetActive(true);
            return;
        }
    }

    public void ResetError() {
        passwordError.SetActive(false);
        usernameError.SetActive(false);
        passwordEmpty.SetActive(false);
        usernameEmpty.SetActive(false);
    }
}