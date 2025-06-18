using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [SerializeField] private Button pauseBtn;

    [Header("UI")]
    [SerializeField] private ConditionUI conditionUI;
    [SerializeField] private SubInventory subInventory;
    [SerializeField] private DialogUI dialogUI;

    public ConditionUI Condition => conditionUI;
    public SubInventory SubInventory => subInventory;
    public DialogUI Dialog => dialogUI;

    private void Start()
    {
        pauseBtn.onClick.AddListener(OnPause);
    }

    private void OnPause()
    {
        UIManager.Instance.Pause.OpenPanel();
    }
}
