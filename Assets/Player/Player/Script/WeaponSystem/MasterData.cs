using UnityEngine;

public class MasterData : MonoBehaviour
{
    public static MasterData instance {  get; private set; }

    private Entity_MainWeapon mainWeapon;

    private Entity_SubWeapon subWeapon;

    public Entity_MainWeapon.Param dataMainWeapon { get; private set; }
    public Entity_SubWeapon.Param dataSubWeapon {  get; private set; }

    private void Awake()
    {
        instance = this;

        mainWeapon = Resources.Load<Entity_MainWeapon>("Excel/MainWeapon");

        subWeapon = Resources.Load<Entity_SubWeapon>("Excel/SubWeapon");

        if (mainWeapon == null)
        {
            Debug.LogError("MainWeaponの読み込みに失敗しました");
        }

        if (subWeapon == null)
        {
            Debug.LogError("SubWeaponの読み込みに失敗しました");
        }
    }

    public void MainWeaponStatus(int weaponID)
    {
        foreach(Entity_MainWeapon.Sheet sheet in mainWeapon.sheets)
        {
            foreach(Entity_MainWeapon.Param param in sheet.list)
            {
                if(param.ID == weaponID)
                {
                    dataMainWeapon = param; 
                    break;
                }
            }
        }
    }

    public void SubWeaponStatus(int weaponID)
    {
        foreach(Entity_SubWeapon.Sheet sheet in subWeapon.sheets)
        {
            foreach(Entity_SubWeapon.Param param in sheet.list)
            {
                if(param.ID == weaponID)
                {
                    dataSubWeapon = param;
                    break;
                }
            }
        }
    }
}
