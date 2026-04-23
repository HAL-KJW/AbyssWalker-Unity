using UnityEngine;

/// <summary>
/// 레벨업 요구 경험치 공식 파라미터
/// 요구 경험치 = baseExp * (growthRate ^ (level - 1))
/// </summary>
[CreateAssetMenu(fileName = "ExperienceData", menuName = "Player/ExperienceData")]
public class ExperienceData : ScriptableObject
{
    [Header("레벨업 공식")]
    public int baseExp = 5; // level1 -> level2 요구 경험치
    public float growthRate = 1.5f; // 레벨당 배율
    public int maxLevel = 50;
}
