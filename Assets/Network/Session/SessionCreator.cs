using System.Threading.Tasks;
using Unity.Services.Multiplayer;
using UnityEngine;


public class SessionCreator : MonoBehaviour
{

    //ルーム・サーバー作成
    //ルーム・サーバー設定

    private string _playerID;

    /// <summary>
    /// ホストが持つ変数
    /// https://docs.unity3d.com/Packages/com.unity.services.multiplayer@1.1/api/Unity.Services.Multiplayer.IHostSession.html
    /// </summary>
    private IHostSession _session;

   

    void Start()
    {
        DontDestroyOnLoad(gameObject);

        if (SessionManager.instance == null) return;

        _playerID = SessionManager.instance.GetPlayerID();
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// セッション作成ボタン
    /// </summary>
    public async void CreateSessionButton()
    {


        await CreateSession();
    }


    /// <summary>
    /// 他プレイヤーが参加できるセッションの作成
    /// 参考　https://docs.unity.com/en-us/mps-sdk/create-session?utm_source=chatgpt.com
    /// </summary>
    private async Task CreateSession()
    {
        SessionOptions options = null;

        options = SessionManager.instance.GetSessionOptions();

        _session = await MultiplayerService.Instance.CreateSessionAsync(options);

        SceneSelectManager.instance.ChangeNetSceneSingle(GameConst.SCENE_TEST_PLAYER);

     
    }

    //自身がホストかの可否
    public bool GetHost()
    {
        return _session != null;
    }



}
