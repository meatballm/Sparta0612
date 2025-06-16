using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lizard_FireBall : MonoBehaviour
{
    public float speed = 5f;
    public int damage = 10;
    Enemy enemy;

    private Vector2 direction;

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
            //Character.hp - damage;
            Destroy(gameObject); // 플레이어와 부딪히면 제거
        }
        else
        {
            Destroy(gameObject); // 벽, 장애물에 부딪히면 제거
        }
    }
}
