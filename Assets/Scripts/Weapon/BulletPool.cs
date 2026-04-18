using UnityEngine;
using UnityEngine.Pool;

/// <summary>
/// 싱글톤 패턴 + 오브젝트 풀링 시스템
/// 총알 오브젝트 풀링 시스템을 관리하는 클래스
/// </summary>

public class BulletPool : MonoBehaviour
{
    private static BulletPool _instance;
    // public static BulletPool Instance => _instance;
    public static BulletPool Instance
    {
        get { return _instance; }
    }

    private ObjectPool<Bullet> _pool;

    [Header("풀 설정")]
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private int defaultCapacity = 20;
    [SerializeField] private int maxSize = 50;

    private void Awake()
    {
        // 싱글톤 패턴 구현
        _instance = this;

        _pool = new ObjectPool<Bullet>(
            createFunc: CreateBullet,
            actionOnGet: OnGetBullet,
            actionOnRelease: OnReleaseBullet,
            actionOnDestroy: OnDestroyBullet,
            defaultCapacity: defaultCapacity,
            maxSize: maxSize
        );
    }

    /// <summary>
    /// 풀에서 총알을 꺼냄
    /// </summary>
    public Bullet Get()
    {
        return _pool.Get();
    }

    /// <summary>
    /// 총알을 풀에 반환(Destroy 대신)
    /// </summary>
    public void Release(Bullet bullet)
    {
        _pool.Release(bullet);
    }

    /// <summary>
    /// 총알을 풀에 반환
    /// </summary>
    private Bullet CreateBullet()
    {
        Bullet bullet = Instantiate(_bulletPrefab);
        bullet.SetPool(this);
        return bullet;
    }

    private void OnGetBullet(Bullet bullet)
    {
        bullet.gameObject.SetActive(true);
    }

    private void OnReleaseBullet(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
    }

    private void OnDestroyBullet(Bullet bullet)
    {
        Destroy(bullet.gameObject);
    }
}
