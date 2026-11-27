
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
    public event Action<int> OnAmmoChanges;

    // 移動・停止可否
    public event Action<bool> OnPlayerMoveStateChanged;

    // ジャンプ
    public event Action OnPlayerJumped;

    // 着地
    public event Action OnPlayerLanded;

    // 走行状態
    public event Action<bool> OnPlayerRunStateChanged;

    // リロードキャンセル
    public event Action OnWeaponReloadCancelled;

    // 復活完了
    public event Action OnPlayerRespawned;


    // =========================
    // イベント発火用メソッド
    //　コードの意味はServer側に書いてます
    // =========================
    //下に書かれている関数を書けばイベントを発火できるようになっています
    // 被弾
    public void PlayerDamaged(int damage)
        => OnPlayerDamaged?.Invoke(damage);

    // 発射可否
    public void ShootStateChanged(bool canShoot)
        => OnShootStateChanged?.Invoke(canShoot);

    // HP変更
    public void HealthChanged(int hp)
        => OnHealthChanged?.Invoke(hp);

    // 死亡
    public void PlayerDeath()
        => OnPlayerDeath?.Invoke();

    // リロード開始
    public void ReloadStarted()
        => OnReloadStarted?.Invoke();

    // リロード完了
    public void ReloadCompleted()
        => OnReloadCompleted?.Invoke();

    // 武器変更
    public void WeaponChanged(int weaponID)
        => OnWeaponChanged?.Invoke(weaponID);

    // リスポーン
    public void PlayerRespawn()
        => OnPlayerRespawn?.Invoke();

    // 弾数変更
    public void AmmoChanged(int ammo)
        => OnAmmoChanges?.Invoke(ammo);

    // 移動・停止可否
    public void PlayerMoveStateChanged(bool canMove)
        => OnPlayerMoveStateChanged?.Invoke(canMove);

    // ジャンプ
    public void PlayerJumped()
        => OnPlayerJumped?.Invoke();

    // 着地
    public void PlayerLanded()
        => OnPlayerLanded?.Invoke();

    // 走行状態
    public void PlayerRunStateChanged(bool isRunning)
        => OnPlayerRunStateChanged?.Invoke(isRunning);

    // リロードキャンセル
    public void WeaponReloadCancelled()
        => OnWeaponReloadCancelled?.Invoke();

    // 復活完了
    public void PlayerRespawned()
        => OnPlayerRespawned?.Invoke();
}
