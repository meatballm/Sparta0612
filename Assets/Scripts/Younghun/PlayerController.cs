using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Camera _camera; // 마우스 위치를 월드 좌표로 변환하기 위한 메인 카메라 참조

    public PlayerStat stats = new PlayerStat();
    [SerializeField] private PlayerAnimation playerAnimation;

    [Header("Dodge")]
    public bool canDodge;         // 회피 가능 여부
    public bool isInvincible;            // 회피 무적 적용 여부
    public float dodgeSpeed;             // 회피 속도
    public float dodgeTime;              // 회피 시간
    public Vector2 dodgeDirection;       // 회피 방향
    private Vector2 originalVelocity;    // 회피 전 속도 저장용

    public static PlayerController Instance;

    protected Rigidbody2D _rigidbody; // 이동을 위한 물리 컴포넌트

    [SerializeField] private SpriteRenderer weaponRenderer; // 좌우 반전을 위한 렌더러
    [SerializeField] private Transform weaponPivot; // 무기를 회전시킬 기준 위치

    private Vector2 movementDirection = Vector2.zero; // 현재 이동 방향
    private Vector2 lookDirection = Vector2.zero; // 현재 바라보는 방향

    [SerializeField] private float moveSpeed;

    private Coroutine speedBuffCoroutine;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();

        //if (weaponRenderer == null) weaponRenderer = GetComponentInChildren<SpriteRenderer>();

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject); // 중복 생성 방지
        }
    }

    protected void Start()
    {
        _camera = Camera.main;
        stats.Start();
        playerAnimation.SetStat(stats);
        canDodge = true;
    }

    void FixedUpdate()
    {
        Movment(movementDirection);
    }

    protected void Update()
    {
        Rotate(lookDirection);
        UpdateLookDirection();
        stats.Updatd();

        // 회피 키 입력 처리
        if (this.canDodge && Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("회피키 입력 확인");
            Debug.Log("canDodge 상태 확인");
            if (stats.curStamina > stats.consumeStamina)
            {
                Debug.Log($"{stats.curStamina} / {stats.consumeStamina}");

                Vector2 inputDir = movementDirection;

                // 입력 없으면 마지막 방향 또는 앞방향 사용
                if (inputDir != Vector2.zero) dodgeDirection = inputDir;
                Debug.Log(dodgeDirection);

                StartCoroutine(Dodge());

            }
        }
    }

    private IEnumerator Dodge()
    {
        canDodge = false;

        originalVelocity = _rigidbody.velocity;
        _rigidbody.velocity = dodgeDirection * dodgeSpeed;

        yield return new WaitForSeconds(dodgeTime);

        _rigidbody.velocity = originalVelocity;
        canDodge = true;

        stats.ReduceStamina(stats.consumeStamina);
    }

    private void Movment(Vector2 direction)
    {
        direction = direction * moveSpeed; // 이동 속도

        // 실제 물리 이동
        _rigidbody.velocity = direction;
    }

    private void Rotate(Vector2 direction)
    {
        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        bool isLeft = Mathf.Abs(rotZ) > 90f;

        if (weaponPivot != null)
        {
            // 무기 회전 처리
            weaponPivot.rotation = Quaternion.Euler(0, 0, rotZ);
            weaponRenderer.flipY = isLeft;
        }
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            movementDirection = context.ReadValue<Vector2>();
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            movementDirection = Vector2.zero;
        }
    }

    private void UpdateLookDirection()
    {
        Vector2 screenPos = Vector2.zero;
        bool validInput = false;

        if (Mouse.current != null)
        {
            screenPos = Mouse.current.position.ReadValue();
            validInput = true;
        }

        if (!validInput || _camera == null || screenPos == Vector2.zero) return;

        // 화면 좌표를 월드 좌표로 변환
        Vector3 screenPos3D = new Vector3(screenPos.x, screenPos.y, _camera.WorldToScreenPoint(transform.position).z);
        Vector3 worldPos = _camera.ScreenToWorldPoint(screenPos3D);

        // 캐릭터 위치 기준 방향 벡터 계산
        Vector2 direction = (worldPos - transform.position).normalized;

        if (direction.sqrMagnitude > 0.01f)
        {
            lookDirection = direction;
        }
    }

    public void BuffMoveSpeed(float value, float duration)
    {
        if (speedBuffCoroutine != null)
            StopCoroutine(speedBuffCoroutine);

        speedBuffCoroutine = StartCoroutine(BuffMoveSpeedRoutine(value, duration));
    }

    private IEnumerator BuffMoveSpeedRoutine(float value, float duration)
    {
        moveSpeed += value;
        yield return new WaitForSeconds(duration);
        moveSpeed -= value;
        speedBuffCoroutine = null;
    }
}
