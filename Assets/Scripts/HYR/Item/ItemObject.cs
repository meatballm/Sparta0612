using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    [SerializeField] private ItemData itemData;
    [SerializeField] private GameObject pickupIcon;

    private bool isPlayerNearby = false;

    private void Start()
    {
        if (pickupIcon != null)
            pickupIcon.SetActive(false);
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
}
