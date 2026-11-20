using UnityEngine;
//ネットの非同期処理に必用
using System.Threading.Tasks;


public class SystemManager : MonoBehaviour
{
    //初期化
    private async void Awake()
    {
        //Manager関連の初期化
        await SessionManager.instance.Initalize();



        if (SceneSelectManager.instance == null) return;

    }
}
