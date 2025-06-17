using UnityEngine;

[CreateAssetMenu(menuName = "Items/PassiveItem")]
public class PassiveItem : ItemBase
{
    public float bonusMaxHP = 20f;
    public float bonusMoveSpeed = 1f;

    public override void ApplyEffect(GameObject user)
    {
        var stats = user.GetComponent<PlayerController>()?.stats;
        if (stats == null) return;

        //stats.maxHp += bonusMaxHP;
        stats.CooldownDodge -= bonusMoveSpeed;
        Debug.Log($"패시브 적용: HP+{bonusMaxHP}, 이동속도+{bonusMoveSpeed}");
    }
}
