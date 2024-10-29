using UnityEngine;

public class MenuController : UIController
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
        SetPanelActive(titleScreen, true);
        SetPanelActive(login, false);
        SetPanelActive(settings, false);
    }

    /// <summary>
    /// Runs every time the start button on the title screen is clicked.
    /// Opens the login page panel.
    /// </summary>
    public void StartGame()
    {
        audioManager.PlaySFX(clickSound);
        SetPanelActive(login, true);
        SetPanelActive(loginForm, true);
        SetPanelActive(titleScreen, false);
    }

    public void CloseLoginForm()
    {
        audioManager.PlaySFX(clickSound);
        SetPanelActive(login, false);
        SetPanelActive(titleScreen, true);
    }

    public void OpenForgotPassForm()
    {
        audioManager.PlaySFX(clickSound);
        SetPanelActive(loginForm, false);
        SetPanelActive(forgotPassForm, true);
    }

    public void CloseForgotPassForm()
    {
        audioManager.PlaySFX(clickSound);
        SetPanelActive(forgotPassForm, false);
        SetPanelActive(loginForm, true);
    }

    /// <summary>
    /// Runs every time the settings button on the title screen is clicked.
    /// Opens the settings panel.
    /// </summary>
    public void OpenSettings()
    {
        audioManager.PlaySFX(clickSound);
        SetPanelActive(settings, true);
        SetPanelActive(titleScreen, false);
    }

    /// <summary>
    /// Runs everytime the exit button on settings panel is clicked.
    /// Closes the settings panel.
    /// Opens the title screen panel.
    /// </summary>
    public void CloseSettings()
    {
        audioManager.PlaySFX(clickSound);
        SetPanelActive(settings, false);
        SetPanelActive(titleScreen, true);
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