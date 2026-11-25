
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


    //=======================================================================
    //   Debug用のシーンの名前の定数
    //=======================================================================
    public static readonly string SCENE_TEST_NET = "Test_NetScene";
#if UNITY_EDITOR
    public static readonly string SCENE_TEST_PLAYER = "Player";

#endif

    //=======================================================================
    //   デスマッチの定数
    //=======================================================================


    //最大人数
    public const int MAX_MATCH_COUNT = 6;

    public const float MAX_DEATHMATCH_TIME = 300.0f;



    //=======================================================================
    //   アンレートの定数
    //=======================================================================




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

