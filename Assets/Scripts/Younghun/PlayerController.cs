using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PlayerController : Character
{
    [SerializeField] private Camera _camera; // 마우스 위치를 월드 좌표로 변환하기 위한 메인 카메라 참조
    
    public PlayerStat stats;
    public static PlayerController Instance;

    void Awake()
    {
        base.Awake();
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
    protected override void Start()
    {
        base.Start();
        _camera = Camera.main;
        stats.Start();
    }

    protected override void Update()
    {
        base.Update();
        UpdateLookDirectionUniversal();
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

    private void UpdateLookDirectionUniversal()
    {
        Vector2 screenPos = Vector2.zero;
        bool validInput = false;

#if UNITY_EDITOR || UNITY_STANDALONE
        // PC 환경: 마우스 입력
        if (Mouse.current != null)
        {
            screenPos = Mouse.current.position.ReadValue();
            validInput = true;

        }
#elif UNITY_ANDROID || UNITY_IOS
    // 모바일 환경: 터치 입력
    if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.isPressed)
    {
        screenPos = Touchscreen.current.primaryTouch.position.ReadValue();
        validInput = true;
    }
#endif

        if (!validInput || _camera == null || screenPos == Vector2.zero)
            return;

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
}