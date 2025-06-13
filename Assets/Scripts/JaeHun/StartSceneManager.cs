using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartSceneManager : MonoBehaviour
{
    public Text storyText;
    public Dropdown characterDropdown;
    public Button startButton;

    void Start()
    {
        ShowStoryText();
        startButton.onClick.AddListener(OnStartClicked);
    }

    void ShowStoryText()
    {
        storyText.text = "...";
    }

    void OnStartClicked()
    {
        string selectedCharacter = characterDropdown.options[characterDropdown.value].text;
        PlayerPrefs.SetString("CharacterType", selectedCharacter);

        SceneManager.LoadScene("MainGameScene");
    }
}
