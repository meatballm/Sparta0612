using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartUI : MonoBehaviour
{
    [SerializeField] private Button startBtn;
    [SerializeField] private Button settingsBtn;
    [SerializeField] private Button quitBtn;

    private void Start()
    {
        startBtn.onClick.AddListener(OnStart);
        settingsBtn.onClick.AddListener(OnSettings);
        quitBtn.onClick.AddListener(OnQuit);
    }

    private void OnStart()
    {
        SceneManager.LoadScene("TownMapScene");
        AudioManager.Instance.PlayBGM(5);
    }

    private void OnSettings()
    {
        
    }

    private void OnQuit()
    {
        Application.Quit();
    }
}
