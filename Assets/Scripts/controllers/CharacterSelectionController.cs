using UnityEngine;

public class CharacterSelectionController : MonoBehaviour
{
    public AudioManager audioManager;
    public GameObject selectTitle;
    public GameObject nameTitle;
    public GameObject maleCardContainer;
    public GameObject femaleCardContainer;
    public GameObject promptNameContainer;

    // TODO: 
    // ui for initial displays
    // ui transition when the character is selected 

    private void Awake()
    {
        InitializeMenu();
        audioManager.PlayMusic();
    }
    private void InitializeMenu()
    {
        selectTitle.SetActive(true);
        nameTitle.SetActive(false);
        maleCardContainer.SetActive(true);
        femaleCardContainer.SetActive(true);
        promptNameContainer.SetActive(false);
    }

    public void OnCardClick(GameObject cardContainer)
    {
        audioManager.PlayClickSound();

        if (cardContainer == maleCardContainer)
        {
            femaleCardContainer.SetActive(false);
            ShowPromptNameContainer(343.947f);
        }
        else
        {
            maleCardContainer.SetActive(false);
            ShowPromptNameContainer(-343.947f);
        }
    }

    private void ShowPromptNameContainer(float pos)
    {
        promptNameContainer.SetActive(true);
        selectTitle.SetActive(false);
        nameTitle.SetActive(true);

        RectTransform rectTransform = promptNameContainer.GetComponent<RectTransform>();

        Vector2 currentPosition = rectTransform.anchoredPosition;
        rectTransform.anchoredPosition = new Vector2(pos, currentPosition.y);
    }

    public void OnCancelClick()
    {
        InitializeMenu();
    }
}