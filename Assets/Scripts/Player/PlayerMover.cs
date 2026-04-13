using UnityEngine;

/// <summary>
/// 플레이어 이동을 담당하는 클래스
/// 물리 기반 이동(Rigidbody)tkdyd
/// </summary>

[RequireComponent(typeof(Rigidbody))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f; // 이동 속도

    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
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
    /// 2D 입력값을 3D 월드방향으로 변환
    /// 쿼터뷰 게임에서 x축은 좌우, z축은 상하 이동으로 매핑
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
        Vector3 velocity = direction * moveSpeed;
        _rigidbody.linearVelocity = new Vector3(velocity.x, _rigidbody.linearVelocity.y, velocity.z);
    }


}
