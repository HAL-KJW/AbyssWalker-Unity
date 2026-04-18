using UnityEngine;

/// <summary>
/// 플레이어 회전을 담당하는 클래스
/// 마우스가 가리키는 바닥위치를 바라보도록 회전
/// </summary>
public class PlayerRotator : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 10f;

    private Camera _mainCamera;
    private Plane _groundPlane = new Plane(Vector3.up, Vector3.zero);

    private void Start()
    {
        _mainCamera = Camera.main;
    }

    /// <summary>
    /// 마우스 화면 좌표를 받아 바닥 기준으로 회전
    /// </summary>
    public void RotateTowardsMouse(Vector2 mouseScreenPosition)
    {
        Vector3 worldPoint;
        if (!TryGetMouseWorldPosition(mouseScreenPosition, out worldPoint)) return;

        Vector3 lookDirection = CalculateLookDirection(worldPoint);
        if (lookDirection.sqrMagnitude < 0.001f) return;

        ApplyRotation(lookDirection);
    }

    /// <summary>
    /// 화면 좌표를 바닥 평면에 Raycast하여 월드 좌표로 변환
    /// </summary>
    private bool TryGetMouseWorldPosition(Vector2 screenPosition, out Vector3 worldPosition)
    {
        worldPosition = Vector3.zero;
        Ray ray = _mainCamera.ScreenPointToRay(screenPosition);

        float distance;
        if (_groundPlane.Raycast(ray, out distance))
        {
            worldPosition = ray.GetPoint(distance);
            return true;
        }
        return false;
    }

    private Vector3 CalculateLookDirection(Vector3 targetWorldPosition)
    {
        Vector3 direction = targetWorldPosition - transform.position;
        direction.y = 0f;
        return direction;
    }

    /// <summary>
    /// Slerp방식을사용(부드러운 회전)
    /// </summary>
    private void ApplyRotation(Vector3 lookDirection)
    {
        Quaternion targetRotation = Quaternion.LookRotation(lookDirection);
        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            targetRotation,
            rotationSpeed * Time.deltaTime
            );
    }
}
