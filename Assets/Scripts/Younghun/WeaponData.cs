using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
    AssaultRifle = 0,
    Shotgun,
    SniperRifle
}

[CreateAssetMenu(fileName = "New WeaponData", menuName = "WeaponData")]
public class WeaponData : ScriptableObject
{
    public uint maxAmmo;
    public float fireRate;
    public uint bulletsPerShot;
    public float reloadSpeed;
    public uint damagePerShot;
    public float spread;
    public float range;
    public float bulletSpeed;
    public ushort pierceCount;
    public string targetTag;
}
