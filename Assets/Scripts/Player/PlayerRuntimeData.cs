using System;
using UnityEngine;

/// <summary>
/// 플레이어 런타임 데이터 클래스
/// 업그레이드시 이 값을 변경
/// </summary>
public class PlayerRuntimeData
{
    public int MaxHealth { get; private set; }
    public float MoveSpeed { get; private set; }
    public float FireRateMultiplier { get; private set; }
    public float DamageMultiplier { get; private set; }

    /// <summary>
    /// 스탯 변경 시 알림 (UI 갱신 등에 활용)
    /// </summary>
    public event Action OnStatsChanged;

    /// <summary>
    /// ScriptableObject 기본값으로 초기화
    /// </summary>
    public void Initialize(PlayerBaseData baseData)
    {
        MaxHealth = baseData.maxHealth;
        MoveSpeed = baseData.moveSpeed;
        FireRateMultiplier = baseData.fireRateMultiplier;
        DamageMultiplier = baseData.damageMultiplier;
    }

    public void AddMaxHealth(int amount)
    {
        MaxHealth += amount;
        OnStatsChanged?.Invoke();
    }

    public void AddMoveSpeed(float amount)
    {
        MoveSpeed += amount;
        OnStatsChanged?.Invoke();
    }

    public void MultiplyFireRate(float multiplier)
    {
        FireRateMultiplier *= multiplier;
        OnStatsChanged?.Invoke();
    }

    public void MultiplyDamage(float multiplier)
    {
        DamageMultiplier *= multiplier;
        OnStatsChanged?.Invoke();
    }
}