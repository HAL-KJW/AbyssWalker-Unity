using UnityEngine;

/// <summary>
/// 적의 각 컴포넌트를 연결 조율하는 허브 클래스
/// 
/// [흐름]
/// 1.EnemyPool에서 Get-> SetActive(true)
/// 2.Initialize()로 EnemyData 세팅
/// 3.코루틴으로 플레이어 추적
/// 4.사망시 OnDeath 이벤트 발생-> EnemyPool에 Return
/// </summary>
[RequireComponent(typeof(EnemyHealth))]
[RequireComponent(typeof(EnemyMover))]
[RequireComponent(typeof(EnemyAttack))]
public class EnemyController : MonoBehaviour
{
    [SerializeField] private EnemyData _enemyData;
    
    private EnemyHealth _health;
    private EnemyMover _mover;
    private EnemyAttack _attack;
    private EnemyPool _pool;

    private Transform _playerTransform;
    private float _aiTimer;
    private bool _isActive;

    private void Awake()
    {
        _health = GetComponent<EnemyHealth>();
        _mover = GetComponent<EnemyMover>();
        _attack = GetComponent<EnemyAttack>();
    }

    
    private void OnEnable()
    {
        _health.OnDeath += HandleDeath;
    }

    private void OnDisable()
    {
        _health.OnDeath -= HandleDeath;
    }

    /// <summary>
    /// EnemyData 기반 초기화
    /// </summary>
    public void Initialize(EnemyData data, Transform player)
    {
        _enemyData = data;
        _playerTransform = player;
        _aiTimer = 0f;
        _isActive = true;

        _health.Initialize(data.maxHealth);
        _mover.Initialize(data.moveSpeed);
        _attack.Initialize(data.attackDamage);
    }

    /// <summary>
    /// 풀 참조 설정 
    /// </summary>
    public void SetPool(EnemyPool pool)
    {
        _pool = pool;
    }

    private void FixedUpdate()
    {
        if (!_isActive || _playerTransform == null) return; // 비활성화 상태거나 플레이어 참조 없으면 AI X

        // AI 갱신 주기 제한 - Update 최소화 최적화
        _aiTimer += Time.fixedDeltaTime;
        if (_aiTimer < _enemyData.aiUpdateInterval) return; 
        _aiTimer = 0f; 

        _mover.MoveToward(_playerTransform.position);
    }

    /// <summary>
    /// 사망처리 - 풀에 반환
    /// </summary>
    private void HandleDeath()
    {
        _isActive = false;
        _mover.Stop();

        // 풀 참조가 있으면 반환, 없으면 비활성화
        if (_pool != null)
        {
            _pool.Release(this);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}

