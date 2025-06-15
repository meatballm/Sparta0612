using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [SerializeField] private Button pauseBtn;

    [SerializeField] private ConditionUI conditionUI;
    [SerializeField] private SubInventory subInventory;

    public ConditionUI Condition => conditionUI;
    public SubInventory SubInventory => subInventory;

    private void Start()
    {
        pauseBtn.onClick.AddListener(OnPause);
        //Debug.Log("SubInventory 연결됨: " + (subInventory != null));
    }

    private void OnPause()
    {
        UIManager.Instance.Pause.OpenPanel();
    }
}
