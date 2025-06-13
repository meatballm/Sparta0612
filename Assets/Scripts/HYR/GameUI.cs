using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [SerializeField] private Button pauseBtn;

    private void Start()
    {
        pauseBtn.onClick.AddListener(OnPause);
    }

    private void OnPause()
    {
        if (UIManager.Instance?.Pause != null)
        {
            UIManager.Instance.Pause.OpenPanel();
        }
        else
        {
            Debug.LogWarning("PauseUI 참조가 null입니다!");
        }
    }
}
