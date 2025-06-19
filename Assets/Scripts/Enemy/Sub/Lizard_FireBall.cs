using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lizard_FireBall : MonoBehaviour
{
    public float speed = 5f;
    public int damage = 20;
    private Vector2 direction;
    PlayerStat playerStat;

    public void Start()
    {
        playerStat = FindObjectOfType<PlayerController>().stats;
        Destroy(gameObject, 3f);
    }
    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerStat.ReduceHp(damage);
            Destroy(gameObject); // 플레이어와 부딪히면 제거
        }
    }
}
