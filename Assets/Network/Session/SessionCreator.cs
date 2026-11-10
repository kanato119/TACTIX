using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Unity.Services.Multiplayer;
using UnityEngine;


public class SessionCreator : MonoBehaviour
{

    //ルーム・サーバー作成
    //ルーム・サーバー設定

    private string _playerID;
    private int _gameTypeID;



    SelectGameType selectGameType;

    void Start()
    {
        if (SessionManager.instance == null) return;

        _playerID = SessionManager.instance.GetPlayerID();

        if (selectGameType == null) return;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void SaveGameType()
    {
        if (selectGameType == null) return;
        _gameTypeID = selectGameType.GetGameType();
        if (selectGameType.GetGameType() == -1) return;

    }


    /// <summary>
    /// 他プレイヤーが参加できるセッションの作成
    /// 参考　https://docs.unity.com/en-us/mps-sdk/create-session?utm_source=chatgpt.com
    /// </summary>
    private async Task CreateSession()
    {
        SaveGameType();

        SessionOptions options = null;

        switch (_gameTypeID)
        {

            case (int)eGameType.Deathmatch:
                Debug.Log("デスマッチのルーム作成");
                options = new SessionOptions
                {
                    MaxPlayers = GameConst.MAX_DEATHMATCH_COUNT,
                   
                }.WithRelayNetwork();
                break;
            case (int)eGameType.Unrated:
                Debug.Log("アンレートのルーム作成");
                break;

        }
        var session = await MultiplayerService.Instance.CreateSessionAsync(options);


    }


    private async Task JoinSession()
    {
        IEnumerable o;
        SaveGameType();
        QuerySessionsOptions options = new QuerySessionsOptions
        {
            FilterOptions = new List<FilterOption> { 
            new FilterOption(
                FilterField.NumberIndex1,
                "SSSSS",
                FilterOperation.Contains
                )
            }

        };

        await MultiplayerService.Instance.QuerySessionsAsync(options);



        

    }


}
