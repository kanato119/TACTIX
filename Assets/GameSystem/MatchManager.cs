using Unity.Netcode;
using UnityEngine;

/// <summary>
/// ゲーム進行のクラス
/// </summary>
public class MatchManager : NetworkBehaviour
{

    //マッチの共通の処理をここで書いて
    //eGameTypeの要素数を取得してSwhich文で分ける


    SessionManager selectGame;

    private int _gameType;
  
    public int GetGameMode()
    {
        _gameType = selectGame.GetGameTypeValue();
        return _gameType;

    }
    public override void OnNetworkSpawn()
    {
        if (!IsServer) return;
        if (selectGame == null) return;

        _gameType = GetGameMode();
    }

    /// <summary>
    /// マッチ開始までの処理
    /// </summary>
    /// <param name="value"></param>
    private void WaitMatch()
    {
        if (!IsServer) return;

        switch (_gameType)
        {
            case (int)eGameType.Deathmatch:
                //ここでセットする

                TimeManager.Instance.GameStart();

                break;
            case (int)eGameType.Unrated:
                //ここでセットする

                break;
        }

    }

    /// <summary>
    /// マッチ開始する時に呼ぶ関数
    /// </summary>
    /// <param name="value"></param>
    private void PlayMatch()
    {

        if (!IsServer) return;
        switch (_gameType)
        {
            case (int)eGameType.Deathmatch:
                //ここでセットする

                TimeManager.Instance.GameStart();

                break;
            case (int)eGameType.Unrated:
                //ここでセットする

                break;
        }


    }

    /// <summary>
    /// マッチ終了時に呼ぶ処理
    /// </summary>
    /// <param name="value"></param>
    private void MatchFinish()
    {
        if (!IsServer) return;
        switch (_gameType)
        {
            case (int)eGameType.Deathmatch:
                //ここでセットする

                break;
            case (int)eGameType.Unrated:
                //ここでセットする

                break;
        }
    }


}
