using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Multiplayer;
using UnityEngine;


/// <summary>
/// 自身のプレイヤーを匿名で登録して保存
/// サービスの初期化
/// 選択したゲームの種類を保存
/// </summary>
public class SessionManager
{
    private static SessionManager _instance;
    public static SessionManager instance
    {
        get
        {
            if (_instance == null)
            {

                _instance = new SessionManager();
            }

            return _instance;
        }
    }

    private SessionManager() { }


    public eGameType selectGameType { get; private set; }


    //https://docs.unity.com/en-us/mps-sdk/init-ugs?utm_source=chatgpt.com
    public async Task Initalize()
    {
        try
        {
            //初期化オプションを作成
            var options = new InitializationOptions();

            //UnityEditor上で実行されているParrelSyncのcloneかどうかの可否
#if UNITY_EDITOR
            if (ParrelSync.ClonesManager.IsClone())
            {
                // Cloneの識別IDを取得
                string cloneID = ParrelSync.ClonesManager.GetArgument();

                // Clone専用Profileを設定
                options.SetProfile($"Clone_{cloneID}_Profile");

                Debug.Log($"Clone Profile: Clone_{cloneID}_Profile");
            }
            else
            {
                Debug.Log("Host Profile: default");
            }
#endif

            //Unity Servicesの初期化
            if (UnityServices.State == ServicesInitializationState.Uninitialized)
            {
                await UnityServices.InitializeAsync(options);
            }
            else
            {
                Debug.LogWarning(
                    $"Unity Servicesはすでに初期化されています。State: {UnityServices.State}"
                );
            }

            //匿名認証
            if (!AuthenticationService.Instance.IsSignedIn)
            {
                await AuthenticationService.Instance.SignInAnonymouslyAsync();
            }

            //PlayerIDとProfileをコンソール上で確認するため
            Debug.Log($"PlayerID: {AuthenticationService.Instance.PlayerId}");
            Debug.Log($"Profile: {AuthenticationService.Instance.Profile}");
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
    }

    /// <summary>
    /// PlayerIDの取得
    /// </summary>
    /// <returns></returns>
    public string GetPlayerID()
    {
        return AuthenticationService.Instance.PlayerId;
    }

    /// <summary>
    /// ゲームの種類を保存
    /// </summary>
    /// <returns></returns>
    public void SaveGameType(eGameType gameType)
    {
        selectGameType = gameType;

        //保存したら対応のマッチ画面に遷移する
        SelectServer();
    }

    /// <summary>
    /// シーン遷移
    /// </summary>
    private void SelectServer()
    {
        if (SceneSelectManager.instance == null) return;
        //仮ゲームマッチ画面
        SceneSelectManager.instance.ChangeLocalScene(
    GameConst.SCENE_TEST_NET
);

    }
    /// <summary>
    /// 選択したゲームモードのStrngを取得
    /// </summary>
    public string GetGameTypeString()
    {
        return ((int)selectGameType).ToString();
    }
    /// <summary>
    /// 選択したゲームモードのStrngを取得
    /// </summary>
    public int GetGameTypeValue()
    {
        return ((int)selectGameType);
    }


    /// <summary>
    /// じぶんが選択したゲームの種類の取得
    /// </summary>
    /// <returns></returns>
    public SessionOptions GetSessionOptions()
    {
       

        return new SessionOptions
        {
            MaxPlayers = GameConst.MAX_MATCH_COUNT,

            SessionProperties =
                new Dictionary<string, SessionProperty>
                {
                {
                    "GameType",
                    new SessionProperty(
                        GetGameTypeString(),
                        VisibilityPropertyOptions.Public,//他プレイヤーから参照できるように
                        PropertyIndex.Number1
                    )
                }
                }
        }.WithRelayNetwork();


       
    }


}
