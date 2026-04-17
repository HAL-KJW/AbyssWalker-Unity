using UnityEngine;

/// <summary>
/// 무기의 스탯 데이터를 저장하는 ScriptableObject
/// 로그라이크 업그레이드 시 이 데이터를 교체,수치 변경
/// </summary>
[CreateAssetMenu(fileName = "NewWeaponData", menuName = "Weapon/WeaponData")]
public class WeaponData : ScriptableObject
{
    [Header("기본 정보")]
    public string weaponName;

    [Header("전투 스탯")]
    public int damage = 10;
    public float fireRate = 0.2f; // 초당 발사 횟수
    public float bulletSpeed = 20f;
    public float bulletLifetime = 2f;

    [Header("투사체")]
    public Bullet bulletPrefab;

    [Header("패턴")]
    public int bulletCount = 1; // 한 번에 발사되는 총알 수
    [Range(0f, 60f)] public float spreadAngle = 0f; // 총알이 퍼지는 각도 (0이면 직선 발사)
}
