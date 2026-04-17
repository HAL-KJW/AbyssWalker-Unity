using UnityEngine;

/// <summary>
/// 총알의 이동, 수명, 충돌 처리를 담당하는 클래스
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private int _damage;
    private float _speed;
    private bool _isPiercing; // 관통 여부

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        InitializeRigidbody();
    }

    /// <summary>
    /// 무기에서 생성 시 호출하여 스탯 설정
    /// </summary>
    public void Initialize(int damage, float speed, float lifeTime)
    {
        _damage = damage;
        _speed = speed;
        ApplyVelocity();
        Destroy(gameObject, lifeTime);
    }

    /// <summary>
    /// 관통 여부 설정
    /// </summary>
    public void SetPiercing(bool piercing)
    {
        _isPiercing = piercing;
    }
    private void InitializeRigidbody()
    {
        _rigidbody.useGravity = false; // 총알은 중력 영향 x
        _rigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic; // 고속 이동 시 충돌 감지 향상
    }

    private void ApplyVelocity()
    {
        _rigidbody.linearVelocity = transform.forward * _speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        TryApplyDamage(other);

        if(!_isPiercing)
        {
            Destroy(gameObject); // 관통이 아니면 충돌 시 파괴
        }
    }

    private void TryApplyDamage(Collider target)
    {
        IDamageable damageable = target.GetComponent<IDamageable>();
        if (damageable == null) return;
        
        damageable.TakeDamage(_damage);
    }
}