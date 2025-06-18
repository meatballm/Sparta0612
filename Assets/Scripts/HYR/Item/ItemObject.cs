using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    [SerializeField] private ItemData itemData;
    [SerializeField] private GameObject pickupIcon;
    private RectTransform iconRect;

    [Header("Weapon")]
    public WeaponData weaponData;

    private bool isPlayerNearby = false;

    private void Start()
    {
        if (pickupIcon != null)
        {
            pickupIcon.SetActive(false);
            iconRect = pickupIcon.GetComponent<RectTransform>();
        }
    }

    private void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            PickupItem();
        }
    }

    private void PickupItem()
    {
        Debug.Log($"아이템 획득: {itemData.itemName}");

        // 아이템 획득
        SubInventory inventory = UIManager.Instance.Game.SubInventory;
        inventory.AddItem(new Item(itemData));
        AudioManager.Instance.PlaySFX(2);
        Destroy(gameObject); // 획득 후 오브젝트 삭제

        if (pickupIcon != null)
            pickupIcon.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            if (pickupIcon != null)
                pickupIcon.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            if (pickupIcon != null)
                pickupIcon.SetActive(false);
        }
    }

    private void LateUpdate()
    {
        // 플레이어 근처에서 아이콘 표시
        if (isPlayerNearby && pickupIcon != null)
        {
            // 아이템 좌표 변환
            Vector3 worldPos = transform.position + Vector3.up * 0.5f;
            Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
            iconRect.position = screenPos;
        }
    }
}
