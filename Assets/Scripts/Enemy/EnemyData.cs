using UnityEngine;

/// <summary>
/// 적의 스탯 데이터를 저장하는 ScriptableObject 클래스
/// </summary>
[CreateAssetMenu(fileName = "EnemyData" , menuName = "Enemy/EnemyData")]
public class EnemyData : ScriptableObject
{
    [Header("기본 정보")]
    public string enemyName;

    [Header("스탯")]
    public int maxHealth = 3;
    public float moveSpeed = 3f;
    public int attackDamage = 1;

    [Header("AI")]
    public float attackRange = 1.5f;
    public float aiUpdateInterval = 0.2f; // AI 행동 업데이트 간격
}