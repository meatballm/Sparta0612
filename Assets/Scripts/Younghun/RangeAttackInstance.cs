public class RangeAttackInstance
{
    public uint maxAmmo;
    public float fireRate;
    public uint bulletsPerShot;
    public float reloadSpeed;
    public float spread;

    public uint damagePerShot;
    public float bulletSpeed;
    public float bulletRange;
    public ushort pierceCount;
    public string targetTag;

    public RangeAttackInstance(WeaponData baseData, RangeAttack rangeAttack)
    {
        // WeaponData에서 복사
        maxAmmo = baseData.maxAmmo;
        fireRate = baseData.fireRate;
        bulletsPerShot = baseData.bulletsPerShot;
        reloadSpeed = baseData.reloadSpeed;
        spread = baseData.spread;

        // RangeAttack에서 초기 스탯 넘김
        damagePerShot = rangeAttack.damagePerShot;
        bulletSpeed = rangeAttack.bulletSpeed;
        bulletRange = rangeAttack.bulletRange;
        pierceCount = rangeAttack.pierceCount;
        targetTag = rangeAttack.targetTag;
    }

    // 강화 아이템 적용 함수(추가 예정)
    public void ApplyPlusDamage(uint plus)
    {
        damagePerShot += plus;
    }

    public void ApplyPlusFireRate(float plus)
    {
        fireRate *= plus;
    }
}
