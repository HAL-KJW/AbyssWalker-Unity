using UnityEngine;
using System;

/// <summary>
/// 플레이어 체력관리 클래스
/// PlayerStats에서 참조
/// </summary>
[RequireComponent(typeof(PlayerStats))]
public class PlayerHealth : MonoBehaviour,IDamageable
{
    private PlayerStats _playerStats;
    private int _currentHealth;

    public event Action OnDeath; // 플레이어 사망 이벤트
    public event Action<int, int> OnHealthChanged; //  (현재HP, 최대HP) - UI용

    private void Awkae()
    {
        _playerStats = GetComponent<PlayerStats>();
    }

    private void Start()
    {
        _currentHealth = _playerStats.RuntimeData.MaxHealth;
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        _currentHealth = Mathf.Max(_currentHealth, 0); // 체력이 0 아래로 내려가지 않도록

        Debug.Log($"[Player] HP: {_currentHealth}/{_playerStats.RuntimeData.MaxHealth}"); // 디버그 로그로 체력 상태 출력
        OnHealthChanged?.Invoke(_currentHealth, _playerStats.RuntimeData.MaxHealth); // 체력 변경 이벤트 호출

        if(_currentHealth <= 0)
        {
            OnDeath?.Invoke(); // 사망 이벤트 호출
            Debug.Log("[Player] 플레이어 사망");
        }
    }

    /// <summary>
    /// 체력 회복 (업그레이드/ 아이템 용)
    /// </summary> 
    public void Heal(int amount)
    {
        _currentHealth = Mathf.Min(_currentHealth + amount, _playerStats.RuntimeData.MaxHealth); // 체력이 최대 체력을 넘지 않도록
        OnHealthChanged?.Invoke(_currentHealth, _playerStats.RuntimeData.MaxHealth); // 체력 변경 이벤트 호출
    } 
}
