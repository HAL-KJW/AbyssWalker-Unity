using UnityEngine;

/// <summary>
/// 플레이어 이동을 담당하는 클래스
/// 물리 기반 이동(Rigidbody)
/// 월드 좌표 기준 이동
/// </summary>

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(PlayerStats))]
public class PlayerMover : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private PlayerStats _stats;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _stats = GetComponent<PlayerStats>();
        InitializeRigidbody();
    }

    /// <summary>
    /// Rigidbody 설정 초기화
    /// 물리충돌시 캐릭터가 넘어지는것 방지하기위한 회전 고정
    /// </summary>
    private void InitializeRigidbody()
    {
        _rigidbody.freezeRotation = true;
    }

    /// <summary>
    /// 입력 캐릭터 이동
    /// </summary>
    /// <param name="moveInput"> InputReader에서 받은 입력값(정규화된 Vector2)</param>
    public void Move(Vector2 moveInput)
    {
        Vector3 moveDirection = ConvertToWorldDirection(moveInput);
        ApplyMovement(moveDirection);
    }

    /// <summary>
    /// 2D 입력값을 월드 좌표 기준 3D 방향으로 변환
    /// W(+Y) = 월드 +Z, S(-Y) = 월드 -Z, A(-X) = 월드 -X, D(+X) = 월드 +X
    /// 회전과 무관하게 항상 같은 방향으로 이동 (쿼터뷰 트윈스틱 방식)
    /// </summary>
    private Vector3 ConvertToWorldDirection(Vector2 input)
    {
        return new Vector3(input.x, 0f, input.y);
    }

    /// <summary>
    /// velocity 방식: AddForce보다 반응성이 좋아 액션 게임에 적합합니다
    /// Y축(중력)은 유지하면서 XZ축만 이동에 사용
    /// </summary>
    private void ApplyMovement(Vector3 direction)
    {
        float speed = _stats.RuntimeData.MoveSpeed;
        Vector3 velocity = direction * speed;
        _rigidbody.linearVelocity = new Vector3(velocity.x, _rigidbody.linearVelocity.y, velocity.z);
    }
}
