using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// 플레이어의 입력을 담당하는 클래스입니다.
/// Input System을 사용
/// </summary>

public class InputReader : MonoBehaviour
{
    /// <summary>
    /// 이동 입력값( x: 좌우, y: 앞뒤 )
    /// </summary>
    public Vector2 MoveInput { get; private set; }

    /// <summary>
    /// 마우스 화면 좌표
    /// </summary>
    public Vector2 MousePosition { get; private set; }

    /// <summary>
    /// 공격(클릭) 입력 여부
    /// </summary>
    public bool IsFirePressed { get; private set; }

    private PlayerInputActions _inputActions;

    private void Awake()
    {
        _inputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        // Input System 활성화
        _inputActions.Enable();
        // Move액션시마다 OnMoveInput 메서드 호출
        _inputActions.Player.Move.performed += OnMoveInput;
        // Move액션이 취소쉬 OnMoveCanceled 메서드 호출
        _inputActions.Player.Move.canceled += OnMoveCanceled;
        _inputActions.Player.Fire.performed += OnFireInput;
        _inputActions.Player.Fire.canceled += OnfireCanceled;
    }

    private void OnDisable()
    {
        // 메모리 누수 방지
        _inputActions.Player.Move.performed -= OnMoveInput;
        _inputActions.Player.Move.canceled -= OnMoveCanceled;
        _inputActions.Player.Fire.performed -= OnFireInput;
        _inputActions.Player.Fire.canceled -= OnfireCanceled;
        _inputActions.Disable();
    }

    private void Update()
    {
        ReadMousePosition();
    }

    /// <summery>
    /// 마우스 화면 좌표갱신
    /// </summery>
    private void ReadMousePosition()
    {
        if(Mouse.current != null)
        {
            MousePosition = Mouse.current.position.ReadValue();
        }
    }

    /// <summary>
    /// 이동 입력이 들어왔을 때 호출
    /// InputAction.CallbackContext: 입력의 상태/값을 담고 있는 구조체
    /// </summary>
    private void OnMoveInput(InputAction.CallbackContext context)
    {
        MoveInput = context.ReadValue<Vector2>().normalized;
    }

    /// <summary>
    /// 이동 입력이 끝났을 때 호출
    /// </summary>
    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        MoveInput = Vector2.zero;
    }

    private void OnFireInput(InputAction.CallbackContext context)
    {
        IsFirePressed = true;
    }

    private void OnfireCanceled(InputAction.CallbackContext context)
    {
        IsFirePressed = false;
    }

}
