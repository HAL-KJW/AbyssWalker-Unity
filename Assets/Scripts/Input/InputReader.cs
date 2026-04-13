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
    }

    private void OnDisable()
    {
        // 메모리 누수 방지
        _inputActions.Player.Move.performed -= OnMoveInput;
        _inputActions.Player.Move.canceled -= OnMoveCanceled;
        _inputActions.Disable();
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

    //private void ReadMoveInput()
    //{
    //    float horizontal = Input.GetAxisRaw("Horizontal");
    //    float vertical   = Input.GetAxisRaw("Vertical");

    //    // normalized 대각선 이동시 속도 보정
    //    MoveInput = new Vector2(horizontal , vertical).normalized;
    //}
}
