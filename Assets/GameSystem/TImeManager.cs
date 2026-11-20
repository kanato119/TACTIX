using TMPro;
using UnityEngine;
using UnityEngine.Analytics;

public class TimeManager : MonoBehaviour
{
    // 制限時間用のテキスト
    [SerializeField] TextMeshProUGUI TimerText;

    


    public static bool Gamestarted = false;

    public static bool Gameended = false;
    // 制限時間（5分）
    public float timeLimit = 300f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Gameended = false;
        Gamestarted = false;
        TimerText.text = timeLimit.ToString("F0");
    }

    // Update is called once per frame
    void Update()
    {

        // 試合開始前は処理しない
        if (!Gamestarted || Gameended)
            return;
        // 時間を減らす
        timeLimit -= Time.deltaTime;

        // テキストを更新
        TimerText.text = timeLimit.ToString("F0");
        // 5分経過
        if (timeLimit <= 0)
        {
            timeLimit = 0;
            DeathMatchResult.Instance.CheckWinner();
        }
    }
}
