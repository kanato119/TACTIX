using UnityEngine;
using Unity.Services.Core;
using System;
using Unity.Services.Authentication;
using System.Threading.Tasks;

public class SessionManager 
{
    public static SessionManager instance
    {
        get
        {
            if (_instance == null) { 
                SessionManager
                    _instance = new SessionManager();
            }

            return _instance;
        }
    }
    private static SessionManager _instance;

    private SessionManager() { }

    //https://docs.unity.com/en-us/mps-sdk/init-ugs?utm_source=chatgpt.com
    public async Task Initalize()
    {
        try
        {
            //回線の初期化(Unity Gaming Servies SDK)
            await UnityServices.InitializeAsync();
            //匿名で認証する
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
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



}
