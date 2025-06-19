using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public ItemData Data { get; private set; }
    public int Count { get; private set; }

    // 무기일 경우만 WeaponData 반환 (아닐 때는 null)
    public WeaponData WeaponData => Data is WeaponData wd ? wd : null;

    // 탄환 등 무기 관련 수치도 옵션으로
    public int Ammo { get; private set; }

    public Item(ItemData data, int count = 1, int ammo = 0)
    {
        Data = data;
        Count = count;
        Ammo = ammo;
    }

    public void SetAmmo(int amount)
    {
        Ammo = amount;
    }
    public void AddCount(int amount)
    {
        Count += amount;
    }
}
