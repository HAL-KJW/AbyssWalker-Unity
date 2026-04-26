using UnityEngine;

/// <summary>
/// 경험치/레벨 런타임 상태
/// </summary>
public class ExperienceRuntimeData
{
    public int Level { get; private set; } = 1;
    public int CurrentExp { get; private set; } = 0;
    public int RequiredExp { get; private set; } // 다음 레벨업에 필요한 경험치

    private ExperienceData _data;

    public void Initialize(ExperienceData data)
    {
        _data = data;
        Level = 1;
        CurrentExp = 0;
        RequiredExp = CalculateRequiredExp(Level);
    }

    /// <summary>
    /// 경험치 누적, 레벨업 발생 횟수를 반환
    /// </summary>
    public int AddExp(int amount)
    {
        if (Level >= _data.maxLevel) return 0; // 최대 레벨 도달

        CurrentExp += amount;
        int levelUps = 0;

        // 한번에 여러 레벨 오르는 경우 처리
        while (CurrentExp >= RequiredExp && Level < _data.maxLevel)
        {
            CurrentExp -= RequiredExp;
            Level++;
            RequiredExp = CalculateRequiredExp(Level);
            levelUps++;
        }

        if (Level >= _data.maxLevel) CurrentExp = 0; // 최대 레벨 도달 시 경험치 초기화
        return levelUps;
    }

    private int CalculateRequiredExp(int level)
    {
        // 요구 경험치 = baseExp * (growthRate ^ (level - 1))
        return Mathf.CeilToInt(_data.baseExp * Mathf.Pow(_data.growthRate, level - 1));// Mathf.CeilToInt로 소수점 올림 처리하여 요구 경험치 정수화
    }

}
