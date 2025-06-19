using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Weapon,
    Consumable,
    Etc
}

public enum ConsumableType
{
    Heal,
    SpeedUp
}

[CreateAssetMenu(fileName = "NewItem", menuName = "Item/ItemData")]

public class ItemData : ScriptableObject
{
    [Header("Weapon")]
    public WeaponData weaponData;

    [Header("Default")]
    public string itemName;
    public Sprite itemIcon;
    public ItemType itemType;
    public string description;
    public int defaultCount;

    [Header("Stacking")]
    public bool canStack;
    public int maxStackAmount;

    [Header("Ammo")]
    public int defaultAmmo;

    [Header("Consumable")]
    public ConsumableType consumableType;
    public int healAmount;
    public float speedUpDuration;
    public float speedUpValue;
}
