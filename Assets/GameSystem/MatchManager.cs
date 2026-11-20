using Unity.VisualScripting;
using UnityEngine;



/// <summary>
/// ゲーム進行のクラス
/// </summary>
public class MatchManager 
{

    //マッチの共通の処理をここで書いて
    //eGameTypeの要素数を取得してSwhich文で分ける


    SessionManager selectGame;



    private void GameUpdate(int value)
    {
        if (selectGame != null) return;

        //選択したゲームモードによって内部のシステムを変える
        value = selectGame.GetGameTypeValue();
        switch (value)
        {
            case (int)eGameType.Deathmatch:

                break;
            case (int)eGameType.Unrated:

                break;
        }

    }


}
