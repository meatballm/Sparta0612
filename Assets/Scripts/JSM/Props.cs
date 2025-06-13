using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Prop : MonoBehaviour
{
    [Header("충돌 시 파괴할 대상 태그 목록")]
    public string[] destroyTags;

    [Header("파괴 이펙트 프리팹")]
    public GameObject impactEffectPrefab;

    [Header("이펙트 자동 삭제 시간")]
    public float effectLifeTime = 2f;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        var contact = collision.GetContact(0);
        TryDestroy(collision.collider.gameObject, contact.point, contact.normal);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        TryDestroy(other.gameObject, other.transform.position, Vector2.up);
    }

    private void TryDestroy(GameObject other, Vector2 spawnPos, Vector2 spawnNormal)
    {
        foreach (var tag in destroyTags)
        {
            if (other.CompareTag(tag))
            {
                if (impactEffectPrefab != null)
                {
                    var rot = Quaternion.FromToRotation(Vector3.up, spawnNormal);
                    var fx = Instantiate(impactEffectPrefab, spawnPos, rot);
                    Destroy(fx, effectLifeTime);
                }
                Destroy(gameObject);
                break;
            }
        }
    }
}
