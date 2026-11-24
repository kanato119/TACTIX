using System.Collections;
using UnityEngine;
using TMPro;

public class DeathMatctCountDown : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI TimerText;
    // カウントダウンタイマー
    float limitTime = 5f;

    void Start()
    {
        TimerText.enabled = true;
        TimerText.text = limitTime.ToString("F0");
    }

    void Update()
    {
        if (limitTime > 0)
        {
            // タイマーを減らす
            limitTime -= Time.deltaTime;

            if (limitTime <= 0)
            {
                limitTime = 0;
                StartCoroutine(StartText());
                return;
            }

            // 0になる前だけ表示を更新
            TimerText.text = Mathf.CeilToInt(limitTime).ToString();
        }
    }

    IEnumerator StartText()
    {
        // start表示して2秒後には消す
        TimerText.text = "Start";

        // ここで試合開始！
        DeathMatchResult.GameStarted = true;
         //TimeManager.Gamestarted = true;
        yield return new WaitForSeconds(2f);

        TimerText.enabled = false;
    }
}