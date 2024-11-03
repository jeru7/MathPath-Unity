using UnityEngine;

public class MenuController : MonoBehaviour 
{
    public AudioManager audioManager;

    public GameObject titleScreen;
    public GameObject login;
    public GameObject loginForm;
    public GameObject forgotPassForm;
    public GameObject settings;

    /// <summary>
    /// On awake method. Runs every time the title screen starts.
    /// </summary>
    private void Awake()
    {
        InitializeMenu();
        audioManager.PlayMusic();
    }

    /// <summary>
    /// Initializes menu. Opens the title screen (start, settings, exit)
    /// </summary>
    private void InitializeMenu()
    {
        titleScreen.SetActive(true);
        login.SetActive(false);
        settings.SetActive(false);
    }

    /// <summary>
    /// Runs every time the start button on the title screen is clicked.
    /// Opens the login page panel.
    /// </summary>
    public void StartGame()
    {
        audioManager.PlayClickSound();
        login.SetActive(true);
        loginForm.SetActive(true);
        titleScreen.SetActive(false);
    }

    public void CloseLoginForm()
    {
        audioManager.PlayClickSound();
        login.SetActive(false);
        titleScreen.SetActive(true);
    }

    public void OpenForgotPassForm()
    {
        audioManager.PlayClickSound();
        loginForm.SetActive(false);
        forgotPassForm.SetActive(true);
    }

    public void CloseForgotPassForm()
    {
        audioManager.PlayClickSound();
        forgotPassForm.SetActive(false);
        loginForm.SetActive(true);
    }

    /// <summary>
    /// Runs every time the settings button on the title screen is clicked.
    /// Opens the settings panel.
    /// </summary>
    public void OpenSettings()
    {
        audioManager.PlayClickSound();
        settings.SetActive(true);
        titleScreen.SetActive(false);
    }

    /// <summary>
    /// Runs everytime the exit button on settings panel is clicked.
    /// Closes the settings panel.
    /// Opens the title screen panel.
    /// </summary>
    public void CloseSettings()
    {
        audioManager.PlayClickSound();
        settings.SetActive(false);
        titleScreen.SetActive(true);
    }

    /// <summary>
    /// Exits the game.
    /// </summary>
    public void Exit()
    {
        audioManager.PlayClickSound();
        Application.Quit();
    }
} 