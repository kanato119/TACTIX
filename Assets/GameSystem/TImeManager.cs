using TMPro;
using UnityEngine;
using UnityEngine.Analytics;
using Unity.Netcode;
using UnityEngine.Rendering;

/// <summary>
/// サーバー側から処理する時間処理
/// </summary>
public class TimeManager : NetworkBehaviour
{
    /*
    // 制限時間用のテキスト
    [SerializeField] TextMeshProUGUI TimerText;

    public static bool Gamestarted = false;

    public static bool Gameended = false;
    // 制限時間（5分）
    public float timeLimit = 300f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Gameended = false;
        Gamestarted = false;
        TimerText.text = timeLimit.ToString("F0");
    }

    // Update is called once per frame
    void Update()
    {

        // 試合開始前は処理しない
        if (!Gamestarted || Gameended)
            return;
        // 時間を減らす
        timeLimit -= Time.deltaTime;

        // テキストを更新
        TimerText.text = timeLimit.ToString("F0");
        // 5分経過
        if (timeLimit <= 0)
        {
            timeLimit = 0;
            DeathMatchResult.Instance.CheckWinner();
        }
    }


     */

    public static TimeManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    /// <summary>
    /// TimeManagerが破棄されたときにシングルトンの登録を解除する処理
    /// </summary>
    public override void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }

        base.OnDestroy();
    }

    /// <summary>
    /// サーバ側でしかTimerをいじれないようにする
    /// </summary>
    public NetworkVariable<float> Timer = new NetworkVariable<float>(
        GameConst.MAX_DEATHMATCH_TIME,
        NetworkVariableReadPermission.Everyone,
        NetworkVariableWritePermission.Server);

    /// <summary>
    /// 時間をサーバーにセットする関数
    /// </summary>
    private void SetTime(float time)
    {
        if ((!IsServer)) return;
        Timer.Value = time;
       
    }

    /// <summary>
    /// ホストのスポーン時に初期化する関数
    /// </summary>
    public override void OnNetworkSpawn()
    {
        if (!IsServer) return;
        SetTime(GameConst.MAX_DEATHMATCH_TIME);
        
    }

    /// <summary>
    /// 終了時間時の処理
    /// </summary>
    private void TimeUp()
    {



    }



}
