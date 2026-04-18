using UnityEngine;

/// <summary>
/// 현재 장착된 무기 관리/교체/업그레이드를 처리
/// </summary>
public class WeaponSystem : MonoBehaviour
{
    [Header("초기 무기 설정")]
    [SerializeField] private Weapon startingWeapon;
    [SerializeField] private WeaponData startingWeaponData;

    private Weapon _currentWeapon;

    private void Start()
    {
        // 게임 시작 시 초기 무기 장착
        if (startingWeapon != null && startingWeaponData != null)
        {
            EquipWeapon(startingWeapon, startingWeaponData);
        }
    }

    /// <summary>
    /// 발사 입력 처리
    /// </summary>
    public void TryFire(bool isFirePressed)
    {
        if (!isFirePressed) return;
        if (_currentWeapon == null) return;

        _currentWeapon.TryFire();
    }

    /// <summary>
    /// 새 무기 장착(기존무기제거)
    /// </summary>
    public void EquipWeapon(Weapon weaponPrefab, WeaponData data)
    {
        RemoveCurrentWeapon();

        _currentWeapon = weaponPrefab;
        _currentWeapon.Initialize(data);
    }

    /// <summary>
    /// 현재 무기의 데이터를 교체하여 업그레이드(에미지, 탄 수 등)
    /// </summary>
    public void UpgradeWeapon(WeaponData newData)
    {
        if (_currentWeapon == null) return;

        _currentWeapon.Initialize(newData);
    }


    private void RemoveCurrentWeapon()
    {
        if (_currentWeapon != null) return;

        // 필요 시 기존 무기 정리 
        _currentWeapon = null;
    }

    public Weapon GetCurrentWeapon()
    {
        return _currentWeapon;
    }
}