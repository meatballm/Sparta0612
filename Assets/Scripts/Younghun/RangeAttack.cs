using System.Collections;
using UnityEngine;

// 무기를 발사하고, Bullet을 초기화하여 날림
// WeaponData의 설정값과 추가 정보들을 이용함
public class RangeAttack : MonoBehaviour
{
    public WeaponData weaponData;            // ScriptableObject로 저장된 무기 데이터

    public GameObject bulletPrefab;          // 발사할 탄환 프리팹
    public Transform firePoint;              // 탄환 발사 위치

    [Header("Bullet Stats")]
    public uint damagePerShot;               // 탄환 당 데미지
    public float bulletSpeed;                // 탄환 속도
    public float bulletRange;                // 탄환 사거리
    public ushort pierceCount;               // 관통 가능 횟수
    public TargetTag targetTag;              // 충돌 대상 태그

    public float fireCooldown;               // 연사 속도
    public uint curAmmo;                     // 현재 탄환 수
    public bool isReloading;                 // 재장전 중인지 확인

    private RangeAttackInstance runtimeStats;

    private void Start()
    {
        // 기준 데이터에서 복사
        runtimeStats = new RangeAttackInstance(weaponData, this);
        curAmmo = weaponData.maxAmmo;
    }

    private void Update()
    {
        fireCooldown -= Time.deltaTime;

        if (Input.GetMouseButton(0) && fireCooldown <= 0f)
        {
            Fire();
            fireCooldown = 1f / weaponData.fireRate;
        }
    }

    private void Fire()
    {
        // 재장전 중일 때는 사격 불가
        if (curAmmo == 0) return;

        for (int i = 0; i < weaponData.bulletsPerShot; i++)
        {
            // 방향에 퍼짐을 적용
            Vector2 direction = GetFireDirectionWithSpread(weaponData.spread);

            GameObject bulletGO = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

            // 위치도 살짝 랜덤 오프셋
            Vector3 offset = firePoint.up * Random.Range(-0.1f, 0.1f);
            Vector3 spawnPos = firePoint.position + offset;

            // 탄환 생성 및 초기화
            Bullet bullet = bulletGO.GetComponent<Bullet>();
            bullet.Initialize(
                damagePerShot,
                bulletSpeed,
                bulletRange,
                pierceCount,
                direction,
                weaponData.targetTag
            );
        }
        curAmmo--;
    }

    private void Reloading()
    {
        if (curAmmo == 0 || Input.GetKeyDown(KeyCode.R))
        {
            isReloading = true;
            
        }
    }

    //IEnumerator 

    // spread 값을 기반으로 랜덤 방향을 생성함
    private Vector2 GetFireDirectionWithSpread(float spread)
    {
        float angle = Random.Range(-spread / 2f, spread / 2f);
        return Quaternion.Euler(0, 0, angle) * transform.right;
    }
}
