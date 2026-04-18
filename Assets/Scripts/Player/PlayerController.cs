using UnityEngine;

/// <summary>
///  플레이어의 각 시스템을 연결, 조율하는 허브 클래스
/// </summary>
[RequireComponent(typeof(InputReader))]
[RequireComponent(typeof(PlayerMover))]
[RequireComponent(typeof(PlayerRotator))]
[RequireComponent(typeof(WeaponSystem))]
public class PlayerController : MonoBehaviour
{
    private InputReader _inputReader;
    private PlayerMover _playerMover;
    private PlayerRotator _playerRotator;
    private WeaponSystem _weaponSystem;

    private void Awake()
    {
        _inputReader = GetComponent<InputReader>();
        _playerMover = GetComponent<PlayerMover>();
        _playerRotator = GetComponent<PlayerRotator>();
        _weaponSystem = GetComponent<WeaponSystem>();
    }

    private void Update()
    {
        HandleRotation();
        HandleShooting();
    }

    private void FixedUpdate()
    {
        // 물리연산은 FixedUpdate에서 처리
        // FixedUpdate는 일정한 시간 간격으로 호출되므로 물리 계산에 적합
        HandleMovement();
    }

    private void HandleMovement()
    {
        _playerMover.Move(_inputReader.MoveInput);
    }

    private void HandleRotation()
    {
        _playerRotator.RotateTowardsMouse(_inputReader.MousePosition);
    }

    private void HandleShooting()
    {
        _weaponSystem.TryFire(_inputReader.IsFirePressed);
    }
}
