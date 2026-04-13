using UnityEngine;

/// <summary>
/// 플레이어 회전을 담당하는 클래스
/// </summary>
public class PlayerRotator : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 10f;

    /// <summary>
    /// 이동방향을 바라보도록 회전
    /// </summary>
    /// <param name="moveInput">이동 입력값</param>
    public void Rotate(Vector2 moveInput)
    {
        if (!HasMoveInput(moveInput)) return; // 입력없으면 리턴

        Vector3 lookDirection = ConvertToWorldDirection(moveInput);
        ApplyRotation(lookDirection);
    }

    /// <summary>
    /// 입력있는지 확인
    /// </summary>
    private bool HasMoveInput(Vector2 input)
    {
        return input.sqrMagnitude > 0f;
    }

    private Vector3 ConvertToWorldDirection(Vector2 input)
    {
        return new Vector3(input.x, 0f, input.y);
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
