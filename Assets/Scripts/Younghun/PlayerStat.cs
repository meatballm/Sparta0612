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

    public int maxHp = 100;
    public int curHp; 

    // Start is called before the first frame update
    void Start()
    {
        curHp = maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HealHp(int amount)
    {
        if (curHp + amount <= maxHp) curHp += amount;
        else curHp = maxHp;
    }

    public void ReduceHp(int amount)
    {
        if (curHp - amount >= 0) curHp -= amount;
        else curHp = 0;
    }

    public void Death()
    {
        if (curHp == 0)
        {

        }
    }
}
