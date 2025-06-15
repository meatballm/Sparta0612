using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using DG.Tweening.Core.Easing;
using UnityEngine.EventSystems;
using System.Linq;

public class SubInventory : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private RectTransform panel;
    [SerializeField] private Transform slotParent;
    [SerializeField] private List<SubInventorySlot> slots;

    // 인벤토리 위치 설정(두트윈)
    [SerializeField] private float visibleY = 5f;
    [SerializeField] private float hiddenY = -115f;
    [SerializeField] private float duration = 0.3f;

    [SerializeField] private float hoverDelay = 0.3f;
    
    private List<Item> items = new List<Item>();
    private int currentSelectedIndex = -1;

    private bool isMouseOver = false;
    private bool isTabLocked = false;
    private Coroutine hoverCoroutine;

    private void Start()
    {
        panel.anchoredPosition = new Vector2(panel.anchoredPosition.x, hiddenY); // 초기 상태에는 창 숨김

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

        // Tab으로 서브 인벤토리를 고정한 상황이 아닐 때
        // 마우스 커서O 보임
        if (isMouseOver && !isTabLocked)
        {
            AnimatePanel(visibleY);
        }

        // 마우스 커서X 숨김
        if (!isMouseOver && !isTabLocked)
        {
            AnimatePanel(hiddenY);
        }
    }

    private void AnimatePanel(float yPos)
    {
        panel.DOAnchorPosY(yPos, duration).SetEase(Ease.OutQuad);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isMouseOver = true;

        if (!isTabLocked)
        {
            if (hoverCoroutine != null) StopCoroutine(hoverCoroutine);
            hoverCoroutine = StartCoroutine(ShowInventoryWithDelay());
        }
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        isMouseOver = false;

        if (!isTabLocked)
        {
            if (hoverCoroutine != null) StopCoroutine(hoverCoroutine);
            AnimatePanel(hiddenY);
        }
    }

    private IEnumerator ShowInventoryWithDelay()
    {
        yield return new WaitForSeconds(hoverDelay);

        if (isMouseOver && !isTabLocked)
        {
            AnimatePanel(visibleY);
        }

        hoverCoroutine = null;
    }

    public void AddItem(Item item)
    {
        items.Add(item);
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
        for (int i = 0; i < slots.Count; i++)
        {
            if (i < items.Count)
                slots[i].SetData(items[i]);
            else
                slots[i].Clear();
        }
    }

    public void SelectSlot(int index)
    {
        if (index >= items.Count) return;

        var item = items[index];
        if (item.Data.itemType == ItemType.Weapon)
        {
            EquipWeapon(item);
        }

        currentSelectedIndex = index;
        UpdateSlotHighlight();
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
            slots[i].SetSelected(i == currentSelectedIndex);
        }
    }
}
