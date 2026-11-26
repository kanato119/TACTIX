using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;


/// <summary>
/// ゲーム進行のクラス
/// </summary>
public class MatchManager : NetworkBehaviour
{

    //マッチの共通の処理をここで書いて
    //eGameTypeの要素数を取得してSwhich文で分ける


    SessionManager selectGame;

    public void GetGameMode(int value)
    {


    }


    /// <summary>
    /// マッチ開始までの処理
    /// </summary>
    /// <param name="value"></param>
    private void WaitMatch(int value)
    {
        value = selectGame.GetGameTypeValue();

        switch (value)
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
    private void PlayMatch(int value)
    {
        value = selectGame.GetGameTypeValue();

        switch (value)
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
    private void MatchFinish(int value)
    {
        value = selectGame.GetGameTypeValue();
        switch (value)
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
