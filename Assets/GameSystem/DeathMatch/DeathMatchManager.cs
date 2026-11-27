using UnityEngine;
using Unity.Netcode;
using System.Collections;

/// <summary>
/// デスマッチだけのシステムを管理するクラス
/// </summary>
public class DeathMatchManager:NetworkBehaviour
{

    private static DeathMatchManager _instance;

    public static DeathMatchManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new DeathMatchManager();
            }
            return _instance;
        }
    }
    private DeathMatchManager() { }

    private TimeManager _time;

    public void GameStart()
    {
        //ここでPlayerPositionをEnemyに設定する
        //別クラスでやらせる

        _time.SetTime(GameConst.MAX_DEATHMATCH_TIME);


        GameUpdate();
    }

    /// <summary>
    /// 試合中の処理
    /// </summary>
    public IEnumerator GameUpdate()
    {

        //ゲームのステータスで処理を分ける
        yield return null;


    }

    public void GameFinish()
    {
        //リザルト画面に飛ばす


    }

  

}