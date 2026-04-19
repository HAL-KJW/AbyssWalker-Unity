using UnityEngine;

/// <summary>
/// 플레이어 스탯을 관리하는 허브 컴포넌트
/// 다른 스크립트에서(PlayerMover, WeaponSystem 등)이 여기서 스탯을 참조
/// </summary>
public class PlayerStats : MonoBehaviour
{
    [SerializeField] private PlayerBaseData _baseData;

    private PlayerRuntimeData _runtimeData;
    public PlayerRuntimeData RuntimeData => _runtimeData; 

    private void Awake()
    {
        _runtimeData = new PlayerRuntimeData();
        _runtimeData.Initialize(_baseData);
    }
}