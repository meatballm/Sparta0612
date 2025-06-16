using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Snail : Enemy
{
    public override void MoveTowards()
    {
        Debug.Log("무브");
    }
    public override void Attack()
    {
        Debug.Log("Snail 공격");
    }
}
