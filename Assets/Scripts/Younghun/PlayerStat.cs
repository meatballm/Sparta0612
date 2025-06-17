using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class PlayerStat
{
    [SerializeField] private bool canDodge;
    [SerializeField] private float cooldownDodge;
    public float CooldownDodge
    {
        get => cooldownDodge;
        set => cooldownDodge = Mathf.Clamp(value, 1f, 5f); // 범위 제한
    }

    public float maxHp = 100;
    public float curHp; 

    public void Start()
    {
        curHp = maxHp;
    }

    void Update()
    {
        
    }

    public void HealHp(int amount)
    {
        if (curHp + amount <= maxHp) curHp += amount;
        else curHp = maxHp;
    }

    public void ReduceHp(float amount)
    {
        if (curHp - amount >= 0) 
        {
            curHp -= amount;
            Debug.Log(curHp);
        }

        else 
        {
            curHp = 0;
            Death();
        }
    }

    public void Death()
    {
        Debug.Log("게임오버");
    }
}
