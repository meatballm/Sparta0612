using UnityEngine;

[RequireComponent(typeof(Collider2D), typeof(SpriteRenderer))]
public class Chest : MonoBehaviour
{
    public string playerTag = "Player";
    public KeyCode interactKey = KeyCode.E;

    [Header("아이템 프리팹")]
    public GameObject itemPrefab;

    [Header("열린 모습 스프라이트")]
    public Sprite openedSprite;

    private bool isPlayerNear = false;
    private bool isOpened = false;
    private SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (isPlayerNear && !isOpened && Input.GetKeyDown(interactKey))// 후에 캐릭터에 상호작용키 할당시 수정 필요
        {
            OpenChest();
        }
    }

    void OpenChest()
    {
        isOpened = true;

        if (openedSprite != null)
            sr.sprite = openedSprite;

        if (itemPrefab != null)
            Instantiate(itemPrefab, transform.position + Vector3.up * 0.5f, Quaternion.identity); // 위에 살짝 떠서 생성

        Debug.Log("상자 열림!");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
            isPlayerNear = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
            isPlayerNear = false;
    }
}
