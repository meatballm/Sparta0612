using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Animator))]
public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput; // Input System 참조
    private Animator animator;

    private Vector2 moveInput;
    private Vector2 lastDirection;

    private void Awake()
    {
        animator = GetComponent<Animator>();

        if (playerInput == null)
        {
            playerInput = GetComponent<PlayerInput>();
        }
    }

    private void Update()
    {
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
}
