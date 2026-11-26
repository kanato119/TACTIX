using UnityEngine;

public class SubWeaponData : MonoBehaviour
{
    public eSubWepon eSubWepon { get; private set; }


    public int SubWeaponID;

    public int MainWeaponID;

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

    private void Awake()
    {
        SubWeaponDatas();
    }

    public void SubWeaponDatas()
    {
        int selectWeapon = (int)eSubWepon;
        SubWeaponID = selectWeapon;

        if (MasterData.instance == null)
        {
            Debug.LogError("MasterData.instance が null");
            return;
        }

        MasterData.instance.SubWeaponStatus(SubWeaponID);

        if (MasterData.instance.dataSubWeapon == null)
        {
            Debug.LogError("dataSubWeapon が null");
            Debug.LogError("SubWeaponID : " + SubWeaponID);
            return;
        }

        Entity_SubWeapon.Param data = MasterData.instance.dataSubWeapon;

        MaxAmmo = data.Ammon;
        MaxTotalAmmo = data.TotalAmmon;
        Damage = data.Damage;
        ReloadTime = data.ReloadTime;
        ShootInterval = data.FireRate;
        IsRapidFire = data.RapidFire;
    }
}
