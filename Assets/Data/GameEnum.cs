
/// <summary>
/// ゲームの種類
/// </summary>
public enum eGameType
{
    Invalid = -1,

    /// <summary>
    /// デスマッチ
    /// </summary>
    Deathmatch,

    /// <summary>
    /// 通常マッチ
    /// </summary>
    TeamBattle,
}

/// <summary>
/// 武器の種類
/// </summary>
public enum eWeapons
{
    Invalid = -1,

    /// <summary>
    /// ナイフ（近接武器）
    /// </summary>
    Knife,

    /// <summary>
    /// ピストル
    /// </summary>
    Pistol,

    /// <summary>
    /// アサルトライフル
    /// </summary>
    Assault,
}

public enum eSubWepon
{
    Inavalid = -1,
 
    /// <summary>
    /// ピストル
    /// </summary>
    Pistol,

}

public enum eMainWeapon
{
    Inavalid = -1,

    /// <summary>
    /// アサルトライフル
    /// </summary>
    Assault,
}


/// <summary>
/// Playerのステータス
/// </summary>
public enum eMatchPosition
{
    Inavalid = -1,

    Enemy,

    Defense,

    Attack,
}