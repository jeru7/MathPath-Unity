using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void GoToTitleScreen()
    {
        SceneManager.LoadScene("CharacterSelection");
    }

    public void GoToMainHub()
    {
        SceneManager.LoadScene("MainHub");
    }
}
