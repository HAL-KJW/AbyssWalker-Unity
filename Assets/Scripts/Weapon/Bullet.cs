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
    private bool _isReleased; // 풀에 반환 여부 체크용 (중복 방지)
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
        _isReleased = false;
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
        if (_isReleased) return; // 이미 풀에 반환된 총알은 무시

        TryApplyDamage(other);

        if(!_isPiercing)
        {
            ReturnToPool();
        }
    }

    /// <summary>
    /// 충돌한 Collider(또는 부모)에서 IDamageable을 찾아 데미지 적용
    /// 자식 Collider(AttackRange 등)에 충돌해도 부모의 Health에 데미지가 전달되도록 Parent까지 탐색
    /// </summary>
    private void TryApplyDamage(Collider target)
    {
        IDamageable damageable = target.GetComponentInParent<IDamageable>();
        if (damageable == null) return;
        
        damageable.TakeDamage(_damage);
    }

    /// <summary>
    /// 풀 반환(Destroy 대신)
    /// </summary>
    private void ReturnToPool()
    {
        if (_isReleased) return; // 중복 반환 방지
        _isReleased = true;

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