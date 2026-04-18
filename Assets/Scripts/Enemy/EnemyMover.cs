using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// 적의 이동을 담당하는 클래스
/// 현재기준 플에이어 방향으로 단순 이동(NavMesh미사용(추후 고민))
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class EnemyMover : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private float _moveSpeed;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.freezeRotation = true;
    }

    public void Initalize(float moveSpeed)
    {
        _moveSpeed = moveSpeed;
    }

    /// <summary>
    /// 타겟 방향으로 이동
    /// </summary>
    public void MoveToward(Vector3 targetPosition)
    {
        // 타겟 방향 계산
        Vector3 direction = (targetPosition - transform.position);
        direction.y = 0f;
        direction.Normalize();

        // 이동 속도 적용
        Vector3 velocity = direction * _moveSpeed;
        _rigidbody.linearVelocity = new Vector3(velocity.x, _rigidbody.linearVelocity.y, velocity.z);

        // 이동 방향으로 회전
        if (direction.sqrMagnitude > 0.001f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 10f * Time.fixedDeltaTime);
        }
    }

    /// <summary>
    /// 이동 정지
    /// </summary>
    public void Stop()
    {
        _rigidbody.linearVelocity = new Vector3(0f, _rigidbody.linearVelocity.y, 0f);
    }
}