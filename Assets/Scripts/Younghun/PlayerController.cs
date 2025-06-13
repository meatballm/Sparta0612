using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PlayerController : Character
{
    private Camera camera; // 마우스 위치를 월드 좌표로 변환하기 위한 메인 카메라 참조

    protected override void Start()
    {
        base.Start();
        camera = Camera.main;
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

        if (!validInput || Camera.main == null)
            return;

        // 화면 좌표를 월드 좌표로 변환
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPos);
        worldPos.z = 0f;

        // 캐릭터 위치 기준 방향 벡터 계산
        Vector2 direction = (worldPos - transform.position).normalized;

        if (direction.sqrMagnitude > 0.01f)
            lookDirection = direction;
    }

}