using InfimaGames.LowPolyShooterPack;
using System.Collections.Generic;
using System.Globalization;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;

public class ChangeWeapon : NetworkBehaviour
{



    [SerializeField]
    List<GameObject> gameObjects;

    [SerializeField] GameObject AmmoCountText;

    //enumのeWeaponsを使用するのでintにする
    //ナイフ表示
    //https://docs.unity3d.com/Packages/com.unity.netcode.gameobjects@2.4/manual/basics/networkvariable.html
    public NetworkVariable<int> Weapons = new NetworkVariable<int>(
        (int)eWeapons.Knife,//武器の番号
        NetworkVariableReadPermission.Everyone,//Serverに参加しているPlayerすべてが読み取り可能に
        NetworkVariableWritePermission.Server//Server側だけが変更
        );

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        if (!IsOwner) return;
        //1を押したらナイフ
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ViewWeapons((int)eWeapons.Knife);
        }

        //2を押したらピストルを装備
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ViewWeapons((int)eWeapons.Pistol);
        }

        //3を押したらアサルトを装備
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ViewWeapons((int)eWeapons.Assault);
        }
    }

    /// <summary>
    /// Server側に武器変更を依頼する関数
    /// </summary>
    /// <param name="numbers"></param>
    private void ViewWeapons(int numbers)
    {
        if (!IsOwner) return;
        ViewWeaponsServerRpc(numbers);
    }

    /// <summary>
    /// Server側で全体にPlayerがなんの武器を持っているか反映する
    /// https://docs.unity3d.com/Packages/com.unity.netcode.gameobjects@2.4/manual/advanced-topics/messaging-system.html
    /// </summary>
    [ServerRpc]
    private void ViewWeaponsServerRpc(int numbers)
    {

        // 番号のチェック
        if (numbers < 0 || numbers >= gameObjects.Count)
            return;

        // NetworkVariableを変更
        Weapons.Value = numbers;

    }
    /// <summary>
    /// 値が変更されたかどうか監視する関数
    /// </summary>
    public override void OnNetworkSpawn()
    {
        //武器変更イベントを登録
        Weapons.OnValueChanged += OnWeaponChanged;

        //初期武器を表示
        UpdateWeapon(Weapons.Value);
    }
    /// <summary>
    /// ネットワークを切断した時
    /// </summary>
    public override void OnNetworkDespawn()
    {
        //武器変更イベントから削除
        Weapons.OnValueChanged -= OnWeaponChanged;
    }

    //武器番号が変更された時
    private void OnWeaponChanged(int oldValue, int newValue)
    {
        UpdateWeapon(newValue);
    }

    /// <summary>
    /// 選択した武器の表示
    /// </summary>
    /// <param name="numbers"></param>
    private void UpdateWeapon(int numbers)
    {

        if (gameObjects == null || numbers < 0 || numbers >= gameObjects.Count)
        {
            return;
        }
        //武器を変える時にすべてのオブジェクトをfalse
        for (int i = 0; i < gameObjects.Count; i++)
        {
            gameObjects[i].SetActive(false);
        }

        //選択した番号を表示する
        gameObjects[numbers].SetActive(true);


        if (!IsOwner) return;
        //テキスト表示
        switch (numbers)
        {
            case (int)eWeapons.Assault:
                AmmoCountText.SetActive(false);
                break;

            case (int)eWeapons.Knife:
                AmmoCountText.SetActive(true);
                break;

            case (int)eWeapons.Pistol:
                AmmoCountText.SetActive(true);
                break;

            default:
                return;
        }
    }
}