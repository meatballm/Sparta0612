using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance {  get; private set; }

    // 각 UI에 연결
    [SerializeField] private StartUI startUI;
    [SerializeField] private GameUI gameUI;
    [SerializeField] private PauseUI pauseUI;

    // 외부 접근용 프로퍼티
    public GameUI Game => gameUI;
    public PauseUI Pause => pauseUI;
    public StartUI Start => startUI;

    //[Header("UI요소들")]
    //[SerializeField] private GameObject mainMenu;
    //[SerializeField] private GameObject settings;
    //[SerializeField] private GameObject gameUI;
    //[SerializeField] private GameObject endPanel;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
            Destroy(gameObject);
    }

    //public void MainMenu()
    //{
    //    mainMenu.SetActive(true);
    //    settings.SetActive(false);
    //    gameUI.SetActive(false);
    //    endPanel.SetActive(false);
    //}
}
