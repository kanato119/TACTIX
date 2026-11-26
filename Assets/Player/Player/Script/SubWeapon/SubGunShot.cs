using InfimaGames.LowPolyShooterPack;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class SubGunShot : NetworkBehaviour
{
    public static SubGunShot instance { get; private set; }

    [Header("弾")]
    [SerializeField] GameObject bulletPrefab;

    [Header("銃口")]
    [SerializeField] Transform muzzle;

    [Header("最大距離")]
    [SerializeField] float maxDistance = 1000f;

    [SerializeField] UnityEngine.Camera cam;

    [SerializeField] TextMeshProUGUI RemainingBulletsText;
    [SerializeField] TextMeshProUGUI MaxRemainingBulletsText;

    [SerializeField] private RecoilControl recoil;

    [SerializeField]
    private eWeapons currentWeapon;


    Vector3 defalutPoition;

    private bool isNotShoot = false;

    private float nextFireTime;

    private bool isReloading = false;

    SubWeaponData weaponData;

    ///networkvariable
    ///弾数の同機
    public NetworkVariable<int> AmmoCount = new NetworkVariable<int>(
        0,//仮数値
        NetworkVariableReadPermission.Everyone,
        NetworkVariableWritePermission.Server
        );

    public NetworkVariable<int> TotalAmmo = new NetworkVariable<int>(
        0,//仮数値
        NetworkVariableReadPermission.Everyone,
        NetworkVariableWritePermission.Server
        );


    private void Awake()
    {
        instance = this;

        weaponData = GetComponent<SubWeaponData>();

    }
    // Start is called before the first frame update
    void Start()
    {

        if (!IsOwner) return;

        defalutPoition = transform.localPosition;

        if (cam == null)
            cam = UnityEngine.Camera.main;

        AmmoCount.Value = weaponData.MaxAmmo;
        TotalAmmo.Value = weaponData.MaxTotalAmmo;
    }

    // Update is called once per frame


    void Update()
    {
        if (!IsOwner) return;

        Debug.Log("Updateに入りました");

        Shooting();

        //リロード
        if (Input.GetKeyDown(KeyCode.R) && !isReloading)
        {
            if (AmmoCount.Value < weaponData.MaxAmmo && TotalAmmo.Value > 0)
            {
                isReloading = true;

                StartCoroutine(Reload(weaponData.ReloadTime));
            }
        }

        ShowRemainingBullets();
    }

    /// <summary>
    /// 
    /// </summary>
    private void Shooting()
    {
        if (Time.timeScale == 0) return;
        if (isReloading) return;
        if (isNotShoot) return;

        //メイン武器かどうかで連射できるかできないか
        bool isFire = weaponData.IsRapidFire
            ? Input.GetMouseButton(0)
            : Input.GetMouseButtonDown(0);

        // 弾数チェック
        if (AmmoCount.Value <= 0)
        {
            Debug.Log("弾切れ");
            return;
        }

        //射撃ボタン
        if (isFire && Time.time >= nextFireTime)
        {
            Debug.Log("左クリックを検知しました");

            Fire();
            nextFireTime = Time.time + weaponData.ShootInterval;
        }
    }

    /// <summary>
    /// 射撃処理
    /// </summary>
    private void Fire()
    {

        if (!IsOwner) return;

        Debug.Log("ServerRpcを呼び出します");


        //画面中央からRay
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        Vector3 targetPoint;

        //狙った場所を決める
        if (Physics.Raycast(ray, out RaycastHit hit, maxDistance))
            targetPoint = hit.point;
        else
            targetPoint = ray.GetPoint(maxDistance);


        //銃口の狙った場所への方向
        Vector3 direction = (targetPoint - muzzle.position).normalized;

        //近距離ズレ防止
        if (Physics.Raycast(muzzle.position, direction, out RaycastHit hit2, maxDistance))
        {
            targetPoint = hit2.point;
            direction = (targetPoint - muzzle.position).normalized;
        }


        Quaternion rotation = Quaternion.LookRotation(direction);

        FireServerRpc(muzzle.position, rotation);

        recoil.RecoilMath();

    }

    /// <summary>
    /// 射撃後の処理
    /// </summary>
    /// <param name="position"></param>
    /// <param name="rotation"></param>
    [ServerRpc]
    private void FireServerRpc(Vector3 position, Quaternion rotation)
    {
        Debug.Log("ServerRpcに入りました");

        // 弾数を減らす
        AmmoCount.Value--;

        Debug.Log("現在の弾数：" + AmmoCount.Value);

        // サーバー側で弾を生成
        GameObject bullet = Instantiate(
            bulletPrefab,
            position,
            rotation
        );

        // Collider取得
        Collider[] playerCollider =
            transform.root.GetComponentsInChildren<Collider>();

        Collider[] bulletCollider =
            bullet.GetComponentsInChildren<Collider>();

        // 自分自身との衝突を無効化
        foreach (Collider playerCol in playerCollider)
        {
            foreach (Collider bulletCol in bulletCollider)
            {
                Physics.IgnoreCollision(playerCol, bulletCol);
            }
        }

        // NetworkObject取得
        NetworkObject networkObject =
            bullet.GetComponent<NetworkObject>();

        if (networkObject == null)
        {
            Debug.LogError("弾にNetworkObjectがありません！");
            Destroy(bullet);
            return;
        }

        // ネットワーク上に生成
        networkObject.Spawn();

        Debug.Log("ネットワーク上に弾を生成しました");
    }

    /// <summary>
    /// 残弾数の表示
    /// </summary>
    private void ShowRemainingBullets()
    {

        if (!IsOwner) return;
        RemainingBulletsText.text = AmmoCount.Value.ToString();
        MaxRemainingBulletsText.text = TotalAmmo.Value.ToString();

    }

    /// <summary>
    /// リロード処理
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    private IEnumerator Reload(float time)
    {
        if (!IsOwner) yield return false;

        Debug.Log("リロード開始");

        isNotShoot = true;

        yield return new WaitForSeconds(time);

        isNotShoot = false;
        isReloading = false;

        ReloadServerRpc();
    }

    /// <summary>
    /// 弾の補充
    /// </summary>
    [ServerRpc]
    void ReloadServerRpc()
    {

        Debug.Log("リロード中");

        int reloadAmount = weaponData.MaxAmmo - AmmoCount.Value;

        int actualReloadAmount = Mathf.Min(reloadAmount, TotalAmmo.Value);

        AmmoCount.Value += actualReloadAmount;

        TotalAmmo.Value -= actualReloadAmount;



        Debug.Log("リロード完了");
    }

    /// <summary>
    /// リロード中に武器変更したとき
    /// </summary>
    private void OnDisable()
    {
        isNotShoot = false;
        isReloading = false;
    }
}
