using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New StatData", menuName = "Stats/Chracter Stats")]
public class Characters : ScriptableObject
{
    // 플레이어와 몬스터 공통 스탯
    // 체력, 이동속도, 공격력
    public int health;
    public float moveSpeed;
    public float attackDamage;
}
