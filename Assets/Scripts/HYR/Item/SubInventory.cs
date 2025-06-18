using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using DG.Tweening.Core.Easing;
using UnityEngine.EventSystems;
using System.Linq;
using static UnityEditor.Progress;

public class SubInventory : MonoBehaviour
{
    [SerializeField] private RectTransform panel;
    [SerializeField] private Transform slotParent;
    [SerializeField] private List<SubInventorySlot> slots;

    // 인벤토리 위치 설정(두트윈)
    [SerializeField] private float visibleY = 5f; // 초기 위치
    [SerializeField] private float hiddenY = -115f; // 화면 밖 위치
    [SerializeField] private float duration = 0.3f;
    
    private List<Item> items = new List<Item>();
    private int currentSelectedIndex = -1;
    private bool isVisible = true;

    private bool isTabLocked = false;

    public bool HasItem(string itemName)
    {
        if (items == null) return false;
        return items.Any(i => i.Data.itemName == itemName);
    }

    private void Start()
    {
        panel.anchoredPosition = new Vector2(panel.anchoredPosition.x, visibleY);

        foreach (Transform child in slotParent)
        {
            SubInventorySlot slot = child.GetComponent<SubInventorySlot>();
            if (slot != null) slots.Add(slot);
        }
    }

    private void Update()
    {
        // Tab키로 열고 닫을 때만 고정
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            isTabLocked = !isTabLocked;
            AnimatePanel(isTabLocked ? visibleY : hiddenY);
        }

        // 1~6으로 슬롯 선택
        for (int i = 0; i < slots.Count; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                SelectSlot(i);
            }
        }
    }

    private void AnimatePanel(float yPos)
    {
        panel.DOAnchorPosY(yPos, duration).SetEase(Ease.OutQuad);
    }

    public void AddItem(Item item) // 획득 아이템 반영
    {
        var existing = items.FirstOrDefault(i => i.Data == item.Data);
        if (existing != null)
        {
            existing.AddCount(item.Count);
        }
        else
        {
            items.Add(item);
        }
        SortItems();
        RefreshUI();
    }

    private void SortItems()
    {
        var originalOrder = items.ToList(); // 순서 저장

        items = items
            .OrderByDescending(i => i.Data.itemType == ItemType.Weapon) // 무기 우선
            .ThenBy(i => items.IndexOf(i)) // 원래 순서 유지 > 정렬 기준이 같을 경우 순서 유지
            .ToList();
    }

    private void RefreshUI()
    {
        int displayCount = Mathf.Min(slots.Count, items.Count);
        for (int i = 0; i < displayCount; i++)
        {
            slots[i].SetData(items[i]);
        }

        for (int i = displayCount; i < slots.Count; i++)
        {
            slots[i].Clear();
        }
    }

    public void SelectSlot(int index)
    {
        if (index >= items.Count) return;
        var item = items[index];
        currentSelectedIndex = index;
        UpdateSlotHighlight();

        if (item.Data.itemType == ItemType.Weapon)
            EquipWeapon(item);
        else if (item.Data.itemType == ItemType.Consumable)
            UseConsumable(item);
    }

    private void UseConsumable(Item item)
    {
        var player = FindObjectOfType<PlayerController>();

        // 효과 적용
        if (item.Data.consumableType == ConsumableType.Heal)
        {
            player.stats.HealHp(item.Data.healAmount);
            Debug.Log($"{item.Data.itemName} 사용 체력 +{item.Data.healAmount}");
        }
        else if (item.Data.consumableType == ConsumableType.SpeedUp)
        {
            player.BuffMoveSpeed(item.Data.speedUpValue, item.Data.speedUpDuration);
            Debug.Log($"{item.Data.itemName} 사용 이동속도 증가");
        }

        item.AddCount(-1);
        if (item.Count <= 0)
            items.Remove(item);
        RefreshUI();
    }

    private void EquipWeapon(Item weapon)
    {
        // 무기 장착 호출
        //GameManager.Instance.Player.EquipWeapon(weapon);
    }

    private void UpdateSlotHighlight()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            bool active = (i == currentSelectedIndex);
            slots[i].SetSelected(active);
        }
    }
}
