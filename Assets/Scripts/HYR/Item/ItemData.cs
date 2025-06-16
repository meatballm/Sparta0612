using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Weapon,
    Consumable,
    Etc
}

[CreateAssetMenu(fileName = "NewItem", menuName = "Item/ItemData")]

public class ItemData : ScriptableObject
{
    public string itemName;
    public Sprite itemIcon;
    public ItemType itemType;
    public int defaultAmmo;
    public int defaultCount;

    // 무기
    public int maxAmmo;

    // 물약
    public int restoreAmount;
}
