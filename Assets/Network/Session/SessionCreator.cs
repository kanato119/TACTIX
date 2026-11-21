using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Services.Multiplayer;
using UnityEngine;


public class SessionCreator : MonoBehaviour
{

    //ルーム・サーバー作成
    //ルーム・サーバー設定

    private string _playerID;

    /// <summary>
    /// ホストが持つ変数
    /// https://docs.unity3d.com/Packages/com.unity.services.multiplayer@1.1/api/Unity.Services.Multiplayer.IHostSession.html
    /// </summary>
    private IHostSession _session;

    private ISession _sessionJoiner;


    void Start()
    {
        DontDestroyOnLoad(gameObject);

        if (SessionManager.instance == null) return;

        _playerID = SessionManager.instance.GetPlayerID();
    }
    /// <summary>
    /// Buttonから呼び出す関数
    /// </summary>
    public async void OnClickAutoSession()
    {
        try
        {
            await AutoSession();
        }
        catch (System.Exception e)
        {
            Debug.LogError($"マッチメイキング失敗: {e}");
        }
    }
    /// <summary>
    /// 自動マッチメイキング
    /// </summary>
    /// <returns></returns>
    public async Task AutoSession()
    {
        //先に参加できるサーバーがあるか確認して参加する
        //参加できるサーバーがなかった場合自分がサーバーを開く
        await JoinServer();
    }

    /// <summary>
    /// 他プレイヤーが参加できるセッションの作成
    /// 参考　https://docs.unity.com/en-us/mps-sdk/create-session?utm_source=chatgpt.com
    /// </summary>
    private async Task CreateSession()
    {
        SessionOptions options = null;

        options = SessionManager.instance.GetSessionOptions();

        _session = await MultiplayerService.Instance.CreateSessionAsync(options);

        SceneSelectManager.instance.ChangeNetSceneSingle(GameConst.SCENE_TEST_PLAYER);
    }



    /// <summary>
    /// ゲームモードにあったセッションのマッチメイキング
    /// </summary>
    /// <returns></returns>
    private async Task JoinServer()
    {
        // ゲームモードの取得
        string gameType = SessionManager.instance.GetGameTypeString();

        var queryOptions = new QuerySessionsOptions
        {
            Count = 20,

            //検索条件
            FilterOptions = new List<FilterOption>
            {
                new FilterOption(
                FilterField.NumberIndex1,//gametypeで
                gameType,
                FilterOperation.Equal
                )
            }
        };

        //セッションの検索
        var result = await MultiplayerService.Instance.QuerySessionsAsync(queryOptions);

        if (result.Sessions.Count == 0)
        {
            Debug.Log("参加できるセッションがありません");
            await CreateSession();
            return;
        }


        foreach (var sessionInfo in result.Sessions)
        {

            //条件に合ったセッションに参加
            _sessionJoiner = await MultiplayerService.Instance.JoinSessionByIdAsync(sessionInfo.Id);

            SceneSelectManager.instance.ChangeNetSceneSingle(GameConst.SCENE_TEST_PLAYER);

            return;
        }

    }


}
