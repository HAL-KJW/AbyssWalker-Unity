using UnityEngine;

/// <summary>
/// 카메라가 타겟을 부드럽게 추적하는 기능을 담당
/// </summary>
public class CameraFollower : MonoBehaviour
{
    [Header("추적 대상")]
    [SerializeField] private Transform target;

    [Header("카메라 오프셋")]
    [SerializeField] private Vector3 offset = new Vector3(0f, 10f, -7f);

    [Header("추적 속도")]
    [SerializeField] private float followSpeed = 5f;

    /// <summary>
    /// 보간없이 타겟위치로 이동(초기화용)
    /// </summary>
    public void SetPositionImmediate()
    {
        transform.position = CalculateDesiredPosition();
    }

    /// <summary>
    /// Lerp를 사용해 부드럽게 타겟 추적
    /// </summary>
    public void Follow()
    {
        Vector3 desiredPosition = CalculateDesiredPosition();
        transform.position = Vector3.Lerp(
            transform.position,
            desiredPosition,
            followSpeed * Time.deltaTime
            );
    }

    /// <summary>
    /// 타겟 위치 + 오프셋   목표위치계산
    /// </summary>
    private Vector3 CalculateDesiredPosition()
    {
        return target.position + offset;
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    public void SetOffset(Vector3 newOffset)
    {
        offset = newOffset;
    }

    public bool HasTarget()
    {
        return target != null;
    }
 
}
