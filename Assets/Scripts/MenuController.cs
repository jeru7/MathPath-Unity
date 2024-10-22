using UnityEngine;

public class MenuController : MonoBehaviour
{
    public GameObject titleScreenPanel;
    public GameObject settingsPanel;

    public void Start() {
        titleScreenPanel.SetActive(true);
        settingsPanel.SetActive(false);
    }

    public void StartGame()
    {

    }

    public void ShowSettings()
    {
        titleScreenPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        titleScreenPanel.SetActive(true);
        settingsPanel.SetActive(false);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
