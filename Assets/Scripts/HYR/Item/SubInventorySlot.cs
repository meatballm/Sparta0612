using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SubInventorySlot : MonoBehaviour
{
    [SerializeField] private Image itemIcon;
    [SerializeField] private TextMeshProUGUI countText;
    [SerializeField] private Image highlight;

    private Item item;


    // 슬롯 버튼 이벤트 등록(클릭 선택)
    private void Start()
    {
        GetComponent<Button>()?.onClick.AddListener(OnClick);
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

            SetSelected(false);
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
        SubInventory inventory = GetComponentInParent<SubInventory>();
        if (inventory == null) return;

        int index = transform.GetSiblingIndex();
        inventory.SelectSlot(index);
    }
}
