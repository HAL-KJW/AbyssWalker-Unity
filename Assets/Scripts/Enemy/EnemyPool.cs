using UnityEngine;
using UnityEngine.Pool;

/// <summary>
/// 적 오브젝트 풀링시스템
/// 싱글톤패턴으로 구현
/// </summary>
public class EnemyPool : MonoBehaviour
{
    private static EnemyPool _instance;
    public static EnemyPool Instance
    {
        get { return _instance; }
    }

    private ObjectPool<EnemyController> _pool;

    [Header("풀 설정")]
    [SerializeField] private EnemyController _enemyPrefab;
    [SerializeField] private EnemyData _enemyData;
    [SerializeField] private int defaultCapacity = 30;
    [SerializeField] private int maxSize = 100;

    private Transform _playerTransform;

    private void Awake()
    {
        _instance = this;

        _pool = new ObjectPool<EnemyController>(
             createFunc: CreateEnemy,
             actionOnGet: OnGetEnemy,
             actionOnRelease: OnReleaseEnemy,
             actionOnDestroy: OnDestroyEnemy,
             defaultCapacity: defaultCapacity,
             maxSize: maxSize
         );
    }

    private void Start()
    {
        // 플레이어 태그로 플레이어 참조획득
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if(player != null)
        {
            _playerTransform = player.transform;
        }
    }

    /// <summary>
    /// 지정된 위치에 적 스폰
    /// </summary>
    public EnemyController Spawn(Vector3 position)
    {
        EnemyController enemy = _pool.Get();
        enemy.transform.position = position;
        enemy.Initialize(_enemyData, _playerTransform);
        return enemy;
    }

    public void Release(EnemyController enemy)
    {
        _pool.Release(enemy);
    }

    private EnemyController CreateEnemy()
    {
        EnemyController enemy = Instantiate(_enemyPrefab);
        enemy.SetPool(this);
        return enemy;
    }

    private void OnGetEnemy(EnemyController enemy)
    {
        enemy.gameObject.SetActive(true);
    }

    private void OnReleaseEnemy(EnemyController enemy)
    {
        enemy.gameObject.SetActive(false);
    }

    private void OnDestroyEnemy(EnemyController enemy)
    {
        Destroy(enemy.gameObject);
    }
}