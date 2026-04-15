using UnityEngine;

/// <summary>
/// 카메라를 제어하는 허브 클래스
/// </summary>
[RequireComponent(typeof(CameraFollower))]
public class CameraController : MonoBehaviour
{
    [Header("카메라 회전 각도(쿼터뷰)")]
    [SerializeField] private Vector3 rotationAngle = new Vector3(50f,0f, 0f);

    private CameraFollower _cameraFollower;

    private void Awake()
    {
        _cameraFollower = GetComponent<CameraFollower>();
    }

    void Start()
    {
        InitlizeCamera();
    }

    /// <summary>
    /// 플레이어의 최종위치를 따라가도록 카메라 초기화
    /// </summary>
    private void LateUpdate()
    {
        if(!_cameraFollower.HasTarget()) return;
        _cameraFollower.Follow();
    }

    /// <summary>
    /// 카메라 초기 위치/회전 설정
    /// </summary>
    private void InitlizeCamera()
    {
        if (!_cameraFollower.HasTarget()) return;

        ApplyRotation();
        _cameraFollower.SetPositionImmediate();
    }

    /// <summary>
    /// 카메라의 쿼터뷰 회전 적용
    /// </summary>
    private void ApplyRotation()
    {
        transform.rotation = Quaternion.Euler(rotationAngle);
    }

}
