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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
