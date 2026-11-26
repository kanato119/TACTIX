using TMPro;
using UnityEngine;
using Unity.Netcode;
using System.Collections;

/// <summary>
/// サーバー側から処理する時間処理
/// </summary>
public class TimeManager : NetworkBehaviour
{
    public static TimeManager Instance { get; private set; }

    public bool isGames { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    /// <summary>
    /// TimeManagerが破棄されたときにシングルトンの登録を解除する処理
    /// </summary>
    public override void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }

        base.OnDestroy();
    }

    /// <summary>
    /// サーバ側でしかTimerをいじれないようにする
    /// </summary>
    public NetworkVariable<float> timer = new NetworkVariable<float>(
        GameConst.MAX_DEATHMATCH_TIME,
        NetworkVariableReadPermission.Everyone,
        NetworkVariableWritePermission.Server);

    private int gameType;
    private void GetGameType()
    {
        if (SessionManager.instance == null) return;
        gameType = SessionManager.instance.GetGameTypeValue();
    }

    /// <summary>
    /// 時間をサーバーにセットする関数
    /// </summary>
    private void SetTime()
    {
        if ((!IsServer)) return;

        GetGameType();

        //ゲームタイプに応じてタイマーの初期値や、システムを設定する
        switch (gameType)
        {
            case (int)eGameType.Deathmatch:
                timer.Value = GameConst.MAX_DEATHMATCH_TIME;
                break;
            case (int)eGameType.TeamBattle:
                break;
            default:
                Debug.LogError("想定していないゲームタイプです!!");
                break;
        }

        //０以下にならないよう
        timer.Value = Mathf.Max(timer.Value, 0.0f);
    }

    /// <summary>
    /// ホストのスポーン時に初期化する関数
    /// </summary>
    public override void OnNetworkSpawn()
    {
        if (!IsServer) return;

    }

    public void GameStart()
    {
        if (!IsServer) return;
        if (isGames) return;

        SetTime();

        isGames = true;

        //StartCoroutine
        //コルーチンを開始するために使用されるUnityのメソッド
        //処理を一時停止して、指定された時間が経過した後に再開することができる
        //今回は、サーバー側でタイマーを管理するために使用される（毎フレーム同機処理を行いたくないから）
        StartCoroutine(TimerServer());
    }

    /// <summary>
    /// 終了時間時の処理
    /// </summary>
    private void TimeUp()
    {
        isGames = false;

        //ゲームタイプに応じて勝者を決定する処理を呼び出す
        switch (gameType)
        {
            case (int)eGameType.Deathmatch:
                DeathMatchResult.Instance.CheckWinner();
                break;
            case (int)eGameType.TeamBattle:
                break;
            default:
                Debug.LogError("想定していないゲームタイプです!!");
                break;
        }

    }


    private IEnumerator TimerServer()
    {
        // 試合開始前・試合終了後は処理しない
        if (!isGames) yield break;

        while (timer.Value > 0)
        {
            ///1秒ごとにタイマーを減らす
            yield return new WaitForSeconds(1f);

            // 試合が途中で終了した場合は処理を抜ける
            if (!isGames) yield break;

            timer.Value--;

            if (timer.Value <= 0)
            {
                TimeUp();
                yield break;
            }
        }
    }
}
