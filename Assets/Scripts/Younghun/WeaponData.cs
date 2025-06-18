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
    public uint maxAmmo;             // 최대 장탄 수
    public float fireRate;           // 초당 발사 속도
    public uint bulletsPerShot;      // 한 번에 나가는 탄 수
    public float reloadSpeed;        // 재장전 시간
    public float spread;             // 퍼짐 각도 (탄 퍼짐 범위)
    public string targetTag;      
}
