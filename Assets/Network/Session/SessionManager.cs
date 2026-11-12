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

            if (UnityServices.State != ServicesInitializationState.Initialized)
            {
                //回線の初期化(Unity Gaming Servies SDK)
                await UnityServices.InitializeAsync();
            }

            // 匿名認証
            if (!AuthenticationService.Instance.IsSignedIn)
            {
                //匿名で認証する
                await AuthenticationService.Instance.SignInAnonymouslyAsync();
            }

            //PlayerIDの取得
            Debug.Log($"Sign in anonymously succeeded! PlayerID:{AuthenticationService.Instance.PlayerId}");
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
        SceneSelectManager.instance.ChangeLocalScene(GameConst.SCENE_TEST_NET);
    }
    /// <summary>
    /// 選択したゲームモードの値を取得
    /// </summary>
    public string GetGameTypeValue()
    {
        return ((int)selectGameType).ToString();
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
                        GetGameTypeValue(),
                        VisibilityPropertyOptions.Public,//他プレイヤーから参照できるように
                        PropertyIndex.Number1
                    )
                }
                }
        };
    }
}
