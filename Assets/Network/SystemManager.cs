using UnityEngine;
//ネットの非同期処理に必用
using System.Threading.Tasks;


public class SystemManager : MonoBehaviour
{
    //初期化
    private async void Start()
    {
        //Manager関連の初期化
        await SessionManager.instance.Initalize();


        //Nullチェック
        if (SessionManager.instance == null) return;
        if (SceneSelectManager.instance == null) return;

    }
}
