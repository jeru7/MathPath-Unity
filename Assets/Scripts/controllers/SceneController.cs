using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void GoToTitleScreen()
    {
        SceneManager.LoadScene("CharacterSelection");
    }

    public void GoToMainHub()
    {
        SceneManager.LoadScene("MainHub");
    }
}
