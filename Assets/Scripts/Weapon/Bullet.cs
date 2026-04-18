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
    private BulletPool _pool;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        InitializeRigidbody();
    }

    /// <summary>
    /// 풀 참조 설정
    /// </summary>
    public void SetPool(BulletPool pool)
    {
        _pool = pool;
    }    

    /// <summary>
    /// 무기에서 생성 시 호출하여 스탯 설정
    /// </summary>
    public void Initialize(int damage, float speed, float lifeTime)
    {
        _damage = damage;
        _speed = speed;
        _isPiercing = false; 
        ApplyVelocity();
        CancelInvoke(); // 이전 ReturnToPool 호출 취소
        Invoke(nameof(ReturnToPool), lifeTime); // 일정 시간 후 풀로 반환
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
            ReturnToPool();
        }
    }

    private void TryApplyDamage(Collider target)
    {
        IDamageable damageable = target.GetComponent<IDamageable>();
        if (damageable == null) return;
        
        damageable.TakeDamage(_damage);
    }

    /// <summary>
    /// 풀 반환(Destroy 대신)
    /// </summary>
    private void ReturnToPool()
    {
        CancelInvoke(); // 중복 호출 방지
        _rigidbody.linearVelocity = Vector3.zero; // 속도 초기화

        if(_pool != null)
        {
            _pool.Release(this);
        }
        else
        {
            Destroy(gameObject); // 풀 참조 없으면 파괴
        }
    }
}