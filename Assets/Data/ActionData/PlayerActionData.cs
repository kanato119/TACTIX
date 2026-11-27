using UnityEngine;
using System;

/// <summary>
/// Player側のアクション
/// </summary>
public class PlayerActionData
{
    // 被弾
    public event Action<int> OnPlayerDamaged;

    // 発射可否
    public event Action<bool> OnShootStateChanged;

    // HP変更
    public event Action<int> OnHealthChanged;

    // 死亡
    public event Action OnPlayerDeath;

    // リロード開始
    public event Action OnReloadStarted;

    // リロード完了
    public event Action OnReloadCompleted;

    // 武器変更
    public event Action<int> OnWeaponChanged;

    // リスポーン
    public event Action OnPlayerRespawn;

    // 弾数変更
    public event Action<int> OnAmmoChanged;
    //移動・停止可否
    public event Action<bool> OnPlayerMoveStateChanged;
    //ジャンプ
    public event Action OnPlayerJumped;
    //着地
    public event Action OnPlayerLanded;
    //走行状態
    public event Action<bool> OnPlayerRunStateChanged;
    //リロードキャンセル
    public event Action OnWeaponReloadCancelled;
    //復活完了
    public event Action OnPlayerRespawned;
    public event Action<int> OnAmmoChanged;



}