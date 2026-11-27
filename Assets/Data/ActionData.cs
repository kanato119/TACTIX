using UnityEngine;
using System;


/*
 *参考サイト
 https://learn.microsoft.com/ja-jp/dotnet/csharp/programming-guide/delegates/
 https://qiita.com/rion0726ittoti/items/c908756247285568ff4a
 https://qiita.com/YouKnow/items/d6823afa7f676f309340

  デリゲート
    メソッドそのものを変数のように保持できる仕組み
      
  Action
    戻り値を持たないメソッドを保持できるデリゲート


  Func
    戻り値を持つメソッドを保持するデリゲート

  
 */

/// <summary>
/// 依存関係を無くすためのイベントシステム
/// </summary>
public class ActionData
{

    public event Action OnGameStart;
    public event Action OnGameEnd;
    public event Action OnPlayerDeath;
    public event Action OnTimeUp;

}
