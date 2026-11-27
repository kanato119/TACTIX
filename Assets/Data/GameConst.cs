
using Unity.Android.Gradle;

public class GameConst
{


    //=======================================================================
    //   シーンの名前の定数
    //=======================================================================
    public static readonly string SCENE_TITLE /*= 作成していない*/;
    public static readonly string SCENE_GAME_MAIN = "MainGame";
    public static readonly string SCENE_GAME_SELECT = "GameSelect";
    public static readonly string SCENE_GAME_LOBYY = "GameLobby";
    public static readonly string SCENE_GAME_LOADING = "GameLoading";
    public static readonly string SCENE_GAME_DEATHMATCH = "DeathMatch";
    public static readonly string SCENE_GAME_TEAMBATTLE = "TeamBattle";


    //=======================================================================
    //   Debug用のシーンの名前の定数
    //=======================================================================
    public static readonly string SCENE_TEST_NET = "Test_NetScene";
#if UNITY_EDITOR

    public static readonly string SCENE_TEST_PLAYER = "Player";

#endif

    //=======================================================================
    //   マッチの共通の処理定数
    //=======================================================================

    //最大人数
    public const int MAX_MATCH_COUNT = 8;
    


    //=======================================================================
    //   デスマッチの定数
    //=======================================================================

    //デスマッチの時間
    public const float MAX_DEATHMATCH_TIME = 300.0f;



    //=======================================================================
    //   アンレートの定数
    //=======================================================================

    //チームバトルの時間
    public const float MAX_TEAMBATTLE_TIME = 120.0f;

    public static readonly string POINT_MATCHPOINT_TEXT = "MATCHPOINT";
    public static readonly string PHASE_PURCHASE_TEXT = "購入フェーズ";
    //役割を伝えるUIに使う定数
    public static readonly string POSITION_DEFENCE_TEXT = "Defender";
    public static readonly string POSITION_ATTACK_TEXT = "Attacker";



    //=======================================================================
    //   プレイヤーの定数
    //=======================================================================

    public const int MAX_HP = 150;

    //=======================================================================
    //   テキスト表示の定数
    //=======================================================================

    public static readonly string TEXT_LOADSCENE = "読み込み中...";
    public static readonly string TEXT_CONNECTING = "接続中...";
    public static readonly string TEXT_WAITING = "待機中...";
    public static readonly string TEXT_MATCHING = "マッチング中...";
    public static readonly string TEXT_GAMESTARTS = "ゲーム開始";
    public static readonly string TEXT_GAMEENDS = "ゲーム終了";

 
}

