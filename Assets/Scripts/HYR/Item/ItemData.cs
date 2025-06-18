using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Weapon,
    Consumable,
    Passive,
    Etc
}

[CreateAssetMenu(fileName = "NewItem", menuName = "Item/ItemData")]

public class ItemData : ScriptableObject
{
    public string itemName;
    public Sprite itemIcon;
    public ItemType itemType;
    public string description;
    public int defaultCount;

    // 무기
    public int defaultAmmo;
    public int maxAmmo;

    // 물약
    public int restoreAmount;
}
