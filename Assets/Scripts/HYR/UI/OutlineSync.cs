using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineSync : MonoBehaviour
{
    [SerializeField] private SpriteRenderer targetRenderer; // Sprites 본체 오브젝트
    private SpriteRenderer outlineRenderer;

    void Awake()
    {
        outlineRenderer = GetComponent<SpriteRenderer>();
    }

    void LateUpdate()
    {
        if (targetRenderer != null && outlineRenderer != null)
        {
            outlineRenderer.sprite = targetRenderer.sprite; // 프레임마다 sprite 복사
        }
    }
}
