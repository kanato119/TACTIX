
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Services.Multiplayer;
using Unity.Netcode;
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

    //マッチメイキングの二重実行を防止
    private bool _isMatching;


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
        //すでにマッチメイキング中なら実行しない
        if (_isMatching)
        {
            Debug.LogWarning("マッチメイキング中です");
            return;
        }

        _isMatching = true;

        ///try（実行）
        ///例外が発生する可能性のある処理を実行する
        ///参考 https://feynman.co.jp/unityforest/unity-introduction/unity-csharp-programming/throw-try-catch-finally/
        try
        {
            await AutoSession();
        }
        ///catch（エラーを受け取る）
        ///発生したエラーを e という変数に格納して処理する
        ///エラー情報を受け取るための変数を宣言している
        catch (System.Exception e)
        {
            Debug.LogError($"マッチメイキング失敗:\n{e}");

            //InnerException
            //発生したエラーの原因となっている、さらに内側のエラー情報を取得することができる
            if (e.InnerException != null)
            {
                Debug.LogError($"InnerException:\n{e.InnerException}");
            }
        }
        ///finally（成功・失敗に関わらず必ず実行）
        finally
        {
            _isMatching = false;
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
        //sessionの作成したオプションを取得
        SessionOptions options = null;

        options = SessionManager.instance.GetSessionOptions();

        _session = await MultiplayerService.Instance.CreateSessionAsync(options);


        //ネットワークの起動状態を確認
        DebugNetworkState();

        //Host側だけがネットワークシーンを変更する
        if (NetworkManager.Singleton != null &&
            NetworkManager.Singleton.IsServer)
        {
            SceneSelectManager.instance.ChangeNetSceneSingle(
                GameConst.SCENE_GAME_DEATHMATCH
            );
        }
        else
        {
            Debug.LogError("セッション作成後、Hostとして起動できていません");
        }
    }



    /// <summary>
    /// ゲームモードにあったセッションのマッチメイキング
    /// </summary>
    /// <returns></returns>
    private async Task JoinServer()
    {

        // ゲームモードの取得
        string gameType = SessionManager.instance.GetGameTypeString();

        Debug.Log($"検索するGameType: {gameType}");

        ///MPS（Multiplayer Services）で参加可能なセッションを検索するために使用するクラス
        var queryOptions = new QuerySessionsOptions
        {
            Count = 20,

            //検索条件
            FilterOptions = new List<FilterOption>
            {
                //ここでList<FilterOption>に検索条件を設定する
                new FilterOption(
                FilterField.NumberIndex1,//gametypeで
                gameType,//自分が選択したゲームモードの値
                FilterOperation.Equal//等しい
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


            //ネットワークの起動状態を確認
            DebugNetworkState();

            //Client側ではシーン遷移を実行しない
            //Host側のネットワークシーン遷移に同期させる

            return;
        }

    }

    /// <summary>
    /// NGOのネットワーク状態を確認する
    /// </summary>
    private void DebugNetworkState()
    {
        NetworkManager manager = NetworkManager.Singleton;

        if (manager == null)
        {
            Debug.LogError("NetworkManager.Singletonが存在しません");
            return;
        }

        Debug.Log($"IsListening: {manager.IsListening}");
        Debug.Log($"IsHost: {manager.IsHost}");
        Debug.Log($"IsServer: {manager.IsServer}");
        Debug.Log($"IsClient: {manager.IsClient}");
        Debug.Log($"ClientID: {manager.LocalClientId}");
    }

}
