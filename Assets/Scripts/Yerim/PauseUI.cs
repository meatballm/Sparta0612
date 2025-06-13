using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseUI : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject outline;
    [SerializeField] private Button pauseBtn;
    [SerializeField] private Button continueBtn;
    [SerializeField] private Button settingsBtn;
    [SerializeField] private Button mainMenuBtn;

    private void Start()
    {
        pausePanel.SetActive(false);
        outline.SetActive(false);

        // 버튼 연결
        pauseBtn.onClick.AddListener(() => PanelOpen());
        continueBtn.onClick.AddListener(OnContinue);
        settingsBtn.onClick.AddListener(OnSettings);
        mainMenuBtn.onClick.AddListener(OnMainMenu);
    }

    public void PanelOpen()
    {
        Time.timeScale = 0f; // 일시 정지
        pausePanel.SetActive(true);
        outline.SetActive(true);
    }

    public void PanelClose()
    {
        pausePanel.SetActive(false);
        outline.SetActive(false);
        Time.timeScale = 1f; // 게임 재개
    }

    private void OnContinue()
    {
        PanelClose();
    }

    private void OnSettings()
    {

    }

    private void OnMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("StartScene"); // 씬 전환
    }
}
