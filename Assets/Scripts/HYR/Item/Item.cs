using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public ItemData Data { get; private set; }

    // 아이템 개수 (회복 물약, 기타)
    public int Count { get; private set; }

    // 무기일 경우 탄환 수량
    public int Ammo { get; private set; }

    public Item(ItemData data, int count = 1, int ammo = 0)
    {
        Data = data;
        Count = count;
        Ammo = ammo;
    }

    // 탄환 업데이트
    public void SetAmmo(int amount)
    {
        Ammo = amount;
    }

    // 수량 업데이트
    public void AddCount(int amount)
    {
        Count += amount;
    }
}
