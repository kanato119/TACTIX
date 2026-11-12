using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Services.Multiplayer;
using UnityEngine;


public class SessionJoin
{
    //参加できるセッションの表示

    //シーンをゲームロビーに遷移させる

    private ISession _sessionJoiner;

    /// <summary>
    /// マッチメイキングボタン
    /// </summary>
    /// <returns></returns>
    public async Task JoinButton()
    {

        await JoinServer();

    }


    /// <summary>
    /// ゲームモードにあったセッションのマッチメイキング
    /// </summary>
    /// <returns></returns>
    private async Task JoinServer()
    {
        // ゲームモードの取得
        string gameType = SessionManager.instance.GetGameTypeValue();

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
            return;
        }


        foreach (var sessionInfo in result.Sessions)
        {

            //条件に合ったセッションに参加
            _sessionJoiner = await MultiplayerService.Instance.JoinSessionByIdAsync(sessionInfo.Id);

            return;
        }

    }



}
