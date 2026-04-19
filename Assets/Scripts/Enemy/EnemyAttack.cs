using UnityEngine;

/// <summary>
/// 적의 공격을 담당하는 클래스
/// (현재) 충돌시 플레이어에게 데미지
/// </summary>
public class EnemyAttack : MonoBehaviour
{
    private int _attakeDamage;

    public void Initialize(int attakeDamage)
    {
        _attakeDamage = attakeDamage;
    }

    private void OnTriggerEnter(Collider other)
    {
        // 플레이어 태그 확인 후 IDamageable로 데미지 전달
        if (!other.CompareTag("Player")) return;

        IDamageable damageable = other.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(_attakeDamage);
        }
    }
}