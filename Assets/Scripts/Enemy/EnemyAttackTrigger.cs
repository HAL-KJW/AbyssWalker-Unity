using UnityEngine;

/// <summary>
/// 적의 공격 감지 트리거 클래스
/// 부모 EnemyAttack에서 충돌 정보를 받아 처리
/// </summary>
[RequireComponent(typeof(Collider))]
public class EnemyAttackTrigger : MonoBehaviour
{
    private EnemyAttack _attack;

    private void Awake()
    {
        _attack = GetComponentInParent<EnemyAttack>();
        if (_attack == null)
        {
            Debug.LogError("EnemyAttackTrigger이 EnemyAttack의 자식 오브젝트에 있어야 합니다.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        _attack.OnAttackTrigger(other);
    }
}