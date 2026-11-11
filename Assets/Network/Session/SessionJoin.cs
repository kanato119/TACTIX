using System.Collections.Generic;
using Unity.Services.Multiplayer;
using UnityEngine;

public class SessionJoin
{
    //参加できるセッションの表示

    //シーンをゲームロビーに遷移させる


    private void JoinServer()
    {

        QuerySessionsOptions options = new QuerySessionsOptions
        {
            FilterOptions = new List<FilterOption>
            {

                

            }

        };

         MultiplayerService.Instance.QuerySessionsAsync(options);





    }



}
