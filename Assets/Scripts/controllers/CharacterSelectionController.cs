using UnityEngine;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine.Rendering;

public class CharacterSelectionController : MonoBehaviour
{
    public GameObject selectTitle;
    public GameObject nameTitle;
    public GameObject maleCardContainer;
    public GameObject femaleCardContainer;
    public GameObject promptNameContainer;
    public TMP_InputField characterNameInput;


    private SceneController sceneController;
    private DatabaseController dbController;
    private AudioManager audioManager;
    private Player player;
    private string chosenCharacter;

    /// <summary>
    /// initializes the needed game object instance
    /// </summary>
    private void Start()
    {
        audioManager = AudioManager.Instance;
        player = Player.Instance;
        sceneController = SceneController.Instance;
        dbController = DatabaseController.Instance;

        InitializeMenu();
        audioManager.PlayMusic();
    }

    /// <summary>
    /// initializes the ui display on the character selection
    /// </summary>
    private void InitializeMenu()
    {
        selectTitle.SetActive(true);
        nameTitle.SetActive(false);
        maleCardContainer.SetActive(true);
        femaleCardContainer.SetActive(true);
        promptNameContainer.SetActive(false);

        characterNameInput.text = "";
        chosenCharacter = "";
    }

    /// <summary>
    /// sets the position of the cards based on the clicked character container
    /// </summary>
    /// <param name="cardContainer"></param>
    public void OnCardClick(GameObject cardContainer)
    {
        audioManager.PlayClickSound();

        if (cardContainer == maleCardContainer)
        {
            femaleCardContainer.SetActive(false);
            ShowPromptNameContainer(343.947f);
            chosenCharacter = "male";
        }
        else
        {
            maleCardContainer.SetActive(false);
            ShowPromptNameContainer(-343.947f);
            chosenCharacter = "female";
        }
    }

    /// <summary>
    /// shows and place the prompt name container based on 'pos'
    /// </summary>
    /// <param name="pos"></param>
    private void ShowPromptNameContainer(float pos)
    {
        promptNameContainer.SetActive(true);
        selectTitle.SetActive(false);
        nameTitle.SetActive(true);

        RectTransform rectTransform = promptNameContainer.GetComponent<RectTransform>();

        Vector2 currentPosition = rectTransform.anchoredPosition;
        rectTransform.anchoredPosition = new Vector2(pos, currentPosition.y);
    }

    /// <summary>
    /// resets the ui display after clicking the cancel button on promtp name container
    /// </summary>
    public void OnCancelClick()
    {
        audioManager.PlayClickSound();
        InitializeMenu();
    }

    // TODO: create function for name
    // - must set to player's name and student's name (mongodb)

    /// <summary>
    /// method that runs when the user clicks the create button on prompt name container
    /// </summary>
    public void OnCreateClick()
    {
        audioManager.PlayClickSound();
        IsValidName();

        player.SetCharacter(chosenCharacter);
        player.SetCharacterName(characterNameInput.text.Trim());

        dbController.SetCharacter(player.GetId(), player.GetCharacter(), player.GetCharacterName());

        sceneController.GoToGameMainHub();
    }

    /// TODO: finalize the required name format

    /// <summary>
    /// checks the validity of name that the user entered
    /// </summary>
    /// <returns></returns>
    private bool IsValidName()
    {
        if (characterNameInput.text.Trim() == "")
        {
            Debug.Log("Empty name");
            return false;
        }

        if (characterNameInput.text.Trim().Length < 4)
        {
            Debug.Log("Invalid: Name must have 4 characters or more");
            return false;
        }

        return true;
    }
}