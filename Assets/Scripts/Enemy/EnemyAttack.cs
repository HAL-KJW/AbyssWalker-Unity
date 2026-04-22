using UnityEngine;

/// <summary>
/// 적의 공격을 담당하는 클래스
/// EnemyAttackTrigger(자식)에서 충돌 정보를 받아 처리
/// </summary>
public class EnemyAttack : MonoBehaviour
{
    private int _attakeDamage;

    public void Initialize(int attakeDamage)
    {
        _attakeDamage = attakeDamage;
    }

    /// <summary>
    /// EnemyAttackTrigger에서 호출됨
    /// </summary>
    public void OnAttackTrigger(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        IDamageable damageable = other.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(_attakeDamage);
        }
    }
}