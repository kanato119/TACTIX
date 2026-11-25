using TMPro;
using UnityEngine;

public class TimerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;

    private void Start()
    {
        if (TimeManager.Instance == null) return;

        // タイマーの変更を監視
        TimeManager.Instance.timer.OnValueChanged += UpdateTimerText;

        // 最初の表示を更新
        UpdateTimerText(0, TimeManager.Instance.timer.Value);
    }

    private void OnDestroy()
    {
        if (TimeManager.Instance == null) return;

        // イベントの登録解除
        TimeManager.Instance.timer.OnValueChanged -= UpdateTimerText;
    }

    private void UpdateTimerText(float previousValue, float newValue)
    {
        int time = Mathf.CeilToInt(newValue);

        int minutes = time / 60;
        int seconds = time % 60;

        timerText.text = $"{minutes:00}:{seconds:00}";
    }
}