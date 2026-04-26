using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 경험치 바 UI를 관리하는 클래스
/// </summary>
public class ExpBarUI : MonoBehaviour
{
    [SerializeField] private ExperienceSystem _expSystem;
    [SerializeField] private Slider _slider;
    [SerializeField] private Text _levelText; // 레벨 표시용 텍스트

    private void OnEnable()
    {
        if(_expSystem != null)
            _expSystem.OnExpChanged += HandleExpChanged;
    }
    
    private void OnDisable()
    {
        if (_expSystem == null) return;
        _expSystem.OnExpChanged -= HandleExpChanged;
    }

    private void HandleExpChanged(int current, int required, int level)
    {
        _slider.value = required > 0 ? (float)current / required : 0f;
        if (_levelText != null) _levelText.text = $"Lv.{level}";
    }

}
