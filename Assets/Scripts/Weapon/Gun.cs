using UnityEngine;

/// <summary>
/// 단발 무기(권총같은)
/// </summary>
public class Gun : Weapon
{
    protected override void Fire()
    {
        SpawnBullet(firePoint.position, firePoint.rotation);
    }
}