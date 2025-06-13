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

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
            Destroy(gameObject);
    }
}
