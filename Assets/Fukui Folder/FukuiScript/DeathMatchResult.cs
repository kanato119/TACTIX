
using UnityEngine;

public class DeathMatchResult : MonoBehaviour
{
    public RulesDeathMatch[] players;
    // 試合開始判定
    public static bool GameStarted = false;
    // 制限時間（5分）
    public float timeLimit = 300f;

    // ゲーム終了判定（全プレイヤーで共有）
    public static bool GameEnded = false;

    void Start()
    {
        GameStarted = false;
        GameEnded = false;
    }

    void Update()
    {
        // 試合開始前は処理しない
        if (!GameStarted || GameEnded)
            return;

        // 25キルに到達したプレイヤーがいるか確認
        foreach (RulesDeathMatch player in players)
        {
            if (player == null)
                continue;

            if (player.Kills >= 25)
            {
                Debug.Log(player.gameObject.name + " の勝ち！");
                GameEnded = true;
                return;
            }
        }
        // 時間を減らす
        timeLimit -= Time.deltaTime;

        // 5分経過
        if (timeLimit <= 0)
        {
            timeLimit = 0;
            CheckWinner();
        }
    }

    public void CheckWinner()
    {
        GameEnded = true;

        int maxKills = -1;

        // 最大キル数を取得
        foreach (RulesDeathMatch player in players)
        {
            if (player == null)
                continue;

            if (player.Kills > maxKills)
            {
                maxKills = player.Kills;
            }
        }

        // 勝者を探す
        foreach (RulesDeathMatch player in players)
        {
            if (player == null)
                continue;

            if (player.Kills == maxKills)
            {
                Debug.Log(player.gameObject.name + " の勝ち！");
            }
        }
    }
}
