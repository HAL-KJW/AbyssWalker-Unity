using UnityEngine;

/// <summary>
/// 모든 무기의 부모 클래스
/// 무기마다 고유한 발사 패턴과 효과를 구현하기 위해 상속 구조로 설계
/// </summary>
public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected Transform firePoint; // 총알이 발사되는 위치

    protected WeaponData _data;  // 무기의 스탯 데이터
    private float _lastFireTime; // 마지막 발사 시간

    /// <summary>
    /// 무기 데이터 설정(장착,업그레이드시 호출)
    /// </summary>
    public void Initialize(WeaponData data)
    {
        _data = data;
    }

    /// <summary>
    /// 쿨타임 체크 후 발사 시도
    /// </summary>
    public void TryFire()
    {
        if (!IsCooldownReady()) return;

        _lastFireTime = Time.time;
        Fire();
    }

    /// <summary>
    /// 각 무기별 발사 로직
    /// </summary>
    protected abstract void Fire();

    /// <summary>
    /// 풀에서 총알을 꺼내 반환
    /// </summary>
    protected Bullet SpawnBullet(Vector3 position, Quaternion rotation)
    {
        Bullet bullet = BulletPool.Instance.Get();
        bullet.transform.position = position;
        bullet.transform.rotation = rotation;
        bullet.Initialize(_data.damage, _data.bulletSpeed, _data.bulletLifetime);
        return bullet;
    }

    private bool IsCooldownReady()
    {
        return Time.time >= _lastFireTime + _data.fireRate;
    }

    public WeaponData GetData()
    {
        return _data;
    }
}