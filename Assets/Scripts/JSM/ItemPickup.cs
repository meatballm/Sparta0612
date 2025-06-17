using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public ItemBase item;

    private bool isPlayerNear = false;
    private GameObject currentPlayer;

    private void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E))
        {
            item.ApplyEffect(currentPlayer);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
            currentPlayer = other.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            currentPlayer = null;
        }
    }
}
