using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class LoadSceneManager : MonoBehaviour
{

    [SerializeField]
    private TextMeshProUGUI _laod_Text;


    /*
     やりたいこと
   
    マッチング専用＝＝＝＝＝＝＝＝＝＝＝＝

    マッチングボタンを押す
    マッチメイキング画面でアイコンを表示し通信中と書いて。アニメーションさせる。
　  マッチングが完了したら「見つかりました」と表示する
    共通の処理を行う

    共通の処理＝＝＝＝＝＝＝＝＝＝＝＝＝＝

    通信中のアイコンをアニメーションさせて
    次のシーンの読み込みが完了したらフェードを行い
    次のシーンへ
    ＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝

     */


    /// <summary>
    /// SceneSelectManagerで呼ぶ処理
    /// </summary>
    /// <returns></returns>
    public void LoadNextScene()
    {


        LoadScene();
    }


    /// <summary>
    /// アイ・イナムレイター / アイ・イニューマレイター　
    /// 非同期のLoading処理
    /// 遷移先のシーンが読み込み完了するまでローディング画面を表示する処理
    /// </summary>
    /// <returns></returns>
    private IEnumerator LoadScene()
    {

        yield break;

    }
}
