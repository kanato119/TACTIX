using UnityEngine;

public class MasterData : MonoBehaviour
{
    public static MasterData instance {  get; private set; }

    private Entity_MainWeapon mainWeapon;

    private Entity_SubWeapon subWeapon;

    private Entity_RecoilPattern recoilPattern;

    public Entity_MainWeapon.Param dataMainWeapon { get; private set; }
    public Entity_SubWeapon.Param dataSubWeapon {  get; private set; }

    public Entity_RecoilPattern.Param dataRecoil {  get; private set; }

    private void Awake()
    {
        instance = this;

        mainWeapon = Resources.Load<Entity_MainWeapon>("Excel/MainWeapon");

        subWeapon = Resources.Load<Entity_SubWeapon>("Excel/SubWeapon");

        recoilPattern = Resources.Load<Entity_RecoilPattern>("Excel/RecoilPattern");
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

    public void RecoilPatten(int bulletNumber, string sheetName)
    {
        dataRecoil = null;
        foreach(Entity_RecoilPattern.Sheet sheet in recoilPattern.sheets)
        {
            if (sheet.name != sheetName)
                continue;
            foreach (Entity_RecoilPattern.Param param in sheet.list)
            {
                if(param.BulletNumber == bulletNumber)
                {
                    dataRecoil = param;
                    return;
                }
            }
        }
    }    
}
