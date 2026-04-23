using System;
using UnityEngine;
/// <summary>
/// 경험치,레벨 관리 허브
/// Enemy사망시 AddExp호출
/// UI,카드 시스템은 이벤트로 발동
/// </summary>
public class ExperienceSystem : MonoBehaviour
{
    [SerializeField] private ExperienceData _expData;

    private ExperienceRuntimeData _runtime;
    public ExperienceRuntimeData Runtime { get { return _runtime; } }

    /// <summary>
    /// 현재exp, 요구exp, 현재level    UI업데이트용
    /// </summary>
    public event Action<int, int, int> OnExpChanged;

    /// <summary>
    /// 레벨업 -> 카드 시스템,이펙트에서 사용
    /// </summary>
    public event Action<int> OnLevelUp;

    private void Awake()
    {
        _runtime = new ExperienceRuntimeData(); // 런타임 데이터 초기화
        _runtime.Initialize(_expData);          // 경험치 데이터로 초기화
    }

    private void Start()
    {
        // UI 초기값 업데이트
        OnExpChanged?.Invoke(_runtime.CurrentExp, _runtime.RequiredExp, _runtime.Level);
    }

    public void AddExp(int amount)
    {
        if (amount <= 0) return; // 음수 경험치 무시

        int levelUps = _runtime.AddExp(amount);
        OnExpChanged?.Invoke(_runtime.CurrentExp, _runtime.RequiredExp, _runtime.Level); // 경험치 변경 이벤트 발동
        
        for(int i = 0; i < levelUps; i++)
        {
            OnLevelUp?.Invoke(_runtime.Level);
            Debug.Log($"Level Up! -> Lv.{_runtime.Level}");
        }
    }
}