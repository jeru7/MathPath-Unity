using UnityEngine;

public class MenuController : MonoBehaviour
{
    public GameObject titleScreenPanel;
    public GameObject settingsPanel;
    public AudioManager audioManager;
    public AudioClip backgroundMusic;
    public AudioClip clickSound;

    public void Start() {
        titleScreenPanel.SetActive(true);
        settingsPanel.SetActive(false);
        audioManager.PlayMusic(backgroundMusic);
    }

    public void StartGame()
    {

    }

    public void ShowSettings()
    {
        audioManager.PlaySFX(clickSound);
        titleScreenPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        audioManager.PlaySFX(clickSound);
        titleScreenPanel.SetActive(true);
        settingsPanel.SetActive(false);
    }

    public void Exit()
    {
        audioManager.PlaySFX(clickSound);
        Application.Quit();
    }
}
