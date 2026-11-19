using UnityEngine;


/// <summary>
/// 銃のステータスの管理するクラス
/// </summary>
public class WeaponData
{
    public eWeapons eWeapons { get; private set; }

    //メイン武器かどうか
    public bool IsMainWeapon;

    // 弾薬
    public int MaxAmmo;
    public int MaxTotalAmmo;

    // 射撃性能
    public float Damage;
    // リロード
    public float ReloadTime;
    //連射速度
    public float ShootInterval;

    // 連射可能
    public bool IsRapidFire;

    public WeaponData(
       eWeapons type,
       bool isMainWeapon,
       int maxAmmo,
       int maxTotalAmmo,
       float damage,
       float reloadTime,
       float shootInterval,
       bool rapidFire
        )
    {
        eWeapons = type;
        IsMainWeapon = isMainWeapon;
        MaxAmmo = maxAmmo;
        MaxTotalAmmo = maxTotalAmmo;
        Damage = damage;
        ReloadTime = reloadTime;
        ShootInterval = shootInterval;
        IsRapidFire = rapidFire;
    }
    
}
