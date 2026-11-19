using System.Collections.Generic;
using Unity.Netcode;

using UnityEngine;
public class WeaponManager : NetworkBehaviour
{

    //データの順番
    //武器の名前・最大マガジン容量・最大所持弾薬数・ダメージ・リロード時間・連射レート・連射可否


    /// <summary>
    /// 武器データの一覧
    /// </summary>
    private List<WeaponData> weaponDatas = new List<WeaponData>()
    {
        //ナイフ
        new WeaponData(eWeapons.Knife,false,0,0,50,0.5f,0f,false),
        //ライフル
        new WeaponData(eWeapons.Assault,true,0,0,50,0.5f,0f,false),
        //ピストル
        new WeaponData(eWeapons.Pistol,false,0,0,50,0.5f,0f,false),
    };

    public WeaponData GetWeaponData(eWeapons weapon)
    {
        return weaponDatas.Find(x => x.eWeapons == weapon);

    }


}
