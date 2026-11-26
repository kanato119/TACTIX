using UnityEngine;

public class MainWeaponData : MonoBehaviour
{
    public eMainWeapon eMainWeapon { get; private set; }

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
        MainWeaponDatas();

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    public void MainWeaponDatas()
    {
        int selectWeapon = (int)eMainWeapon;
        MainWeaponID = selectWeapon;

        if (MasterData.instance == null)
        {
            Debug.LogError("MasterData.instance が null");
            return;
        }

        MasterData.instance.MainWeaponStatus(MainWeaponID);

        if (MasterData.instance.dataMainWeapon == null)
        {
            Debug.LogError("dataMainWeapon が null");
            return;
        }

        Entity_MainWeapon.Param data = MasterData.instance.dataMainWeapon;

        MaxAmmo = data.Ammon;
        MaxTotalAmmo = data.TotalAmmon;
        Damage = data.Damage;
        ReloadTime = data.ReloadTime;
        ShootInterval = data.FireRate;
        IsRapidFire = data.RapidFire;
    }
}
