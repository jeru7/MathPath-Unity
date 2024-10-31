using UnityEngine;

public class MenuController : MonoBehaviour 
{
    public AudioManager audioManager;

    public GameObject titleScreen;
    public GameObject login;
    public GameObject loginForm;
    public GameObject forgotPassForm;
    public GameObject settings;

    public AudioClip backgroundMusic;
    public AudioClip clickSound;

    /// <summary>
    /// On awake method. Runs every time the title screen starts.
    /// </summary>
    private void Awake()
    {
        InitializeMenu();
        audioManager.PlayMusic(backgroundMusic);
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
        audioManager.PlaySFX(clickSound);
        login.SetActive(true);
        loginForm.SetActive(true);
        titleScreen.SetActive(false);
    }

    public void CloseLoginForm()
    {
        audioManager.PlaySFX(clickSound);
        login.SetActive(false);
        titleScreen.SetActive(true);
    }

    public void OpenForgotPassForm()
    {
        audioManager.PlaySFX(clickSound);
        loginForm.SetActive(false);
        forgotPassForm.SetActive(true);
    }

    public void CloseForgotPassForm()
    {
        audioManager.PlaySFX(clickSound);
        forgotPassForm.SetActive(false);
        loginForm.SetActive(true);
    }

    /// <summary>
    /// Runs every time the settings button on the title screen is clicked.
    /// Opens the settings panel.
    /// </summary>
    public void OpenSettings()
    {
        audioManager.PlaySFX(clickSound);
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
        audioManager.PlaySFX(clickSound);
        settings.SetActive(false);
        titleScreen.SetActive(true);
    }

    /// <summary>
    /// Exits the game.
    /// </summary>
    public void Exit()
    {
        audioManager.PlaySFX(clickSound);
        Application.Quit();
    }
} 