
using TMPro;
using UnityEngine;

public class DeathMatchResult : MonoBehaviour
{
    // インスタンス化
    public static DeathMatchResult Instance;
    // プレイヤーリスト
    public RulesDeathMatch[] players;

    // 試合開始判定
    public static bool GameStarted = false;

    // ゲーム終了判定（全プレイヤーで共有）
    public static bool GameEnded = false;

    void Start()
    {
        GameStarted = false;
        GameEnded = false;

        GetPlayers();
    }
    void Awake()
    {
        // 重複していないか
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // プレイヤーを全員取得
    void GetPlayers()
    {
        players = FindObjectsByType<RulesDeathMatch>(
            FindObjectsSortMode.None
        );
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
                TimeManager.Gameended=true;
                GameEnded = true;
                return;
            }
        }
 
        if (Input.GetKeyDown(KeyCode.C)){ 
        
        GetPlayers();
        }
    }
    // 勝ちを探す
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
