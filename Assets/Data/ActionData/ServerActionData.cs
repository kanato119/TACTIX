
using UnityEngine;
using System;


/*
 *参考サイト
 https://learn.microsoft.com/ja-jp/dotnet/csharp/programming-guide/delegates/
 https://qiita.com/rion0726ittoti/items/c908756247285568ff4a
 https://qiita.com/YouKnow/items/d6823afa7f676f309340

  デリゲート
    メソッドそのものを変数のように保持できる仕組み

  Action
    戻り値を持たないメソッドを保持できるデリゲート


  Func
    戻り値を持つメソッドを保持するデリゲート


 */

/// <summary>
/// 依存関係を無くすためのイベントシステム
/// </summary>
public class ServerActionData
{

    // 時間制限
    public event Action<float> OnTimerChanged;
    public event Action OnTimerEnd;

    // ゲーム進行
    public event Action OnGameStart;
    public event Action OnGameEnd;
    public event Action OnMatchReset;

    // 勝利した側
    public event Action<int> OnWinnerChanged;

    // プレイヤー参加・退出
    public event Action<ulong> OnPlayerJoined;
    public event Action<ulong> OnPlayerLeft;

    // プレイヤー死亡確定
    public event Action<ulong> OnPlayerDeathConfirmed;

    // スコア変更
    public event Action<ulong, int> OnScoreChanged;

    // イベント発火用メソッド


    /// <summary>
    /// 使い方
    /// On～～ ←使いたい（発火）したいイベント名
    /// ? ←　null出ない時だけ
    /// Invoke() ← 登録されたメソッドを実行
    /// </summary>
    

    //下に書かれている関数を書けばイベントを発火できるようになっています


    // 時間制限
    public void TimerChanged(float time)
        => OnTimerChanged?.Invoke(time);

    public void TimerEnd()
        => OnTimerEnd?.Invoke();

    // ゲーム進行
    public void GameStart()
        => OnGameStart?.Invoke();

    public void GameEnd()
        => OnGameEnd?.Invoke();

    public void MatchReset()
        => OnMatchReset?.Invoke();

    // 勝利した側
    public void WinnerChanged(int winnerID)
        => OnWinnerChanged?.Invoke(winnerID);

    // プレイヤー参加・退出
    public void PlayerJoined(ulong clientID)
        => OnPlayerJoined?.Invoke(clientID);

    public void PlayerLeft(ulong clientID)
        => OnPlayerLeft?.Invoke(clientID);

    // プレイヤー死亡確定
    public void PlayerDeathConfirmed(ulong clientID)
        => OnPlayerDeathConfirmed?.Invoke(clientID);

    // スコア変更
    public void ScoreChanged(ulong clientID, int score)
        => OnScoreChanged?.Invoke(clientID, score);

}
