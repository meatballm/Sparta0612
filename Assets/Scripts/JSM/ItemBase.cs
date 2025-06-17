using UnityEngine;

public abstract class ItemBase : ScriptableObject
{
    public string itemName;
    public Sprite icon;
    public string description;

    // 아이템 효과 적용 (필수 구현)
    public abstract void ApplyEffect(GameObject user);
}
