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
    //[SerializeField] private DamageUI damageUI;

    public ConditionUI Condition => conditionUI;
    public SubInventory SubInventory => subInventory;
    public DialogUI Dialog => dialogUI;
    //public DamageUI Damage => damageUI;

    private void Start()
    {
        pauseBtn.onClick.AddListener(OnPause);
    }

    private void OnPause()
    {
        UIManager.Instance.Pause.OpenPanel();
    }
}
