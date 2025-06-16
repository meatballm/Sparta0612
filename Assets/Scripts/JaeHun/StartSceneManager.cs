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
        storyText.text = "깊고 어두운 지하 던전...\r\n전설에 따르면 이곳에 숨겨진 보물은" +
            "\r\n모든 욕망을 실현시킨다고 한다.\r포인트 받고 10조!\r\n이제 모험이 시작된다.\r\n";
    }

    void OnStartClicked()
    {
        string selectedCharacter = characterDropdown.options[characterDropdown.value].text;
        PlayerPrefs.SetString("CharacterType", selectedCharacter);

        SceneManager.LoadScene("MainGameScene");
    }
}
