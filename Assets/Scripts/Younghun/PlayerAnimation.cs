using UnityEngine;
using UnityEngine.InputSystem;
using System;

[RequireComponent(typeof(Animator))]
public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput; // Input System 참조
    private Animator animator;

    private Vector2 moveInput;
    private Vector2 lastDirection;

    [SerializeField] private PlayerStat stat;
    private bool isDeath;

    private void Awake()
    {
        animator = GetComponent<Animator>();

        // 이벤트 등록
        if (stat != null)
        {
            stat.OnDeath += Death;
        }

        if (playerInput == null)
        {
            playerInput = GetComponent<PlayerInput>();
        }
    }

    private void Update()
    {
        if (isDeath == true) return;
        
        // 현재 입력값 읽기 (Input System)
        moveInput = playerInput.actions["Move"].ReadValue<Vector2>().normalized;

        bool isMoving = moveInput.sqrMagnitude > 0.01f;

        animator.SetBool("IsMoving", isMoving);
        animator.SetFloat("MoveX", moveInput.x);
        animator.SetFloat("MoveY", moveInput.y);

        if (isMoving)
        {
            // 이동 중일 때 마지막 방향 갱신
            lastDirection = moveInput;

            animator.SetFloat("LastX", moveInput.x);
            animator.SetFloat("LastY", moveInput.y);
        }
    }

    private void Death()
    {
        isDeath = true;
        animator.SetTrigger("IsDeath");
    }

    private void OnDestroy()
    {
        // 이벤트 해제
        if (stat != null)
        {
            stat.OnDeath -= Death;
        }
    }
}
