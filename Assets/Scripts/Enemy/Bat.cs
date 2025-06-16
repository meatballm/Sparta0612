using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : Enemy
{
    float speed = 3f;
    public override void Attack()
    {


        Debug.Log("bat공격");
    }

    public override void MoveTowards()
    {
        base.MoveTowards();
    }
}
