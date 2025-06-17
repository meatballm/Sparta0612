using UnityEngine;

[CreateAssetMenu(menuName = "Items/ConsumableItem")]
public class ConsumableItem : ItemBase
{
    public int healAmount = 20;

    public override void ApplyEffect(GameObject user)
    {
        var stats = user.GetComponent<PlayerController>()?.stats;
        if (stats == null) return;

        stats.HealHp(healAmount);
        Debug.Log($"소비 아이템 사용: 체력 +{healAmount}");
    }
}
