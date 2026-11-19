using TMPro;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DebugText : MonoBehaviour
{
    public static DebugText Instance;

    public TextMeshProUGUI debugText;

    private List<string> logs = new List<string>();

    // ログごとの削除処理を管理
    private List<Coroutine> logCoroutines = new List<Coroutine>();

    void Awake()
    {
        Instance = this;
        debugText.text = "";
    }

    public void Show(string message)
    {
        // ログを追加
        logs.Add(message);

        // 3件を超えたら一番古いログを削除
        if (logs.Count > 3)
        {
            logs.RemoveAt(0);
            StopCoroutine(logCoroutines[0]);
            logCoroutines.RemoveAt(0);
        }

        // 画面に表示
        UpdateText();

        // 3秒後に消す処理を開始
        Coroutine coroutine = StartCoroutine(KillText(message));
        logCoroutines.Add(coroutine);
    }

    IEnumerator KillText(string message)
    {
        yield return new WaitForSeconds(3f);

        // 対象のログを削除
        int index = logs.IndexOf(message);

        if (index >= 0)
        {
            logs.RemoveAt(index);
            logCoroutines.RemoveAt(index);
        }

        UpdateText();
    }

    void UpdateText()
    {
        debugText.text = string.Join("\n", logs);
    }
}