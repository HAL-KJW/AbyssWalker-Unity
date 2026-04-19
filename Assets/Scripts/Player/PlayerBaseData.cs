using UnityEngine;

/// <summary>
/// 플레이어 기본 스탯 데이터 저장하는 ScriptableObject 클래스
/// </summary>
[CreateAssetMenu(fileName = "PlayerBaseData", menuName = "Player/PlayerBaseData")]
public class PlayerBaseData : ScriptableObject
{
    [Header("기본 스탯")]
    public int maxHealth = 10;
    public float moveSpeed = 5f;

    [Header("전투")]
    public float fireRateMultiplier = 1f;  // 발사 속도 배율
    public float damageMultiplier = 1f;    // 데미지 배율
}