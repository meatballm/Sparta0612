using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SubInventorySlot : MonoBehaviour
{
    [SerializeField] private Image itemIcon;
    [SerializeField] private TextMeshProUGUI selectText;
    [SerializeField] private TextMeshProUGUI countText;
    [SerializeField] private Image highlight;

    private Item item;

    private void Awake()
    {
        // 인덱스 기반으로 숫자 1부터
        selectText.text = (transform.GetSiblingIndex() + 1).ToString();

        // 초기 상태! 아이콘, 개수, 하이라이트 비활성화
        if (itemIcon != null) itemIcon.enabled = false;
        if (countText != null) countText.enabled = false;
        if (highlight != null) highlight.enabled = false;
    }

    // 슬롯 버튼 이벤트 등록(클릭 선택)
    private void Start()
    {
        GetComponent<Button>()?.onClick.AddListener(OnClick);
        highlight.enabled = false;
    }

    public void SetData(Item data)
    {
        item = data;

        if (item != null && item.Data != null)
        {
            itemIcon.enabled = true;
            itemIcon.sprite = item.Data.itemIcon;

            // 무기일 경우: 탄환 수, 기타 아이템은 개수 표시
            countText.text = item.Data.itemType == ItemType.Weapon
            ? item.Ammo.ToString()
            : item.Count.ToString();

            countText.enabled = true;
            highlight.enabled = false;
        }
        else
        {
            Clear();
        }
    }

    public void Clear()
    {
        item = null;
        itemIcon.enabled = false;
        countText.enabled = false;
        countText.text = "";
        highlight.enabled = false;
    }

    public void SetSelected(bool isSelected)
    {
        if (highlight != null)
            highlight.enabled = isSelected;
    }

    public Item GetItem() => item;

    public void OnClick()
    {
        if (item == null || item.Data == null)
            return;

        // 인벤토리 찾아서 선택 슬롯 인덱스 전달
        var inventory = GetComponentInParent<SubInventory>();
        if (inventory == null)
            return;

        int index = transform.GetSiblingIndex();
        inventory.SelectSlot(index);

        SetSelected(true);
    }
}
