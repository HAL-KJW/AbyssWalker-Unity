using UnityEngine;

/// <summary>
/// 데미지 받을수 있는 오브젝트가 구현하는 인터페이스
/// </summary>
public interface IDamageable
{
    void TakeDamage(int damage);
}