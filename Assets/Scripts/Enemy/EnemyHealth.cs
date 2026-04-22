using UnityEngine;
using System;

/// <summary>
/// 적 체력 관리 클래스
/// </summary>
public class EnemyHealth : MonoBehaviour, IDamageable
{
    private int _maxHealth;
    private int _currenHealth;
    private bool _isDead;

    /// <summary>
    /// 사망 시 호출 되는 이벤트
    /// </summary>
    public event Action OnDeath;

    /// <summary>
    /// EnemyData의 maxHealth로 체력 초기화
    /// </summary>
    public void Initialize(int maxHealth)
    {
        _maxHealth = maxHealth;
        _currenHealth = maxHealth;
        _isDead = false;
    }

    public void TakeDamage(int damage)
    {
        if (_isDead) return;

        _currenHealth -= damage;

        if(_currenHealth <= 0)
        {
            _currenHealth = 0;
            _isDead = true;
            OnDeath?.Invoke();
        }
    }
}